using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class SettingsManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_Dropdown dropdown;

    private void Awake()
    {
        StartCoroutine(LoadLanguage());
    }

    public void SwitchLanguage(int value)
    {

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[value];

        SaveLanguage(value);
    }

    private void SaveLanguage(int value)
    {
        PlayerPrefs.SetInt("language", value);
        PlayerPrefs.Save();
    }

    private IEnumerator LoadLanguage()
    {
        yield return LocalizationSettings.InitializationOperation;

        int languageIndex = PlayerPrefs.GetInt("language", 0);
        SwitchLanguage(languageIndex);

        dropdown.value = languageIndex;

    }
}
