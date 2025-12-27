using System;
using UnityEngine;

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
    [SerializeField] private BoxCollider2D bottomCol;

    private Rigidbody2D rb;
    private SpriteRenderer playerRenderer;
    
    private Vector3 startingPos;

    private bool hardDropped = false;
    private bool calculatingTime = false;
    private float fallingTime = 0f;
    public bool IsBeingHeld { get; private set; }
    public bool Grounded => rb.linearVelocity.y == 0f;
    public bool Falling => !Grounded && rb.linearVelocity.y < 0f;
    public bool GoingToRespawn { get; private set; }

    public static Action OnGrabbed;

    private void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();
        playerRenderer = player.GetComponent<SpriteRenderer>();

        startingPos = transform.position;
        hardDropped = false;

        GoingToRespawn = false;
    }

    private void Start()
    {
        SetGravity();
    }

    private void Update()
    {
        CheckBreak();
    }

    #region management

    public void Grab()
    {
        if (hardDropped) return;

        ResetGravity();
        IsBeingHeld = true;

        rb.linearVelocity = Vector2.zero;
        calculatingTime = false;

        transform.rotation = Quaternion.identity;
        transform.SetParent(null);
        transform.SetParent(player.transform);
        transform.localPosition = offset;

        gameObject.layer = LayerMask.NameToLayer("Player");

        rb.bodyType = RigidbodyType2D.Kinematic;


        SoundFXManager.Instance.PlaySoundFXClip(grabClip, transform);

        OnGrabbed?.Invoke();

    }

    public void HardDrop()
    {
        IsBeingHeld = false;
        hardDropped = true;
        bottomCol.isTrigger = true;

        rb.linearVelocity = Vector3.zero;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(new Vector2(0, 6f), ForceMode2D.Impulse);

        transform.SetParent(null);

        SoundFXManager.Instance.PlaySoundFXClip(dropClip, transform);

    }

    public void SoftDrop()
    {
        IsBeingHeld = false;
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
                GoingToRespawn = true;
                Respawn();
                SteamAchievementsEventsHandler.RaiseBoxDestroyed();
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
        bottomCol.isTrigger = false;
        gameObject.layer = LayerMask.NameToLayer("Default");
        transform.rotation = Quaternion.identity;
        if (startsFloating) rb.gravityScale = 0f;
        GoingToRespawn = false;

    }

    private Vector2 GetSoftDropDir()
    {
        return playerRenderer.flipX ? new Vector2(-4,1) : new Vector2(4,1);
    }

    private void SetGravity()
    {
        if (!rb) return;
        rb.gravityScale = startsFloating ? 0f : 1f;
    }

    private void ResetGravity()
    {
        if (!rb) return;
        rb.gravityScale = 1f;
    }

    #endregion

    #region collisions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;

        if (hardDropped)
        {
            Respawn();
            SteamAchievementsEventsHandler.RaiseBoxDestroyed();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResetGravity();
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
