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

    public int MaxLevelUnlocked
    {
        get
        {
            return PlayerPrefs.GetInt("max_level_unlocked", 1);
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
        return new LevelRecord
        {
            Stars = PlayerPrefs.GetInt($"record_stars_{level}", 0),
            CompletedTime = PlayerPrefs.GetFloat($"record_time_{level}", Mathf.Infinity)
        };
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
            PlayerPrefs.SetInt("max_level_unlocked", MaxLevelUnlocked + 1);
            PlayerPrefs.Save();

        }
    }
    private void SetLevelRecord(int level, LevelRecord newRecord)
    {
        PlayerPrefs.SetInt($"record_stars_{level}", newRecord.Stars);
        PlayerPrefs.SetFloat($"record_time_{level}", newRecord.CompletedTime);
        PlayerPrefs.Save();


    }

}
