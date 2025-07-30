using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (!Package.Instance.IsBeingHeld)
        {
            WarningManager.Instance.ShowWarning("You need the package!", 1.5f);
            return;
        }

        GameController.Instance.WinLevel();
    }
}
