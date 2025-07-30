using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CanvasGroup mainMenuGroup;
    [SerializeField] CanvasGroup optionsGroup;

    public void PlayButton()
    {
        LevelManager.Instance.LoadLevel(LevelManager.Instance.CurrentLevel);
    }

    public void OptionsButton()
    {
        mainMenuGroup.alpha = 0f;
        mainMenuGroup.blocksRaycasts = false;

        optionsGroup.alpha = 1f;
        optionsGroup.blocksRaycasts = true;
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
