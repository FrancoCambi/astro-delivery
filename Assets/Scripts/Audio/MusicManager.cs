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

    public void StopMusic()
    {
        musicSource.Pause();
    }

    public void StartMusic()
    {
        musicSource.UnPause();
    }
}
