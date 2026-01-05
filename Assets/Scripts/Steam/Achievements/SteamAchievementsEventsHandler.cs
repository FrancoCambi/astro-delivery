using System;
using UnityEngine;

public struct LevelCompletedData
{
    public int levelIndex;
    public int stars;
    public float time;
    public int attempts;
}

public class SteamAchievementsEventsHandler : MonoBehaviour
{

    #region events

    public static event Action<LevelCompletedData> OnLevelCompleted;

    public static event Action OnBoxDestroyed;

    public static event Action OnDeath;

    #endregion

    #region aux_var

    private bool packageLost = false;

    private bool stoppedMovement = false;

    #endregion
    private void OnEnable()
    {
        OnLevelCompleted += HandleLevelCompleted;
        OnBoxDestroyed += HandleBoxDestroyed;
        OnDeath += HandleDeath;

        PlayerMovement.OnStopped += StoppedMovement;
        GameController.OnRetry += AddRetry;
        OverlayManager.OnMenu += ResetRetries;
    }

    private void OnDisable()
    {
        OnLevelCompleted -= HandleLevelCompleted;
        OnBoxDestroyed -= HandleBoxDestroyed;
        OnDeath -= HandleDeath;

        PlayerMovement.OnStopped -= StoppedMovement;
        GameController.OnRetry -= AddRetry;
        OverlayManager.OnMenu -= ResetRetries;

    }

    #region Raisers

    public static void RaiseLevelCompleted(LevelCompletedData levelCompletedData)
        => OnLevelCompleted?.Invoke(levelCompletedData);

    public static void RaiseBoxDestroyed() => OnBoxDestroyed?.Invoke();

    public static void RaiseDeath() => OnDeath?.Invoke();

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

        if (!SteamAchievements.IsUnlocked("ACH_LOST_PACKAGE_WON") && packageLost && data.stars == 3)
            SteamAchievements.UnlockAchievement("ACH_LOST_PACKAGE_WON");

        if (!SteamAchievements.IsUnlocked("ACH_WON_AFTER_10") && data.attempts > 10)
            SteamAchievements.UnlockAchievement("ACH_WON_AFTER_10");

        if (!SteamAchievements.IsUnlocked("ACH_WON_AFTER_10") && data.attempts > 25)
            SteamAchievements.UnlockAchievement("ACH_WON_AFTER_25");

        if (!SteamAchievements.IsUnlocked("ACH_WON_AFTER_10") && data.attempts > 50)
            SteamAchievements.UnlockAchievement("ACH_WON_AFTER_50");

        if (!SteamAchievements.IsUnlocked("ACH_WON_AFTER_10") && data.attempts > 100)
            SteamAchievements.UnlockAchievement("ACH_WON_AFTER_100");

        if (!SteamAchievements.IsUnlocked("ACH_WON_AFTER_10") && data.attempts > 200)
            SteamAchievements.UnlockAchievement("ACH_WON_AFTER_200");

        if (!SteamAchievements.IsUnlocked("ACH_NON_STOP") && !stoppedMovement)
            SteamAchievements.UnlockAchievement("ACH_NON_STOP");

        if (!SteamAchievements.IsUnlocked("ACH_UNDER_10_SECONDS") && data.time < 10)
            SteamAchievements.UnlockAchievement("ACH_UNDER_10_SECONDS");

        packageLost = false;
    }

    private void HandleBoxDestroyed()
    {

        if (!SteamAchievements.IsUnlocked("ACH_BOX_DESTROYED"))
            SteamAchievements.UnlockAchievement("ACH_BOX_DESTROYED");

        packageLost = true;
    }

    private void HandleDeath()
    {
        if (!SteamAchievements.IsUnlocked("ACH_FIRST_DEATH"))
            SteamAchievements.UnlockAchievement("ACH_FIRST_DEATH");
    }

    private void HandleRetried()
    {
        int rowRetries = PlayerPrefs.GetInt("rowRetries", 0);

        if (!SteamAchievements.IsUnlocked("ACH_25_RESET_NO_MENU") && rowRetries >= 25)
            SteamAchievements.UnlockAchievement("ACH_25_RESET_NO_MENU");
    }

    #endregion

    #region aux_functions

    private bool CheckRangeThreeStars(int a, int b)
    {
        GameData data = PersistenceManager.Load();

        for (int i = a; i <= b; i++)
        {
            if (data.LevelStars[i] != 3)
            {
                return false;
            }
        }

        return true;
    }

    private void StoppedMovement()
    {
        stoppedMovement = true;
    }

    private void AddRetry(int _)
    {
        PlayerPrefs.SetInt("rowRetries", PlayerPrefs.GetInt("rowRetries", 0) + 1);

        HandleRetried();
    }

    private void ResetRetries()
    {
        PlayerPrefs.SetInt("rowRetries", 0);
    }

    private void OnApplicationQuit()
    {
        ResetRetries();
    }

    #endregion
}
