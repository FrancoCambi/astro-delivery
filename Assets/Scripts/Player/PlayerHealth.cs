using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.contacts[0].otherCollider.CompareTag("Player")) return;

        if (collision.gameObject.CompareTag("RedSpike") || collision.gameObject.CompareTag("Wheel"))
        {
            GameController.Instance.LoseLevel();
        }
    }
}
