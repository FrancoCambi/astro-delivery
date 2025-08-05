using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Package : MonoBehaviour
{
    private static Package instance;

    public static Package Instance
    {
        get
        {
            return instance;
        }
    }

    [Header("Settings")]
    [SerializeField] private Vector2 offset;
    [SerializeField] private bool startsFloating;

    [Header("References")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject player;
    [SerializeField] private PhysicsMaterial2D normalFriction;
    [SerializeField] private PhysicsMaterial2D highFriction;
    [SerializeField] private AudioClip grabClip;
    [SerializeField] private AudioClip dropClip;

    private Rigidbody2D rb;
    private SpriteRenderer playerRenderer;
    private new BoxCollider2D collider;
    
    private Vector3 startingPos;

    private bool hardDropped = false;
    private bool calculatingTime = false;
    private float fallingTime = 0f;
    public bool IsBeingHeld => transform.parent != null;
    public bool Grounded => rb.linearVelocity.y == 0f;
    public bool Falling => !Grounded && rb.linearVelocity.y < 0f;
   
    private void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();
        playerRenderer = player.GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();

        startingPos = transform.position;
        hardDropped = false;

        if (startsFloating) rb.gravityScale = 0f;
    }

    private void Update()
    {
        CheckBreak();
    }

    #region management

    public void Grab()
    {
        if (hardDropped) return;
        if (startsFloating) ResetGravity();

        rb.linearVelocity = Vector2.zero;
        calculatingTime = false;

        transform.rotation = Quaternion.identity;
        transform.SetParent(player.transform);
        transform.localPosition = offset;

        gameObject.layer = LayerMask.NameToLayer("Player");

        rb.bodyType = RigidbodyType2D.Kinematic;


        SoundFXManager.Instance.PlaySoundFXClip(grabClip, transform);

    }

    public void HardDrop()
    {
        hardDropped = true;
        collider.isTrigger = true;

        rb.linearVelocity = Vector3.zero;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(new Vector2(0, 6f), ForceMode2D.Impulse);

        transform.SetParent(null);

        SoundFXManager.Instance.PlaySoundFXClip(dropClip, transform);

    }

    public void SoftDrop()
    {
        rb.linearVelocity = Vector3.zero;
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.SetParent(null);
        gameObject.layer = LayerMask.NameToLayer("Default");

        Vector2 dropDir = GetSoftDropDir();
        rb.AddForce(dropDir, ForceMode2D.Impulse);

        SoundFXManager.Instance.PlaySoundFXClip(dropClip, transform);
    }

    #endregion

    #region utils

    private void CheckBreak()
    {
        if (Falling && !calculatingTime)
        {
            calculatingTime = true;
            fallingTime = 0f;
        }
        else if (Falling && calculatingTime)
        {
            fallingTime += Time.deltaTime;
        }
        else
        {
            if (!IsBeingHeld && !hardDropped && fallingTime >= 1f)
            {
                Respawn();
            }
            calculatingTime = false;
            fallingTime = 0f;
        }
    }

    private void Respawn()
    {
        gameObject.SetActive(false);
        transform.position = startingPos;
        gameObject.SetActive(true);
        SetUp();

    }

    private void SetUp()
    {
        hardDropped = false;
        collider.isTrigger = false;
        gameObject.layer = LayerMask.NameToLayer("Default");
        transform.rotation = Quaternion.identity;
        rb.gravityScale = 0f;

    }

    private Vector2 GetSoftDropDir()
    {
        return playerRenderer.flipX ? new Vector2(-4,1) : new Vector2(4,1);
    }

    private void ResetGravity()
    {
        if (rb)
        {
            rb.gravityScale = 1f;
        }
    }

    #endregion

    #region collisions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && hardDropped)
        {
            Respawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (startsFloating) ResetGravity();

            rb.sharedMaterial = normalFriction;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.sharedMaterial = highFriction;
        }
    }
    #endregion
}
