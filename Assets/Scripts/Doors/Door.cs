using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (!PlayerManager.Instance.HoldingPackage)
        {
            print("You need the package!");
            return;
        }
        
        GameController.Instance.AdvanceToNextLevel();
    }
}
