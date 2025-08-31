using System;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI timerText;

    private bool start = false;
    private float currentSeconds;
    private bool playing = true;

    private void Awake()
    {
        currentSeconds = 0f;
    }

    private void OnEnable()
    {
        PlayerMovement.OnFirstMove += StartTimer;
        Package.OnGrabbed += StartTimer;
    }

    private void Start()
    {
        UpdateText();
    }

    private void Update()
    {
        if (!start || !playing) return;

        currentSeconds += Time.deltaTime;

        UpdateText();

        if (currentSeconds >= 3600)
        {
            GameController.Instance.LoseLevel();
            start = false;
        }
    }

    public float GetCompletedTime()
    {
        playing = false;
        return currentSeconds;
    }

    private void UpdateText()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentSeconds);

        String[] values = timeSpan.ToString(@"mm\:ss\:ff").Split(":");

        timerText.text = $"{values[0]}:{values[1]}<size=30>:{values[2]}</size>";
    }

    private void OnDisable()
    {
        PlayerMovement.OnFirstMove -= StartTimer;
        Package.OnGrabbed -= StartTimer;
    }

    private void StartTimer()
    {
        start = true;
    }
}
