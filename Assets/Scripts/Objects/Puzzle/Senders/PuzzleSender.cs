using UnityEngine;

public abstract class PuzzleSender : MonoBehaviour
{
    [Header("Sender References")]
    [SerializeField] PuzzleReceiver[] receivers;
    [SerializeField] private Sprite spriteUnused;
    [SerializeField] private Sprite spriteUsed;

    [Header("Sender Settings")]
    [SerializeField] protected bool releaseAfterTime;
    [SerializeField] protected float releaseTime;

    [Header("Audio")]
    [SerializeField] private AudioClip activateClip;
    [SerializeField] private AudioClip deActivateClip;

    protected SpriteRenderer spriteRenderer;

    protected bool used = false;
    protected float timeAfterPressed = 0f;

    private void Awake()
    {
        if (spriteUsed || spriteUnused) spriteRenderer = GetComponent<SpriteRenderer>();
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
        foreach (PuzzleReceiver receiver in receivers)
        {
            receiver.Actuate();
        }
        used = true;
        if (spriteUsed) spriteRenderer.sprite = spriteUsed;
        timeAfterPressed = 0f;

        if (activateClip) SoundFXManager.Instance.PlaySoundFXClip(activateClip, transform);
    }

    public virtual void DeActivate()
    {
        foreach (PuzzleReceiver receiver in receivers)
        {
            receiver.Restore();
        }
        used = false;
        if (spriteUnused) spriteRenderer.sprite = spriteUnused;

        if (deActivateClip) SoundFXManager.Instance.PlaySoundFXClip(deActivateClip, transform);
    }

    #endregion
}
