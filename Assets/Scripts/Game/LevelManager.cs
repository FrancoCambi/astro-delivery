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

    private int maxLevelUnlocked;

    public void Start()
    {
        maxLevelUnlocked = PersistenceManager.Load().MaxLevelUnlocked;
    }

    public int MaxLevelUnlocked
    {
        get
        {
            return maxLevelUnlocked;
        }
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
        int starsWon = timer.GetStarsAmount();
        float completedTime = timer.GetCompletedTime();
        OverlayManager.Instance.OpenWon(starsWon, completedTime);

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
        if (nextLevel > GameConstants.MaxBetaLevels)
        {
            MenuManager.Instance.GoToWishListMenu();
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

    private void SetLevelRecord(int level, LevelRecord newRecord)
    {
        PersistenceManager.UpdateStarsInLevel(level, newRecord.Stars);
        PersistenceManager.UpdateMinTimeInLevel(level, newRecord.CompletedTime);

    }
    public void LoadLevel(int level)
    {
        PlayerPrefs.SetInt("playing_level", level);
        PlayerPrefs.Save();
        SceneManager.LoadScene(level);

    }
    private void UpdateMaxLevel()
    {
        if (PlayingLevel == MaxLevelUnlocked)
        {
            maxLevelUnlocked = PlayingLevel + 1;
            PersistenceManager.UpdateMaxLevelUnlocked(maxLevelUnlocked);
        }
    }

}
