using UnityEngine;

public class PlayerHurtbox : MonoBehaviour
{
    public static GameObject TouchingSpike = null;
    public static GameObject TouchingWheel = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RedSpike"))
        {
            TouchingSpike = collision.gameObject;
        }
        else if (collision.gameObject.CompareTag("Wheel"))
        {
            TouchingWheel = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RedSpike") && collision.gameObject == TouchingSpike)
        {
            TouchingSpike = null;
        }
        else if (collision.gameObject.CompareTag("Wheel") && collision.gameObject == TouchingWheel)
        {
            TouchingWheel = null;
        }
    }
}
