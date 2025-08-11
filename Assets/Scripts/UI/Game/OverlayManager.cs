using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

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
    [SerializeField] private CanvasGroup pauseGroup;
    [SerializeField] private CanvasGroup lostGroup;
    [SerializeField] private CanvasGroup wonGroup;
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
    public bool IsPauseOpen => pauseGroup.alpha == 1;

    private void Start()
    {
        mainCamera = Camera.main;
        ResetStars();
    }

    public void OpenPause()
    {
        pauseGroup.alpha = 1;
        pauseGroup.blocksRaycasts = true;
        pauseTransform.DOLocalMove(Vector3.zero, tweenTime).SetEase(ease).SetUpdate(true);

    }

    public void OpenLost()
    {
        lostGroup.alpha = 1f;
        lostGroup.blocksRaycasts = true;
        lostTransform.DOLocalMove(Vector3.zero, tweenTime).SetEase(ease).SetUpdate(true);

        SoundFXManager.Instance.PlaySoundFXClip(lostClip, transform);

    }

    public void OpenWon(int starsAmount, float completedTime)
    {
        wonGroup.alpha = 1f;
        wonGroup.blocksRaycasts = true;
        wonTransform.DOLocalMove(Vector3.zero, tweenTime).SetEase(ease).SetUpdate(true);

        for (int i = 0; i < starsAmount; i++)
        {
            stars[i].sprite = fullStar;
        }

        TimeSpan timeSpan = TimeSpan.FromSeconds(completedTime);

        String[] values = timeSpan.ToString(@"mm\:ss\:ff").Split(":");

        completedTimeText.text = $"{values[0]}:{values[1]}<size=25>:{values[2]}</size>";

        SoundFXManager.Instance.PlaySoundFXClip(wonClip, transform);



    }

    public void ClosePause()
    {
        pauseTransform.DOLocalMove(GetOLDefaultPos(), tweenTime).SetEase(ease).SetUpdate(true);
        //pauseGroup.alpha = 0;
        //pauseGroup.blocksRaycasts = false;


    }

    public void CloseLost()
    {
        //lostGroup.alpha = 0f;
        //lostGroup.blocksRaycasts = false;

    }
    public void CloseWon()
    {
        //wonGroup.alpha = 0f;
        //wonGroup.blocksRaycasts = false;

        ResetStars();
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
