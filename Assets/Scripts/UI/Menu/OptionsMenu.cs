using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CanvasGroup mainMenuGroup;
    [SerializeField] CanvasGroup optionsGroup;
    [SerializeField] CanvasGroup audioGroup;

    public void AudioButton()
    {
        optionsGroup.alpha = 0f;
        optionsGroup.blocksRaycasts = false;

        audioGroup.alpha = 1f;
        audioGroup.blocksRaycasts = true;
    }

    public void BackButton()
    {
        optionsGroup.alpha = 0f;
        optionsGroup.blocksRaycasts = false;

        mainMenuGroup.alpha = 1f;
        mainMenuGroup.blocksRaycasts = true;
    }
}
