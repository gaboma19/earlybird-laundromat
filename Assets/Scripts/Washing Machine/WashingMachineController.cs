using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachineController : MonoBehaviour, IInteractable
{
    GameObject buttonPrompt;
    Animator anim;
    [SerializeField] private AudioSource audioSource;
    public bool isInteractedWith { get; set; }
    public bool isInteractable { get; set; }
    WashingMachineState currentState;
    public Laundry loadedLaundry { get; set; }
    public ProgressBar progressBar;

    void Start()
    {
        buttonPrompt = this.transform.Find("Button Prompt").gameObject;
        anim = this.GetComponent<Animator>();
        currentState = new ReadyWash(this.gameObject, anim);
    }

    void Update()
    {
        currentState = currentState.Process();

        if (loadedLaundry != null && loadedLaundry.state == Laundry.STATE.DISCARD)
        {
            SetReadyState();
        }
    }

    void Awake()
    {
        Timer.OnTimerEnded += SetReadyState;
        Timer.OnTimerEnded += StopSound;
    }

    public void SetReadyState()
    {
        anim.SetTrigger("Off");
        currentState = new ReadyWash(this.gameObject, anim);
        loadedLaundry = null;
    }

    public void Interact()
    {
        if (!isInteractable)
        {
            return;
        }

        isInteractedWith = true;
    }

    public bool CanInteract()
    {
        return isInteractable;
    }

    public void ShowInputPrompt()
    {
        buttonPrompt.SetActive(true);
    }

    public void HideInputPrompt()
    {
        buttonPrompt.SetActive(false);
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
