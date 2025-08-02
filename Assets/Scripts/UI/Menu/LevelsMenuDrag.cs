using UnityEngine;
using UnityEngine.EventSystems;

public class LevelsMenuDrag : MonoBehaviour, IEndDragHandler
{
    [Header("References")]
    [SerializeField] private LevelsMenu levelsMenu;

    private float dragThreshold;

    private void Awake()
    {
        dragThreshold = Screen.width / 15;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshold)
        {
            if (eventData.position.x > eventData.pressPosition.x) levelsMenu.Previous();
            else levelsMenu.Next();
        }
        else
        {
            levelsMenu.MovePage();
        }
    }
}
