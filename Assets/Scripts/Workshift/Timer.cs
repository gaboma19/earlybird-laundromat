using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public float workshiftDuration = 160;
    float timeRemaining;
    public bool timerIsRunning = false;
    private bool isTimerStopped = false;
    [SerializeField] TextMeshProUGUI timeText;
    public static event Action OnTimerEnded;

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    public void StopTimer()
    {
        isTimerStopped = true;
    }

    private void SetWorkshiftDuration()
    {
        workshiftDuration = 80 * Calendar.instance.GetDate();
        timeRemaining = workshiftDuration;
        DisplayTime(timeRemaining);

        if (workshiftDuration > 300)
        {
            workshiftDuration = 300;
        }
    }

    void Start()
    {
        Apartment.OnDayStarted += SetWorkshiftDuration;

        timeRemaining = workshiftDuration;

        DisplayTime(timeRemaining);
    }

    void Update()
    {
        if (isTimerStopped)
        {
            isTimerStopped = false;

            timeRemaining = 0;
            timerIsRunning = false;
            OnTimerEnded.Invoke();
        }

        if (timerIsRunning && !isTimerStopped)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                DisplayTime(timeRemaining);
                timerIsRunning = false;
                OnTimerEnded.Invoke();
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
}
