using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerKeyManager playerKeyManager;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private AudioClip openClip;

    private SpriteRenderer spriteRenderer;

    private bool isOpen = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Open()
    {
        isOpen = true;
        spriteRenderer.sprite = openSprite;
        SoundFXManager.Instance.PlaySoundFXClip(openClip, transform);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (!isOpen)
        {
            if (playerKeyManager.HasKey)
            {
                Open();
                playerKeyManager.ConsumeKey();
            }
            else
            {
                WarningManager.Instance.ShowWarning("You need the key!", 1.5f);
            }

            return;
        }

        if (isOpen)
        {
            if (Package.Instance.IsBeingHeld)
            {
                GameController.Instance.WinLevel();
            }
            else
            {
                WarningManager.Instance.ShowWarning("You need the package!", 1.5f);
            }

            return;
        }
    }

}
