using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CanvasGroup optionsGroup;
    [SerializeField] CanvasGroup audioGroup;

    public void BackButton()
    {
        audioGroup.alpha = 0f;
        audioGroup.blocksRaycasts = false;

        optionsGroup.alpha = 1f;
        optionsGroup.blocksRaycasts = true;
    }
}
