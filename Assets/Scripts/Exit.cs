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
    private float fadeTime = 5f;
    private float fadeTimeRemaining;
    public static event Action OnDayStarted;
    public static event Action OnDayEnded;
    public void Activate()
    {
        sprite.SetActive(true);
        isActive = true;
        if (Calendar.instance.GetDate() == 1)
        {
            Debug.Log("show exit tutorial");
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
            Debug.Log("exit laundromat scene");

            black.Fade();
            fadingToBlack = true;
            fadeTimeRemaining = fadeTime;
            Deactivate();
            OnDayEnded.Invoke();
        }
        else
        {
            Debug.Log("exit is inactive");
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
