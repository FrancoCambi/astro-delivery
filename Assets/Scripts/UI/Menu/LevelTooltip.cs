using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTooltip : MonoBehaviour
{
    private static LevelTooltip instance; 

    public static LevelTooltip Instance
    {
        get
        {
            instance = instance != null ? instance : FindAnyObjectByType<LevelTooltip>();
            return instance;
        }
    }

    [Header("References")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup group;
    [SerializeField] private Image[] tooltipStars;
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite emptyStar;

    [Header("Settings")]
    [SerializeField] private Vector2 offset;

    public void ShowTooltip(int level, Vector2 pos)
    {
        LevelRecord record = LevelManager.Instance.GetLevelRecord(level);

        for (int i = 0; i < 3; i++)
        {
            tooltipStars[i].sprite = emptyStar;
            if (i < record.Stars) tooltipStars[i].sprite = fullStar;
        }

        tooltipText.text = record.CompletedTime != Mathf.Infinity ? SecondsToTimeText(record.CompletedTime) : "Not completed.";

        rectTransform.anchoredPosition = pos + offset;

        group.alpha = 1f;
        group.blocksRaycasts = true;

    }

    public void HideTooltip()
    {
        group.alpha = 0f;
        group.blocksRaycasts = false;
    }

    private string SecondsToTimeText(float seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);

        String[] values = timeSpan.ToString(@"mm\:ss\:ff").Split(":");

        return $"{values[0]}:{values[1]}<size=40>:{values[2]}</size>";
    }
}
