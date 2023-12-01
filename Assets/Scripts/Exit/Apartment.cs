using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Apartment : MonoBehaviour
{
    public static Apartment instance;
    public static event Action OnDayStarted;
    public static event Action OnDayEnded;
    Black black;
    Splash splash;
    [SerializeField] GameObject sprite;
    private bool fadingToBlack = false;
    private float fadeTime = 3f;
    private float fadeTimeRemaining;
    private bool isActive = false;

    public void Activate()
    {
        sprite.SetActive(true);
        isActive = true;
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
            Deactivate();
            black.Fade();
            fadingToBlack = true;
            fadeTimeRemaining = fadeTime;
            OnDayEnded.Invoke();
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

        splash = GameObject.Find("Splash").GetComponent<Splash>();
        black = GameObject.Find("Black").GetComponent<Black>();

        Activate();
    }
}
