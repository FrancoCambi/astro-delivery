using UnityEngine;

public abstract class PuzzleSender : MonoBehaviour
{
    [Header("Sender References")]
    [SerializeField] PuzzleReceiver receiver;
    [SerializeField] private Sprite spriteUnused;
    [SerializeField] private Sprite spriteUsed;

    [Header("Sender Settings")]
    [SerializeField] protected bool releaseAfterTime;
    [SerializeField] protected float releaseTime;

    protected SpriteRenderer spriteRenderer;

    protected bool used = false;
    protected float timeAfterPressed = 0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    protected virtual void Update()
    {
        if (releaseAfterTime && used)
        {
            timeAfterPressed += Time.deltaTime;

            if (timeAfterPressed >= releaseTime)
            {
                DeActivate();
                timeAfterPressed = 0;
            }
        }
    }

    #region methods
    public virtual void Activate()
    {
        receiver.Actuate();
        used = true;
        spriteRenderer.sprite = spriteUsed;
        timeAfterPressed = 0f;
    }

    public virtual void DeActivate()
    {
        receiver.Restore();
        used = false;
        spriteRenderer.sprite = spriteUnused;

    }

    #endregion
}
