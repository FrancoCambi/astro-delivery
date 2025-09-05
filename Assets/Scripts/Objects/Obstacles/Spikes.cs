using UnityEngine;

public class Spikes : MonoBehaviour
{
    [Header("Spike Settings")]
    [SerializeField] private bool deadly;

    private PlayerCarryHandler carryHandler;
    private bool lost = false;

    private void Start()
    {
        carryHandler = FindAnyObjectByType<PlayerCarryHandler>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (deadly && PlayerHurtbox.TouchingSpike == gameObject)
        {
            GameController.Instance.LoseLevel();
        }
        else if (carryHandler.IsCarrying && !deadly)
        {
            carryHandler.HardDropPackage();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (deadly && PlayerHurtbox.TouchingSpike == gameObject && !lost)
        {
            lost = true;
            GameController.Instance.LoseLevel();       
        }
        else if (carryHandler.IsCarrying && !deadly)
        {
            carryHandler.HardDropPackage();
        }
    }
}
