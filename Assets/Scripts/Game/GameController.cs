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
        LevelManager.Instance.LoadLevel(LevelManager.Instance.CurrentLevel);
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

        LevelManager.Instance.WinCurrentLevel();
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
