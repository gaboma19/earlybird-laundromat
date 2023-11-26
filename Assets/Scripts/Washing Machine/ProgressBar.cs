using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    FillBar fillBar;
    float fillAmount;

    public void StartProgressBar()
    {
        gameObject.SetActive(true);
    }

    public void StopProgressBar()
    {
        gameObject.SetActive(false);
    }

    public void DisplayProgress(float duration, float remaining)
    {
        fillAmount = (duration - remaining) / duration;
        fillBar.SetFillBar(fillAmount);
    }

    void Awake()
    {
        fillBar = gameObject.GetComponent<FillBar>();
    }
}
