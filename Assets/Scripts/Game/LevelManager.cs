using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct LevelRecord
{
    public int Stars;
    public float CompletedTime;
}
public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    public static LevelManager Instance
    {
        get
        {
            instance = instance != null ? instance : FindAnyObjectByType<LevelManager>();
            return instance;
        }
    }

    [Header("References")]
    [SerializeField] private LevelTimer timer;
    [SerializeField] private LevelInfo levelInfo;

    private int maxLevelUnlocked;

    public LevelInfo LevelInfo
    {
        get
        {
            return levelInfo;
        }
    }

    public void Start()
    {
        maxLevelUnlocked = PersistenceManager.Load().MaxLevelUnlocked;
    }

    public int PlayingLevel
    {
        get
        {
            return PlayerPrefs.GetInt("playing_level", 1);
        }
    }
    public int LastLevelIndex
    {
        get
        {
            return SceneManager.sceneCountInBuildSettings - 1;
        }
    }

    public void WinLevel()
    {
        GameData data = PersistenceManager.Load();
        float completedTime = timer.GetCompletedTime();
        int starsWon = GetStarsAmount(completedTime);
        OverlayManager.Instance.OpenWon(starsWon, completedTime);

        SteamAchievementsEventsHandler.RaiseLevelCompleted(new LevelCompletedData
        {
            levelIndex = PlayingLevel,
            stars = starsWon,
            time = completedTime,
            attempts = data.LevelAttempts[PlayingLevel]
        });

        UpdateMaxLevel();
        UpdateRecord(starsWon, completedTime);
    }

    public void LoseLevel()
    {
        OverlayManager.Instance.OpenLost();
    }

    public void PlayNextLevel()
    {
        int nextLevel = PlayingLevel + 1;
#if DEMO_BUILD
        if (nextLevel > GameConstants.DemoLevels)
        {
            LoadLevel(0);
            return;
        }
#endif
        if (nextLevel <= LastLevelIndex)
        {
            LoadLevel(nextLevel);
        }
    }

    public void UpdateRecord(int starsWon, float completedTime)
    {
        LevelRecord currentRecord = GetLevelRecord(PlayingLevel);
        LevelRecord newRecord = currentRecord;

        if (starsWon > currentRecord.Stars)
        {
            newRecord.Stars = starsWon;
        }

        if (completedTime < currentRecord.CompletedTime)
        {
            newRecord.CompletedTime = completedTime;
        }

        SetLevelRecord(PlayingLevel, newRecord);
    }

    public LevelRecord GetLevelRecord(int level)
    {
        GameData data = PersistenceManager.Load();

        int stars = data.LevelStars[level];
        float completedTime = data.MinTimes[level];

        return new LevelRecord
        {
            Stars = stars,
            CompletedTime = completedTime,
        };
    }

    public int GetStarsAmount(float completedTime)
    {
        float threeStarsMax = levelInfo.SpecialTimeCap;
        float twoStarsMax = levelInfo.NormalTimeCap;

        if (0 <= completedTime && completedTime <= threeStarsMax)
        {
            return 3;
        }
        else if (threeStarsMax < completedTime && completedTime <= twoStarsMax)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
    public void LoadLevel(int level)
    {
        PlayerPrefs.SetInt("playing_level", level);
        PlayerPrefs.Save();
        SceneManager.LoadScene(level);
        MusicManager.Instance.PlayMusic();

    }

    private void SetLevelRecord(int level, LevelRecord newRecord)
    {
        PersistenceManager.UpdateStarsInLevel(level, newRecord.Stars);
        PersistenceManager.UpdateMinTimeInLevel(level, newRecord.CompletedTime);

    }
    private void UpdateMaxLevel()
    {
        if (PlayingLevel == maxLevelUnlocked)
        {
            maxLevelUnlocked = PlayingLevel + 1;
            PersistenceManager.UpdateMaxLevelUnlocked(maxLevelUnlocked);
        }
    }


}
