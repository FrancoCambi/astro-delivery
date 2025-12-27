using System;
using UnityEngine;

public struct LevelCompletedData
{
    public int levelIndex;
    public int stars;
    public float time;
}

public class SteamAchievementsEventsHandler : MonoBehaviour
{
    private void OnEnable()
    {
        OnLevelCompleted += HandleLevelCompleted;
        OnBoxDestroyed += HandleBoxDestroyed;
    }

    private void OnDisable()
    {
        OnLevelCompleted -= HandleLevelCompleted;
        OnBoxDestroyed -= HandleBoxDestroyed;

    }

    #region events

    public static event Action<LevelCompletedData> OnLevelCompleted;

    public static event Action OnBoxDestroyed;

    #endregion

    #region Raisers

    public static void RaiseLevelCompleted(LevelCompletedData levelCompletedData)
        => OnLevelCompleted?.Invoke(levelCompletedData);

    public static void RaiseBoxDestroyed() => OnBoxDestroyed?.Invoke();

    #endregion

    #region handlers

    private void HandleLevelCompleted(LevelCompletedData data)
    {

        if (!SteamAchievements.IsUnlocked("ACH_FIRST_LEVEL") && data.levelIndex == 1) 
            SteamAchievements.UnlockAchievement("ACH_FIRST_LEVEL");

        if (!SteamAchievements.IsUnlocked("ACH_FIRST_LEVEL_THREE_STARS") && data.levelIndex == 1 && data.stars == 3) 
            SteamAchievements.UnlockAchievement("ACH_FIRST_LEVEL_THREE_STARS");

        if (!SteamAchievements.IsUnlocked("ACH_ALL_FIRST_WORLD") && data.levelIndex == 6)
            SteamAchievements.UnlockAchievement("ACH_ALL_FIRST_WORLD");

        if (!SteamAchievements.IsUnlocked("ACH_ALL_FIRST_WORLD_THREE_STARS") && data.levelIndex >= 1 && data.levelIndex <= 6
            && CheckRangeThreeStars(1, 6))
            SteamAchievements.UnlockAchievement("ACH_ALL_FIRST_WORLD_THREE_STARS");

        if (!SteamAchievements.IsUnlocked("ACH_ALL_SECOND_WORLD") && data.levelIndex == 12)
            SteamAchievements.UnlockAchievement("ACH_ALL_SECOND_WORLD");

        if (!SteamAchievements.IsUnlocked("ACH_ALL_SECOND_WORLD_THREE_STARS") && data.levelIndex >= 7 && data.levelIndex <= 12
            && CheckRangeThreeStars(7, 12))
            SteamAchievements.UnlockAchievement("ACH_ALL_SECOND_WORLD_THREE_STARS");

        if (!SteamAchievements.IsUnlocked("ACH_ALL_THIRD_WORLD") && data.levelIndex == 18)
            SteamAchievements.UnlockAchievement("ACH_ALL_THIRD_WORLD");

        if (!SteamAchievements.IsUnlocked("ACH_ALL_THIRD_WORLD_THREE_STARS") && data.levelIndex >= 12 && data.levelIndex <= 18
            && CheckRangeThreeStars(12, 18))
            SteamAchievements.UnlockAchievement("ACH_ALL_THIRD_WORLD_THREE_STARS");

        if (!SteamAchievements.IsUnlocked("ACH_ALL_FOURTH_WORLD") && data.levelIndex == 24)
            SteamAchievements.UnlockAchievement("ACH_ALL_FOURTH_WORLD");

        if (!SteamAchievements.IsUnlocked("ACH_ALL_FOURTH_WORLD_THREE_STARS") && data.levelIndex >= 18 && data.levelIndex <= 24
            && CheckRangeThreeStars(18, 24))
            SteamAchievements.UnlockAchievement("ACH_ALL_FOURTH_WORLD_THREE_STARS");

        if (!SteamAchievements.IsUnlocked("ACH_ALL_LEVELS") && data.levelIndex == 24)
            SteamAchievements.UnlockAchievement("ACH_ALL_LEVELS");

        if (!SteamAchievements.IsUnlocked("ACH_ALL_LEVELS_THREE_STARS") && data.levelIndex >= 1 && data.levelIndex <= 24
            && CheckRangeThreeStars(1, 24))
            SteamAchievements.UnlockAchievement("ACH_ALL_LEVELS_THREE_STARS");
    }

    private void HandleBoxDestroyed()
    {
        if (!SteamAchievements.IsUnlocked("ACH_BOX_DESTROYED"))
            SteamAchievements.UnlockAchievement("ACH_BOX_DESTROYED");
    }

    #endregion

    #region aux

    private bool CheckRangeThreeStars(int a, int b)
    {
        GameData data = PersistenceManager.Load();

        for (int i = a; i <= b; i++)
        {
            if (data.LevelStars[i] != 3)
            {
                print(data.LevelStars[i]);
                return false;
            }
        }

        return true;
    }

    #endregion
}
