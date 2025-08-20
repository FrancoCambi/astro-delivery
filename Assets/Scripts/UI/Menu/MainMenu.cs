using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject mainMenuGO;
    [SerializeField] GameObject optionsGO;
    [SerializeField] GameObject levelsGO;
    [SerializeField] GameObject whishlistButtonGO;

    private void Start()
    {
        ShowWishlistButton();

        if (PlayerPrefs.GetInt("levelMenu", 0) == 1)
        {
            LevelSelection();
            PlayerPrefs.SetInt("levelMenu", 0);
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

    public void ExitButton()
    {
        Application.Quit();
    }

    private void ShowWishlistButton()
    {
#if DEMO_BUILD
        whishlistButtonGO.SetActive(true);
#endif
    }
}
