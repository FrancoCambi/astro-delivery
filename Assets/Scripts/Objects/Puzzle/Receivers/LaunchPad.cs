using System.Collections;
using UnityEngine;

public class LaunchPad : PuzzleReceiver
{
    [Header("Platform References")]
    [SerializeField] private Sprite spriteDown;
    [SerializeField] private Sprite spriteUp;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private BoxCollider2D downCollider;
    [SerializeField] private BoxCollider2D upCollider;

    [Header("Platform Settings")]
    [SerializeField] private float pushForce;
    [SerializeField] private float restoreTime;
    [SerializeField] private bool activated;

    private SpriteRenderer sr;

    private float delay = 1f;
    private bool used = false;
    private float timeAfterUsed = 0f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (!activated) sr.color = Color.gray;
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
        activated = true;
        sr.color = Color.white;
    }

    public override void Restore()
    {
        activated = false;
        sr.color = Color.gray;

    }

    private void Launch()
    {
        StartCoroutine(ApplyForce());
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
        yield return new WaitForSeconds(delay);
        sr.sprite = spriteUp;
        downCollider.enabled = false;
        upCollider.enabled = true;
        playerMovement.ExternalJumpBoost(pushForce);
        used = true;
        timeAfterUsed = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !used && activated)
        {
            Launch();
        }
    }
}
