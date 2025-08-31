using UnityEngine;

public class PlayerHurtbox : MonoBehaviour
{
    public static bool CanDie = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RedSpike"))
        {
            CanDie = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RedSpike"))
        {
            CanDie = false;
        }
    }
}
