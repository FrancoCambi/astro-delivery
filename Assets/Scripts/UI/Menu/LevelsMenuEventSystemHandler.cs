using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsMenuEventSystemHandler : MenuEventSystemHandler
{
    [Header("Levels Menu References")]
    [SerializeField] protected List<GameObject> levelsGO = new();

    public override void Awake()
    {
        foreach (GameObject go in levelsGO)
        {
            int levelNumber = go.GetComponent<LevelButton>().Level;
            if (levelNumber > LevelManager.Instance.MaxLevelUnlocked) continue;

            Button levelButton = go.GetComponent<Button>();
            AddSelectionListeners(levelButton);
            scales.Add(levelButton, levelButton.transform.localScale);
        }

        base.Awake();
    }
}

    
