using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int CurrentLevel { get; private set; }
    public int LastLevelIndex { get; private set; }

    private void Awake()
    {
        CurrentLevel = PlayerPrefs.GetInt("level", 1);
        LastLevelIndex = SceneManager.sceneCountInBuildSettings - 1;
    }
    public void AdvanceToNextLevel()
    {
        CurrentLevel++;
        PlayerPrefs.SetInt("level", CurrentLevel);

        if (CurrentLevel <= LastLevelIndex)
        {
            LoadLevel(CurrentLevel);
        }

    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
