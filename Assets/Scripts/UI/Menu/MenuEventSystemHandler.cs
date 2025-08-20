using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem;
using System.Collections;

public class MenuEventSystemHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected List<Selectable> selectables = new();
    [SerializeField] protected Selectable firstSelected;

    [Header("Controls")]
    [SerializeField] protected InputActionReference navigateReference;

    [Header("Animations")]
    [SerializeField] protected float selectedAnimationScale = 1.1f;
    [SerializeField] protected float scaleDuration = 0.25f;

    protected Dictionary<Selectable, Vector3> scales = new();

    protected Tween scaleUpTween;
    protected Tween scaleDownTween;
    protected Selectable lastSelected;

    public virtual void Awake()
    {
        foreach (Selectable selectable in selectables)
        {
            AddSelectionListeners(selectable);
            scales.Add(selectable, selectable.transform.localScale);
        }
    }

    public virtual void OnEnable()
    {
        navigateReference.action.performed += OnNavigate;

        for (int i = 0; i < selectables.Count; i++)
        {
            selectables[i].transform.localScale = scales[selectables[i]];
        }

        StartCoroutine(SelectAfterDelay());
    }

    protected virtual IEnumerator SelectAfterDelay()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(firstSelected.gameObject);
    }

    public virtual void OnDisable()
    {
        navigateReference.action.performed -= OnNavigate;

        scaleUpTween.Kill(true);
        scaleDownTween.Kill(true);
    }

    protected virtual void AddSelectionListeners(Selectable selectable)
    {
        EventTrigger trigger = selectable.gameObject.GetComponent<EventTrigger>();

        if (trigger == null )
        {
            trigger = selectable.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry selectEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.Select,
        };
        selectEntry.callback.AddListener(OnSelect);
        trigger.triggers.Add(selectEntry);

        EventTrigger.Entry deselectEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.Deselect,
        };
        deselectEntry.callback.AddListener(OnDeselect);
        trigger.triggers.Add(deselectEntry);

        EventTrigger.Entry pointerEnter = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter,
        };
        pointerEnter.callback.AddListener(OnPointerEnter);
        trigger.triggers.Add(pointerEnter);

        EventTrigger.Entry pointerExit = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit,
        };
        pointerExit.callback.AddListener(OnPointerExit);
        trigger.triggers.Add(pointerExit);

    }

    public virtual void OnSelect(BaseEventData eventData)
    {
        lastSelected = eventData.selectedObject.GetComponent<Selectable>();

        Vector3 newScale = eventData.selectedObject.transform.localScale * selectedAnimationScale;
        scaleUpTween = eventData.selectedObject.transform.DOScale(newScale, scaleDuration).SetUpdate(true);
    }

    public virtual void OnDeselect(BaseEventData eventData)
    {
        Selectable sel = eventData.selectedObject.GetComponent<Selectable>();
        scaleDownTween = eventData.selectedObject.transform.DOScale(scales[sel], scaleDuration).SetUpdate(true);

    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        if (pointerEventData != null)
        {
            Selectable sel = pointerEventData.pointerEnter.GetComponentInParent<Selectable>();
            if (sel == null)
            {
                sel = pointerEventData.pointerEnter.GetComponentInChildren<Selectable>();
            }
            pointerEventData.selectedObject = sel.gameObject;
        }
    }

    public void OnPointerExit(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        if (pointerEventData != null)
        {
            pointerEventData.selectedObject = null;
        }

    }

    protected virtual void OnNavigate(InputAction.CallbackContext context)
    {
        if (EventSystem.current.currentSelectedGameObject == null && lastSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelected.gameObject);
        }
    }


}
