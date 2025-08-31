using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private static MenuManager instance;

    public static MenuManager Instance
    {
        get
        {
            instance = instance != null ? instance : FindAnyObjectByType<MenuManager>();
            return instance;
        }
    }

    [Header("References")]
    [SerializeField] private LevelsMenu levelsMenu;

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        LoadMainMenu();
    }

    public void GoToLevelMenu()
    {
        PlayerPrefs.SetInt("levelMenu", 1);
        GoToMainMenu();
    }

    public void GoToWishListMenu()
    {
        PlayerPrefs.SetInt("wishMenu", 1);
        GoToMainMenu();
    }
    private void LoadMainMenu()
    {
        PlayerPrefs.SetInt("playing_level", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }
}
