using UnityEngine;

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

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        LevelManager.Instance.LoadLevel(0);
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
}
