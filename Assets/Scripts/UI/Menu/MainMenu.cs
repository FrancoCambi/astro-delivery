using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CanvasGroup mainMenuGroup;
    [SerializeField] CanvasGroup optionsGroup;
    [SerializeField] CanvasGroup levelsGroup;

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
        mainMenuGroup.alpha = 0f;
        mainMenuGroup.blocksRaycasts = false;

        levelsGroup.alpha = 1f;
        levelsGroup.blocksRaycasts = true;
    }

    public void OptionsButton()
    {
        mainMenuGroup.alpha = 0f;
        mainMenuGroup.blocksRaycasts = false;

        optionsGroup.alpha = 1f;
        optionsGroup.blocksRaycasts = true;
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
