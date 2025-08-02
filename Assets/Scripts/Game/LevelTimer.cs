using System;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Settings")]
    [SerializeField] private float maxSeconds;

    private bool start = false;
    private float secondsRemaining;

    private void Awake()
    {
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
        float baseTime = maxSeconds / 2.5f;
        float completedTime = maxSeconds - secondsRemaining;

        if (0 <= completedTime && completedTime <= baseTime * 1.25f)
        {
            return 3;
        }
        else if (baseTime * 1.25f < completedTime && completedTime <= baseTime * 2f)
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
