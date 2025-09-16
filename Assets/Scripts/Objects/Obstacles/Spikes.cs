using UnityEngine;

public class Spikes : MonoBehaviour
{

    private PlayerCarryHandler carryHandler;

    private void Start()
    {
        carryHandler = FindAnyObjectByType<PlayerCarryHandler>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;


        else if (carryHandler.IsCarrying)
        {
            carryHandler.HardDropPackage();
        }
    }
}
