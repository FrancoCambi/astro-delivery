using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{

    private static SoundMixerManager instance;

    public static SoundMixerManager Instance
    {
        get
        {
            instance = instance != null ? instance : FindAnyObjectByType<SoundMixerManager>();
            return instance;
        }
    }

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider soundFXSlider;
    [SerializeField] Slider musicSlider;

    private void Start()
    {
        LoadVolumes();
    }

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);
        SaveMasterVolume();
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("SoundFXVolume", Mathf.Log10(level) * 20f);
        SaveSoundFXVolume();
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
        SaveMusicVolume();
    }

    public void SetSoundFXPitch(float level)
    {
        audioMixer.SetFloat("SoundFXPitch", level);
        audioMixer.GetFloat("SoundFXPitch", out float pitch);
    }

    private void SaveMasterVolume()
    {
        audioMixer.GetFloat("MasterVolume", out float masterVolume);
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.Save();

    }

    private void SaveSoundFXVolume()
    {
        audioMixer.GetFloat("SoundFXVolume", out float soundFXVolume);
        PlayerPrefs.SetFloat("soundFXVolume", soundFXVolume);
        PlayerPrefs.Save();

    }

    private void SaveMusicVolume()
    {
        audioMixer.GetFloat("MusicVolume", out float musicVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.Save();
    }

    public void LoadVolumes()
    {
        float masterVolume = PlayerPrefs.GetFloat("masterVolume");
        float soundFXVolume = PlayerPrefs.GetFloat("soundFXVolume");
        float musicVolume = PlayerPrefs.GetFloat("musicVolume");

        audioMixer.SetFloat("MasterVolume", masterVolume);
        audioMixer.SetFloat("SoundFXVolume", soundFXVolume);
        audioMixer.SetFloat("MusicVolume", musicVolume);
       
    }
}
