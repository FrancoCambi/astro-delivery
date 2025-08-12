using UnityEngine;
using UnityEngine.UI;

public class AudioMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject optionsGO;
    [SerializeField] GameObject audioGO;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider soundFXSlider;
    [SerializeField] Slider musicSlider;

    private void OnEnable()
    {
        LoadVolumes();
    }

    public void BackButton()
    {
        audioGO.SetActive(false);


        optionsGO.SetActive(true);
    }

    private void LoadVolumes()
    {
        float masterVolume = PlayerPrefs.GetFloat("masterVolume");
        float soundFXVolume = PlayerPrefs.GetFloat("soundFXVolume");
        float musicVolume = PlayerPrefs.GetFloat("musicVolume");

        float masterVolumeNormalized = Mathf.Pow(10, (masterVolume / (float)20));
        float soundFxVolumeNormalized = Mathf.Pow(10, (soundFXVolume / (float)20));
        float musicVolumeNormalized = Mathf.Pow(10, (musicVolume / (float)20));

        // When sliders' values are changed, the OnValueChanged event of the slider
        // sraises, calling the set functions.
        masterSlider.value = masterVolumeNormalized;
        soundFXSlider.value = soundFxVolumeNormalized;
        musicSlider.value = musicVolumeNormalized;
    }
}
