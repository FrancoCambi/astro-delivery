using UnityEngine;
using UnityEngine.UI;

public class LevelPage : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button[] levelsButtons;

    public void MakeInteractable()
    {
        foreach (Button btn in levelsButtons)
        {
            btn.interactable = true;
            btn.GetComponent<LevelButton>().SetInteractable();
        }
    }

    public void MakeNotInteractable()
    {
        foreach (Button btn in levelsButtons)
        {
            btn.interactable = false;
            btn.GetComponent<LevelButton>().SetInteractable();
        }
    }
}
