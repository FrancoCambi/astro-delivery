using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
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

    private int maxLevelUnlocked;
    private GameData gameData;

    private void Start()
    {
        maxLevelUnlocked = PersistenceManager.Load().MaxLevelUnlocked;
        gameData = PersistenceManager.Load();
    }

    public void ShowTooltip(int level, Vector2 pos)
    {
        LevelRecord record = new LevelRecord
        {
            CompletedTime = gameData.MinTimes[level],
            Stars = gameData.LevelStars[level],
        };

        for (int i = 0; i < 3; i++)
        {
            tooltipStars[i].sprite = emptyStar;
            if (i < record.Stars) tooltipStars[i].sprite = fullStar;
        }

        string notCompletedLocalized = new LocalizedString("UI", "NotCompletedLevel").GetLocalizedString();
        string lockedLocalized = new LocalizedString("UI", "LockedLevel").GetLocalizedString();

#if FULL_BUILD
        tooltipText.text = record.CompletedTime != float.MaxValue ? SecondsToTimeText(record.CompletedTime) 
            : level <= maxLevelUnlocked ? notCompletedLocalized : lockedLocalized;
#endif
#if DEMO_BUILD
        tooltipText.text = record.CompletedTime != float.MaxValue ? SecondsToTimeText(record.CompletedTime)
            : level <= GameConstants.DemoLevels ? level <= maxLevelUnlocked ? notCompletedLocalized : lockedLocalized : lockedLocalized;
#endif

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
