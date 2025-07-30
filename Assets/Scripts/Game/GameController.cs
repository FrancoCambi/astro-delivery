using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance;

    public static GameController Instance
    {
        get
        {
            instance = instance != null ? instance : FindAnyObjectByType<GameController>();
            return instance;
        }
    }

    [Header("References")]
    [SerializeField] private LevelTimer timer;
    public KeyCode InteractKey => KeyCode.E;
    public string InteractKeyString => InteractKey.ToString();
    public bool IsPaused { get; private set; } 

    private void Awake()
    {
        PlayerPrefs.SetInt("level", 1);
        Unpause();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsPaused)
            {
                Pause(true);
            }
            else
            {
                Unpause();
            }
        }
    }

    public void ResetLevel()
    {
        LevelManager.Instance.LoadLevel(LevelManager.Instance.CurrentLevel);
    }

    public void GoToMainMenu()
    {
        LevelManager.Instance.LoadLevel(0);
    }

    public void GoToLevelMenu()
    {

    }

    public void LoseLevel()
    {
        Pause(false);
        OverlayManager.Instance.OpenLost();
    }

    public void WinLevel()
    {
        Pause(false);
        int starsWon = timer.GetStarsAmount();
        float completedTime = timer.GetCompletedTime();
        OverlayManager.Instance.OpenWon(starsWon, completedTime);
    }

    private void Pause(bool overlay)
    {
        if (overlay) OverlayManager.Instance.OpenPause();
        Time.timeScale = 0;
        IsPaused = true;
    }

    private void Unpause()
    {
        OverlayManager.Instance.ClosePause();
        Time.timeScale = 1;
        IsPaused = false;
    }

    private void GameFinished()
    {
        print("You Won!");
    }
}
