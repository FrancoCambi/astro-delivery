using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverlayManager : MonoBehaviour
{
    private static OverlayManager instance;

    public static OverlayManager Instance
    {
        get
        {
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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    private void Start()
    {
        ResetStars();
    }

    public void OpenPause()
    {
        pauseGroup.alpha = 1;
        pauseGroup.blocksRaycasts = true;
    }

    public void OpenLost()
    {
        lostGroup.alpha = 1f;
        lostGroup.blocksRaycasts = true;
    }

    public void OpenWon(int starsAmount, float completedTime)
    {
        wonGroup.alpha = 1f;
        wonGroup.blocksRaycasts = true;

        for (int i = 0; i < starsAmount; i++)
        {
            stars[i].sprite = fullStar;
        }

        TimeSpan timeSpan = TimeSpan.FromSeconds(completedTime);

        String[] values = timeSpan.ToString(@"mm\:ss\:ff").Split(":");

        completedTimeText.text = $"{values[0]}:{values[1]}<size=25>:{values[2]}</size>";
    }

    public void ClosePause()
    {
        pauseGroup.alpha = 0;
        pauseGroup.blocksRaycasts = false;
    }

    public void CloseLost()
    {
        lostGroup.alpha = 0f;
        lostGroup.blocksRaycasts = false;
    }
    public void CloseWon()
    {
        wonGroup.alpha = 0f;
        wonGroup.blocksRaycasts = false;

        ResetStars();
    }

    private void ResetStars()
    {
        for (int i = 0; i < 3; i++)
        {
            stars[i].sprite = emptyStar;
        }
    }
}
