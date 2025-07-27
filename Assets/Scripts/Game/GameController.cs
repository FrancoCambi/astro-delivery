using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance;

    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }

    public static int CurrentLevel { get; private set; }

    public static int LastLevelIndex { get; private set; }

    public static KeyCode InteractKey = KeyCode.E;
    public static string InteractKeyString => InteractKey.ToString();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        CurrentLevel = PlayerPrefs.GetInt("level", 1);
        LastLevelIndex = SceneManager.sceneCountInBuildSettings - 1;

    }

    private void Start()
    {
        LoadLevel(CurrentLevel);
    }

    public void AdvanceToNextLevel()
    {
        CurrentLevel++;
        print(CurrentLevel);
        PlayerPrefs.SetInt("level", CurrentLevel);

        if (CurrentLevel <= LastLevelIndex)
        {
            LoadLevel(CurrentLevel);    
        }
        else
        {
            GameFinished();
        }

    }

    private void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    private void GameFinished()
    {
        print("You Won!");
    }
}
