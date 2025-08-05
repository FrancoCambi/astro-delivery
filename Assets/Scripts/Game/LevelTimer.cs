using System;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Settings")]
    [SerializeField] private float minTime;
    [SerializeField] private float normalTime;

    private bool start = false;
    private float secondsRemaining;
    private float maxSeconds;

    private void Awake()
    {
        maxSeconds = normalTime * 2.5f;
        secondsRemaining = maxSeconds;
    }

    private void OnEnable()
    {
        PlayerMovement.OnFirstMove += StartTimer;
    }

    private void Start()
    {
        UpdateText();
    }

    private void Update()
    {
        if (!start) return;

        secondsRemaining -= Time.deltaTime;

        UpdateText();

        if (secondsRemaining <= 0)
        {
            GameController.Instance.LoseLevel();
            start = false;
        }
    }

    public int GetStarsAmount()
    {
        float threeStarsMax = minTime * 1.25f;
        float twoStarsMax = normalTime * 2;
        float completedTime = maxSeconds - secondsRemaining;

        if (0 <= completedTime && completedTime <= threeStarsMax)
        {
            return 3;
        }
        else if (threeStarsMax < completedTime && completedTime <= twoStarsMax)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    public float GetCompletedTime()
    {
        float completedTime = maxSeconds - secondsRemaining;

        return completedTime;
    }

    private void UpdateText()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(secondsRemaining);

        String[] values = timeSpan.ToString(@"mm\:ss\:ff").Split(":");

        timerText.text = $"{values[0]}:{values[1]}<size=30>:{values[2]}</size>";
    }

    private void OnDisable()
    {
        PlayerMovement.OnFirstMove -= StartTimer;
    }

    private void StartTimer()
    {
        start = true;
    }
}
