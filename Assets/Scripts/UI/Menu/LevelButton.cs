using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private LevelsMenu levelsMenu;
    
    [Header("Settings")]
    [SerializeField] private int level;

    private Button btn;
    private TextMeshProUGUI levelText;
    private RectTransform rectTransform;
    private int maxLevelUnlocked;

    public int Level
    {
        get
        {
            return level;
        }
    }

    private void Awake()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(StartLevel);
        levelText = GetComponentInChildren<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        maxLevelUnlocked = PersistenceManager.Load().MaxLevelUnlocked;
    }

    private void Start()
    {
        SetInteractable();
    }

    public void SetInteractable()
    {

#if DEMO_BUILD
        if (level > GameConstants.DemoLevels)
        {
            levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, 128);
            btn.interactable = false;
        }
#endif

        if (level > maxLevelUnlocked)
        {
            levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, 128);
            btn.interactable = false;
        }
    }

    public void ShowTooltip()
    {
        LevelTooltip.Instance.ShowTooltip(level, rectTransform.localPosition);
    }

    public void HideTooltip()
    {
        LevelTooltip.Instance.HideTooltip();
    }

    private void StartLevel()
    {
        levelsMenu.LoadLevel(level);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideTooltip();
    }
}
