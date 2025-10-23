using UnityEngine;

public class KeyCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject images;

    private void OnEnable()
    {
        PlayerKeyManager.OnKeyCollected += Show;
        PlayerKeyManager.OnKeyConsumed += Hide;
    }

    private void OnDisable()
    {
        PlayerKeyManager.OnKeyCollected -= Show;
        PlayerKeyManager.OnKeyConsumed -= Hide;
    }

    private void Show()
    {
        images.SetActive(true);
    }

    private void Hide()
    {
        images.SetActive(false);
    }
}

