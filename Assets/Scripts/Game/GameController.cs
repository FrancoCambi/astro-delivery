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
            else if (IsPaused && OverlayManager.Instance.IsPauseOpen)
            {
                Unpause();
            }
        }
    }

    public void ResetLevel()
    {
        LevelManager.Instance.LoadLevel(LevelManager.Instance.PlayingLevel);
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
        MusicManager.Instance.StopMusic();
    }

    private void Unpause()
    {
        OverlayManager.Instance.ClosePause();
        Time.timeScale = 1;
        IsPaused = false;
        MusicManager.Instance.StartMusic();
    }
}
