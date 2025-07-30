using System;
using UnityEngine;

public struct FrameInput
{
    public bool JumpDown;
    public bool JumpHeld;
    public Vector2 Move;
}
public class PlayerMovement : MonoBehaviour
{
    #region properties

    [Header("Modifiers")]
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float jumpBuffer;
    [SerializeField] private float coyoteTime;

    [Header("Physics")]
    [SerializeField] private float acceleration;
    [SerializeField] private float groundDeceleration;
    [SerializeField] private float airDeceleration;
    [SerializeField] private float groundingForce;
    [SerializeField] private float fallAcceleration;
    [SerializeField] private float jumpEndEarlyGravityModifier;

    [Header("Misc")]
    [SerializeField] LayerMask jumpCollisionMask;
    [SerializeField] float grounderDistance;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 frameVelocity;
    private float launchForce;

    private PlayerColliderManager playerColliderManager;
    private PlayerAnimations playerAnimations;
    private FrameInput frameInput;

    private bool grounded = true;
    private bool endedJumpEarly = false;
    private bool jumpToConsume = false;
    private float time;
    private float timeJumpWasPressed;
    private float frameLeftGrounded = float.MinValue;
    private bool bufferedJumpUsable;
    private bool coyoteUsable;
    private bool launchToConsume = false;
    private bool launchJumping = false;
    private bool madeFirstMove = false;

    private bool HasBufferedJump => bufferedJumpUsable && time < timeJumpWasPressed + jumpBuffer;
    private bool CanUseCoyote => coyoteUsable && !grounded && time < frameLeftGrounded + coyoteTime;
    public bool Grounded => grounded;
    public bool Falling => !grounded && frameVelocity.y < 0;

    public static Action OnFirstMove;

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        playerAnimations = GetComponent<PlayerAnimations>();

    }

    private void Start()
    {
        playerColliderManager = GetComponent<PlayerColliderManager>();

    }

    private void Update()
    {
        if (GameController.Instance.IsPaused) return;

        time += Time.deltaTime;
        GatherInput();
        SetAnimation();
        FlipSprite();
    }

    private void FixedUpdate()
    {
        CheckCollisions();

        HandleJump();
        HandleMovement();
        HandleGravity();

        ApplyMovement();
    }

    private void GatherInput()
    {
        frameInput = new FrameInput
        {
            JumpDown = Input.GetButtonDown("Jump"),
            JumpHeld = Input.GetButton("Jump"),
            Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
        };

        if (frameInput.JumpDown)
        {
            jumpToConsume = true;
            timeJumpWasPressed = time;
        }

        if (!madeFirstMove && (frameInput.Move != Vector2.zero || frameInput.JumpDown))
        {
            OnFirstMove?.Invoke();
            madeFirstMove = true;
        }
    }


    private void SetAnimation()
    {
        if (frameInput.Move.x != 0f && grounded)
        {
            playerAnimations.State = PlayerAnimationState.Walk;
        }
        else if (!grounded)
        {
            playerAnimations.State = PlayerAnimationState.Jump;
        }
        else
        {
            playerAnimations.State = PlayerAnimationState.Idle;
        }
    }

    private void FlipSprite()
    {
        if (frameInput.Move.x > 0f)
        {
            sr.flipX = false;
        }
        else if (frameInput.Move.x < 0f)
        {
            sr.flipX = true;
        }
    }

    private void CheckCollisions()
    {
        BoxCollider2D currentCollider = playerColliderManager.CurrentCollider;

        bool groundHit = Physics2D.BoxCast(currentCollider.bounds.center, currentCollider.size, 0, Vector2.down, grounderDistance, ~jumpCollisionMask);
        bool ceilingHit = Physics2D.BoxCast(currentCollider.bounds.center, currentCollider.size, 0, Vector2.up, grounderDistance, ~jumpCollisionMask);

        if (ceilingHit) frameVelocity.y = Mathf.Min(0, frameVelocity.y);
        
        if (!grounded && groundHit)
        {
            grounded = true;
            launchJumping = false;
            coyoteUsable = true;
            bufferedJumpUsable = true;
            endedJumpEarly = false;
        }
        else if (grounded && !groundHit)
        {
            grounded = false;
            frameLeftGrounded = time;
        }
    }

    private void HandleMovement()
    {
        if (frameInput.Move.x == 0)
        {
            float deceleration = grounded ? groundDeceleration : airDeceleration;
            frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, frameInput.Move.x * maxMoveSpeed, acceleration * Time.fixedDeltaTime);
        }
    }

    private void HandleJump()
    {
        if (!endedJumpEarly && !grounded && !frameInput.JumpHeld && !launchJumping && frameVelocity.y > 0f) endedJumpEarly = true;

        if (!jumpToConsume && !HasBufferedJump && !launchToConsume) return;

        if (grounded || CanUseCoyote) ExecuteJump();

        jumpToConsume = false;
        launchToConsume = false;
    }

    private void ExecuteJump()
    {
        float force = jumpForce;

        if (launchToConsume)
        {
            force = launchForce;
            launchJumping = true;
        }
        
        endedJumpEarly = false;
        timeJumpWasPressed = 0f;
        bufferedJumpUsable = false;
        coyoteUsable = false;
        frameVelocity.y = force;
    }

    private void HandleGravity()
    {
        if (grounded && frameVelocity.y <= 0f)
        {
            frameVelocity.y = groundingForce;
        }
        else
        {
            float inAirGravity = fallAcceleration;
            if (endedJumpEarly && frameVelocity.y > 0) inAirGravity *= jumpEndEarlyGravityModifier;
            frameVelocity.y = Mathf.MoveTowards(frameVelocity.y, -maxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    private void ApplyMovement() => rb.linearVelocity = frameVelocity;

    #region extras
    public void ExternalJumpBoost(float force)
    {
        launchForce = force;
        launchToConsume = true;
    }

    #endregion
}
