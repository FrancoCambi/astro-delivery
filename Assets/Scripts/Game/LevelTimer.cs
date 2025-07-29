using System;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Settings")]
    [SerializeField] private float seconds;

    private bool start = false;

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

        seconds -= Time.deltaTime;

        UpdateText();

        if (seconds <= 0)
        {
            GameController.Instance.LoseLevel();
        }
    }

    private void UpdateText()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);

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
