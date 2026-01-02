using TMPro;
using UnityEditor;
using UnityEditor.Localization.Editor;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class AttemptsCounter : MonoBehaviour
{
    private LocalizedString localizedString;
    private IntVariable attempts;
    private GameData gameData;

    private void Start()
    {
        localizedString = GetComponent<LocalizeStringEvent>().StringReference;
        attempts = null;
        if (!localizedString.TryGetValue("attemptsCount", out var variable))
        {
            attempts = new IntVariable();
            localizedString.Add("attemptsCount", attempts);
        }
        else
        {
            attempts = variable as IntVariable;
        }
        
        gameData = PersistenceManager.Load();
        UpdateCounter(LevelManager.Instance.PlayingLevel);
    }

    private void OnEnable()
    {
        GameController.OnRetry += UpdateCounter;
    }
    private void OnDisable()
    {
        GameController.OnRetry -= UpdateCounter;
    }

    private void UpdateCounter(int level)
    {
        attempts.Value = gameData.LevelAttempts[level];
    }
}
