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
        GoToMainMenu();
        PlayerPrefs.SetInt("levelMenu", 1);
        
    }
}
