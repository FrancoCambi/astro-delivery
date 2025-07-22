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
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private Vector3 offset;

    private GameObject player;
    private Camera mainCamera;
    private RectTransform rectTransform;

    private void Awake()
    {
        instance = this;
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        // Inefficient, i know.
        // Does not matter given the size of the game. For now.
        player = GameObject.FindGameObjectWithTag("Player");

        mainCamera = Camera.main;
    }

    private void Update()
    {
        rectTransform.position = player.transform.position + offset;
    }

    public void ShowTooltip(string text)
    {
        tooltipText.enabled = true;
        tooltipText.text = text;
    }

    public void HideTooltip()
    {
        tooltipText.enabled = false;
    }
}
