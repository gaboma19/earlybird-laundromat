using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SequenceTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    public void DisplayTime(float timeToDisplay)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);
        string formattedTime = $"{timeSpan.Seconds:D2}:{(timeSpan.Milliseconds / 10):D2}";

        timeText.text = formattedTime;
    }
}
