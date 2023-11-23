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
    [SerializeField] TextMeshProUGUI timeText;
    public static event Action OnTimerEnded;

    public void ResetTimer()
    {
        timeRemaining = workshiftDuration;
    }

    private void SetWorkshiftDuration()
    {
        workshiftDuration = 80 * Calendar.instance.GetDate();

        if (workshiftDuration > 300)
        {
            workshiftDuration = 300;
        }
    }

    void Start()
    {
        Exit.OnDayEnded += SetWorkshiftDuration;

        timeRemaining = workshiftDuration;
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                OnTimerEnded.Invoke();
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
}
