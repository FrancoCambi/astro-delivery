using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject mainMenuGO;
    [SerializeField] GameObject optionsGO;
    [SerializeField] GameObject levelsGO;

    private void Start()
    {
        if (PlayerPrefs.GetInt("levelMenu", 0) == 1)
        {
            LevelSelection();
            PlayerPrefs.SetInt("levelMenu", 0);
        }

        else if (PlayerPrefs.GetInt("wishMenu", 0) == 1)
        {
            WishListMenu();
            PlayerPrefs.SetInt("wishMenu", 0);
        }
    }

    public void LevelSelection()
    {
        mainMenuGO.SetActive(false);

        levelsGO.SetActive(true);
    }

    public void OptionsButton()
    {
        mainMenuGO.SetActive(false);

        optionsGO.SetActive(true);
    }

    public void WishListMenu()
    {
        LevelSelection();
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
