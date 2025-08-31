using UnityEngine;

public class Spikes : MonoBehaviour
{
    [Header("Spike Settings")]
    [SerializeField] private bool deadly;

    private PlayerCarryHandler carryHandler;

    private void Start()
    {
        carryHandler = FindAnyObjectByType<PlayerCarryHandler>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (deadly && PlayerHurtbox.CanDie)
        {
            GameController.Instance.LoseLevel();
            PlayerHurtbox.CanDie = false;        
        }
        else if (carryHandler.IsCarrying && !deadly)
        {
            carryHandler.HardDropPackage();
        }
    }
}
