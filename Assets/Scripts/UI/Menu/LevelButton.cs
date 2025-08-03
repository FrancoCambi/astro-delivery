using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    [SerializeField] private int level;

    private Button btn;
    private TextMeshProUGUI levelText;
    private RectTransform rectTransform;

    private Vector3 upScale = new(1.2f, 1.2f, 1);

    private void Awake()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(StartLevel);
        levelText = GetComponentInChildren<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        if (level > LevelManager.Instance.MaxLevelUnlocked)
        {
            levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, 128);
            btn.interactable = false;
        }
    }

    private void StartLevel()
    {
        StartCoroutine(WaitForAnim());
    }
    private void Anim()
    {
        LeanTween.scale(gameObject, upScale, 0.1f);
        LeanTween.scale(gameObject, Vector3.one, 0.1f).setDelay(0.1f);
    }

    private IEnumerator WaitForAnim()
    {
        Anim();
        yield return new WaitForSeconds(0.15f);
        LevelManager.Instance.LoadLevel(level);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        LevelTooltip.Instance.ShowTooltip(level, rectTransform.localPosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LevelTooltip.Instance.HideTooltip();
    }
}
