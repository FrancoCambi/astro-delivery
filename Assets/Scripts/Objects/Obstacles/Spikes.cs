using UnityEngine;

public class Spikes : MonoBehaviour
{
    [Header("Spikes References")]
    [SerializeField] private PlayerCarryHandler carryHandler;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && carryHandler.IsCarrying)
        {
            carryHandler.HardDropPackage();
        }
    }
}
