using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (!Package.Instance.IsBeingHeld)
        {
            print("You need the package!");
            return;
        }

        PlayerCarryHandler.RequestDrop();
        //GameController.Instance.AdvanceToNextLevel();
    }
}
