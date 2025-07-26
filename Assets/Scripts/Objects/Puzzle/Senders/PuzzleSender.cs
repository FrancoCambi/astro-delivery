using UnityEngine;

public abstract class PuzzleSender : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PuzzleReceiver receiver;

    [Header("Settings")]
    [SerializeField] protected bool releaseAfterTime;
    [SerializeField] protected float releaseTime;

    #region methods
    public virtual void Activate()
    {
        receiver.Actuate();
    }

    public virtual void DeActivate()
    {
        receiver.Restore();
    }

    #endregion
}
