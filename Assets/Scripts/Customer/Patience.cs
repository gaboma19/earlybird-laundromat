using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Patience : MonoBehaviour
{
    FillBar fillBar;
    [SerializeField] Face face;
    [SerializeField] float patienceDuration = 60f;
    float patienceRemaining;
    bool isPatienceRunning = false;
    public static event Action OnPatienceEnded;

    void Start()
    {
        fillBar = gameObject.GetComponent<FillBar>();

        patienceRemaining = patienceDuration;
    }

    void Update()
    {
        if (isPatienceRunning)
        {
            if (patienceRemaining > 0)
            {
                patienceRemaining -= Time.deltaTime;
                DisplayPatience();
            }
            else
            {
                isPatienceRunning = false;
                OnPatienceEnded.Invoke();
            }
        }
    }

    private void DisplayPatience()
    {
        float fillAmount = (patienceDuration - patienceRemaining) / patienceDuration;
        fillBar.SetFillBar(fillAmount);
        face.SetFace(fillAmount);
    }
}
