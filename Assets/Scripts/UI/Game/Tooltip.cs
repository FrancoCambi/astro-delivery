using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;

    public static Tooltip Instance
    {
        get
        {
            return instance;
        }
    }

    [Header("Settings")]
    [SerializeField] private float Yoffset;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject player;

    private Camera mainCamera;
    private RectTransform rectTransform;
    private Vector3 desiredPosition;

    private void Awake()
    {
        instance = this;
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        rectTransform.localPosition = desiredPosition;
    }

    public void ShowTooltip(string text)
    {
        tooltipText.enabled = true;
        tooltipText.text = text;

        desiredPosition = GetClampedPosition();
    }

    public void HideTooltip()
    {
        tooltipText.enabled = false;
    }

    private Vector3 GetClampedPosition()
    {
        Vector3 playerScreenPos = mainCamera.WorldToScreenPoint(player.transform.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            playerScreenPos,
            mainCamera,
            out Vector2 playerLocalPos
        );

        Vector3 desiredPosition = new()
        {
            x = Mathf.Clamp(playerLocalPos.x, -(Screen.width / 2f) + rectTransform.rect.width / 2f, (Screen.width / 2f) - rectTransform.rect.width / 2f),
            y = Mathf.Clamp(playerLocalPos.y + Yoffset, playerLocalPos.y + Yoffset + 45, (Screen.height / 2f) - rectTransform.rect.height / 2f - 45 - Yoffset),
            z = 0
        };

        return desiredPosition;
    }
}
