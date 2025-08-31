using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelsMenuEventSystemHandler : MenuEventSystemHandler
{
    [Header("Levels Menu References")]
    [SerializeField] protected List<GameObject> levelsGO = new();

    private int maxLevelUnlocked;

    public override void Awake()
    {
        maxLevelUnlocked = PersistenceManager.Load().MaxLevelUnlocked;

        foreach (GameObject go in levelsGO)
        {
            int levelNumber = go.GetComponent<LevelButton>().Level;
#if DEMO_BUILD
            if (levelNumber > maxLevelUnlocked
                || levelNumber > GameConstants.DemoLevels) continue;
#endif

#if FULL_BUILD
            if (levelNumber > maxLevelUnlocked) continue;
#endif

            Button levelButton = go.GetComponent<Button>();
            AddSelectionListeners(levelButton);
            scales.Add(levelButton, levelButton.transform.localScale);
        }

        base.Awake();
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);

        if (eventData.selectedObject.GetComponent<LevelButton>() is LevelButton btn and not null)
        {
            btn.ShowTooltip();
        }
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);

        if (eventData.selectedObject.GetComponent<LevelButton>() is LevelButton btn and not null)
        {
            btn.HideTooltip();
        }
    }
}

    
