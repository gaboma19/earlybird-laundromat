using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patience : MonoBehaviour
{
    FillBar fillBar;
    [SerializeField] Face face;
    [SerializeField] float patienceDuration = 120f;
    float patienceRemaining;
    bool isPatienceRunning = false;
    public bool hasPatienceEnded = false;
    public float fillAmount;
    [SerializeField] float patiencePenalty = 20f;

    public void StartPatienceMeter()
    {
        gameObject.SetActive(true);
        isPatienceRunning = true;
    }

    public void SetDone()
    {
        isPatienceRunning = false;
        patienceRemaining = patienceDuration;
        DisplayPatience();
    }
    public void SetDiscard()
    {
        isPatienceRunning = false;
        patienceRemaining = 0;
        DisplayPatience();
    }

    public void ApplyPenalty()
    {
        patienceRemaining -= patiencePenalty;
    }

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
                hasPatienceEnded = true;
            }
        }
    }

    private void DisplayPatience()
    {
        fillAmount = (patienceDuration - patienceRemaining) / patienceDuration;
        fillBar.SetFillBar(fillAmount);
        face.SetFace(fillAmount);
    }
}
