using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumePercentageText : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI percentageText;


    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        percentageText.text = $"{Math.Round(slider.value * 100)}%";
    }
}