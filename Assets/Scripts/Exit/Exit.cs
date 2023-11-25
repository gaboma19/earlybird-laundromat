using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Exit : MonoBehaviour
{
    public static Exit instance;
    [SerializeField] GameObject sprite;
    [SerializeField] Black black;
    [SerializeField] Splash splash;
    private bool isActive = false;
    private bool fadingToBlack = false;
    private float fadeTime = 3f;
    private float fadeTimeRemaining;
    public static event Action OnDayStarted;
    public static event Action OnDayEnded;

    public void ActivateWithDelay(float time)
    {
        Invoke("Activate", time);
    }

    public void Activate()
    {
        sprite.SetActive(true);
        isActive = true;
        if (Calendar.instance.GetDate() == 1)
        {
            Tutorial.instance.ShowTutorial(new List<string> { "Good work attendant" });
        }
    }

    public void Deactivate()
    {
        sprite.SetActive(false);
        isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive)
        {
            black.Fade();
            fadingToBlack = true;
            fadeTimeRemaining = fadeTime;
            DestroyAllCustomers();
            Deactivate();
            OnDayEnded.Invoke();
        }
    }

    private void DestroyAllCustomers()
    {
        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
        foreach (GameObject go in customers)
        {
            Destroy(go);
        }
    }

    void Update()
    {
        if (fadingToBlack)
        {
            if (fadeTimeRemaining > 0)
            {
                fadeTimeRemaining -= Time.deltaTime;
            }
            else
            {
                fadingToBlack = false;
                black.Unfade();
                OnDayStarted.Invoke();
                splash.DisplayNewDay();
            }
        }
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
    }
}
