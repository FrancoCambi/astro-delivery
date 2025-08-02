using UnityEngine;

public class Spikes : MonoBehaviour
{
    [Header("Spike References")]
    [SerializeField] private PlayerCarryHandler carryHandler;

    [Header("Spike Settings")]
    [SerializeField] private bool deadly;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (deadly)
        {
            GameController.Instance.LoseLevel();
        }
        else if (carryHandler.IsCarrying)
        {
            carryHandler.HardDropPackage();
        }
    }
}
