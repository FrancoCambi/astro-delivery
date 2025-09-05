using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISFXHandler : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Button[] defaultSoundButtons;
    [SerializeField] private AudioClip defaultSoundClip;
    [SerializeField] private Button[] pageArrowsButtons;
    [SerializeField] private AudioClip pageArrowsClip;
    [SerializeField] private TMP_Dropdown languagesDropdown;

    void Start()
    {
        foreach (Button btn in defaultSoundButtons)
        {
            btn.onClick.AddListener(PlayDefaultSound);
        }

        foreach (Button btn in pageArrowsButtons)
        {
            btn.onClick.AddListener(PlayPageArrowsSound);
        }

        languagesDropdown.onValueChanged.AddListener(PlayDefaultSoundDD);
    }

    private void PlayDefaultSound()
    {
        SoundFXManager.Instance.PlaySoundFXClip(defaultSoundClip, transform);
    }

    private void PlayDefaultSoundDD(int val)
    {
        if (val == PlayerPrefs.GetInt("old_language", 0)) return;

        SoundFXManager.Instance.PlaySoundFXClip(defaultSoundClip, transform);
    }

    private void PlayPageArrowsSound()
    {
        SoundFXManager.Instance.PlaySoundFXClip(pageArrowsClip, transform);
    }
}
