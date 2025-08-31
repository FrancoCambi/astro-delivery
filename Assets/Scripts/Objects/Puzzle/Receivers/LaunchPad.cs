using System.Collections;
using UnityEngine;

public class LaunchPad : PuzzleReceiver
{
    [Header("LaunchPad References")]
    [SerializeField] private Sprite spriteDown;
    [SerializeField] private Sprite spriteUp;
    [SerializeField] private BoxCollider2D downCollider;
    [SerializeField] private BoxCollider2D upCollider;

    [Header("LaunchPad Settings")]
    [SerializeField] private float pushForce;
    [SerializeField] private float restoreTime;
    [SerializeField] private bool activated;

    [Header("Audio")]
    [SerializeField] private AudioClip soundClip;

    private SpriteRenderer sr;

    private PlayerMovement playerMovement;

    private readonly float delay = 0.5f;
    private bool used = false;
    private float timeAfterUsed = 0f;
    private bool playerOnTop;
    private bool routineRunning = false;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (!activated) sr.color = Color.gray;

        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    private void Update()
    {
        if (used)
        {
            timeAfterUsed += Time.deltaTime;

            if (timeAfterUsed >= restoreTime)
            {
                SetDown();
                timeAfterUsed = 0f;
            }
        }
    }

    public override void Actuate()
    {
        activated = !activated;
        sr.color = activated ? Color.white : Color.gray;
    }

    public override void Restore()
    {
        activated = !activated;
        sr.color = activated ? Color.white : Color.gray;

    }

    private void Launch()
    {
        if (!routineRunning) StartCoroutine(ApplyForce());
    }

    private void SetDown()
    {
        used = false;
        sr.sprite = spriteDown;
        downCollider.enabled = true;
        upCollider.enabled = false;
    }

    private IEnumerator ApplyForce()
    {
        routineRunning = true;
        yield return new WaitForSeconds(delay);
        used = true;
        sr.sprite = spriteUp;
        downCollider.enabled = false;
        upCollider.enabled = true;
        if (playerOnTop) playerMovement.ExternalJumpBoost(pushForce);
        timeAfterUsed = 0f;

        SoundFXManager.Instance.PlaySoundFXClip(soundClip, transform);

        routineRunning = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !used && activated)
        {
            playerOnTop = true;
            Launch();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && activated)
        {
            playerOnTop = false;
        }
    }
}
