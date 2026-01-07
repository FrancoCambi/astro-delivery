using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class OverlayManager : MonoBehaviour
{
    private static OverlayManager instance;

    public static OverlayManager Instance
    {
        get
        {
            instance = instance != null ? instance : FindAnyObjectByType<OverlayManager>();
            return instance;
        }
    }

    [Header("References")]
    [SerializeField] private GameObject pauseGO;
    [SerializeField] private GameObject lostGO;
    [SerializeField] private GameObject wonGO;
    [SerializeField] private TextMeshProUGUI completedTimeText;
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite emptyStar;
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Transform pauseTransform;
    [SerializeField] private Transform wonTransform;
    [SerializeField] private Transform lostTransform;

    [Header("Settings")]
    [SerializeField] private float tweenTime;
    [SerializeField] private Ease ease;
    [SerializeField] private Vector3 startPos;

    [Header("Audio")]
    [SerializeField] private AudioClip lostClip;
    [SerializeField] private AudioClip wonClip;

    private Camera mainCamera;

    private bool lost = false;
    public bool IsPauseOpen { get; private set; }

    public static event Action OnMenu;

    private void Start()
    {
        mainCamera = Camera.main;
        ResetStars();
    }

    public void OpenPause()
    {
        pauseGO.SetActive(true);
        pauseTransform.DOLocalMove(Vector3.zero, tweenTime).SetEase(ease).SetUpdate(true);
        IsPauseOpen = true;

    }

    public void OpenLost()
    {
        lostGO.SetActive(true);
        lostTransform.DOLocalMove(Vector3.zero, tweenTime).SetEase(ease).SetUpdate(true);

        SoundFXManager.Instance.PlaySoundFXClip(lostClip, transform);

        lost = true;

    }

    public void OpenWon(int starsAmount, float completedTime)
    {
        wonGO.SetActive(true);
        wonTransform.DOLocalMove(Vector3.zero, tweenTime).SetEase(ease).SetUpdate(true);

        for (int i = 0; i < starsAmount; i++)
        {
            stars[i].sprite = fullStar;
        }

        TimeSpan timeSpan = TimeSpan.FromSeconds(completedTime);

        String[] values = timeSpan.ToString(@"mm\:ss\:ff").Split(":");

        completedTimeText.text = $"{values[0]}:{values[1]}<size=30>:{values[2]}</size>";

        SoundFXManager.Instance.PlaySoundFXClip(wonClip, transform);

    }

    public void ClosePause()
    {
        StartCoroutine(ClosePauseIE());
    }

    public void CloseLost()
    {
        lostGO.SetActive(false);
    }
    public void CloseWon()
    {
        wonGO.SetActive(false);

        ResetStars();
    }

    public void GoToMainMenu()
    {
        if (lost)
            GameController.Instance.LevelRetried(LevelManager.Instance.PlayingLevel, false);

        Time.timeScale = 1;
        LoadMainMenu();
    }

    public void GoToLevelMenu()
    {
        PlayerPrefs.SetInt("levelMenu", 1);
        GoToMainMenu();
    }
    private void LoadMainMenu()
    {
        PlayerPrefs.SetInt("playing_level", 0);
        PlayerPrefs.Save();
        OnMenu?.Invoke();
        SceneManager.LoadScene(0);
        MusicManager.Instance.PlayMusic();
    }
    private IEnumerator ClosePauseIE()
    {
        pauseTransform.DOLocalMove(GetOLDefaultPos(), tweenTime).SetEase(ease).SetUpdate(true);
        IsPauseOpen = false;
        yield return new WaitForSeconds(tweenTime);
        pauseGO.SetActive(false);
    }

    private void ResetStars()
    {
        for (int i = 0; i < 3; i++)
        {
            stars[i].sprite = emptyStar;
        }
    }

    private Vector3 GetOLDefaultPos() => new(mainCamera.transform.position.x, mainCamera.transform.position.y - 900, 0);

}
