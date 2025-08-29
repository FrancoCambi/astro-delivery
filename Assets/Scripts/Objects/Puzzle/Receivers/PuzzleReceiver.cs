using UnityEngine;

public abstract class PuzzleReceiver : MonoBehaviour
{
    [Header("Receiver Settings")]
    [SerializeField] private int activatorsAmount = 1;

    private int currentActivators = 0;

    public abstract void Actuate();

    public abstract void Restore();

    public virtual void ReceiveActivation()
    {
        currentActivators++;

        if (currentActivators == activatorsAmount)
        {
            Actuate();
        }
    }

    public virtual void ReceiveDeactivation()
    {
        if (currentActivators > 0) currentActivators--;
        Restore();
    }
}
