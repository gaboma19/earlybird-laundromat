using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calendar : MonoBehaviour
{
    public static Calendar instance;
    private int date = 1;
    [SerializeField] TextMeshProUGUI calendarText;

    public void IncrementDate()
    {
        date++;
    }

    public int GetDate()
    {
        return date;
    }

    void Update()
    {
        calendarText.text = "Day " + date;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        Apartment.OnDayStarted += IncrementDate;
    }
}
