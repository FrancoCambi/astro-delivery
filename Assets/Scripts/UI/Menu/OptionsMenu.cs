using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject mainMenuGO;
    [SerializeField] GameObject optionsGO;
    [SerializeField] GameObject audioGO;

    public void AudioButton()
    {
        optionsGO.SetActive(false);

        audioGO.SetActive(true);
    }

    public void BackButton()
    {
        optionsGO.SetActive(false);

        mainMenuGO.SetActive(true);
    }

  
}
