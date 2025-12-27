using Steamworks;
using Steamworks.Data;
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

    public KeyCode InteractKey => KeyCode.E;
    public string InteractKeyString => InteractKey.ToString();
    public bool IsPaused { get; private set; } 
    private void Awake()
    {
        Time.timeScale = 1;
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
            else if (IsPaused && OverlayManager.Instance.IsPauseOpen)
            {
                Unpause();
            }
        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            SteamUserStats.ResetAll(true);
        }

        /*if (Input.GetKeyDown(KeyCode.J))
        {
            SteamAchievements.UnlockAchievement("ACH_TEST");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            print(SteamAchievements.IsUnlocked("ACH_FIRST_LEVEL"));

        }*/
    }

    public void ResetLevel()
    {
        LevelManager.Instance.LoadLevel(LevelManager.Instance.PlayingLevel);
        MusicManager.Instance.PlayMusic();
    }

    public void LoseLevel()
    {
        if (IsPaused) return;

        Pause(false);
        LevelManager.Instance.LoseLevel();
    }

    public void WinLevel()
    {
        Pause(false);
        LevelManager.Instance.WinLevel();
    }

    private void Pause(bool overlay)
    {
        if (overlay) OverlayManager.Instance.OpenPause();
        Time.timeScale = 0;
        IsPaused = true;
        MusicManager.Instance.PauseMusic();
    }

    private void Unpause()
    {
        OverlayManager.Instance.ClosePause();
        Time.timeScale = 1;
        IsPaused = false;
        MusicManager.Instance.UnPauseMusic();
    }
}
