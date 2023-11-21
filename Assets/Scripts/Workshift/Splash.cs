using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Splash : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI splashText;
    [SerializeField] private float duration = 4f;
    [SerializeField] private GameObject scoreImage;
    [SerializeField] private GameObject workshiftImage;
    [SerializeField] private GameObject calendarImage;
    [SerializeField] TextMeshProUGUI scoreText;
    private float timeout;
    private bool isActive = false;

    public void DisplaySplash(string _text)
    {
        isActive = true;
        timeout = duration;
        splashText.text = _text;
        workshiftImage.SetActive(true);
    }

    public void DisplayScore(string _text)
    {
        isActive = true;
        timeout = duration;
        scoreText.text = "$" + _text;
        scoreImage.SetActive(true);
    }

    public void DisplayNewDay()
    {
        isActive = true;
        timeout = duration;
        calendarImage.SetActive(true);
    }

    void Update()
    {
        if (isActive)
        {
            if (timeout > 0)
            {
                timeout -= Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
                scoreImage.SetActive(false);
                calendarImage.SetActive(false);
                timeout = duration;
                isActive = false;
            }
        }
    }
}
