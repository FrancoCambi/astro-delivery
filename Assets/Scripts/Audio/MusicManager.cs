using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    public static MusicManager Instance
    {
        get
        {
            instance = instance != null ? instance : FindAnyObjectByType<MusicManager>();
            return instance;
        }
    }

    [Header("References")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private LevelInfo levelInfo;

    private AudioClip musicClip;

    private void Start()
    {
        musicClip = levelInfo.MusicClip;
        musicSource.resource = musicClip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Pause();
    }

    public void StartMusic()
    {
        musicSource.UnPause();
    }
}
