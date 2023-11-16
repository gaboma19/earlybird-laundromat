using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class RegisterController : MonoBehaviour, IInteractable
{
    public GameObject buttonPrompt;
    public GameObject customer;
    public static event Action OnWorkshiftStart;
    public static event Action OnLaundryDone;
    private bool isInteractable;

    void Start()
    {
        isInteractable = true;
    }
    public void Interact()
    {
        if (Workshift.instance.state == Workshift.STATE.STARTED)
        {
            OnLaundryDone.Invoke();
        }
        else if (Workshift.instance.state == Workshift.STATE.READY)
        {
            OnWorkshiftStart.Invoke();
        }
        else if (Workshift.instance.state == Workshift.STATE.DONE)
        {
            // show dialogue saying the work shift is over
        }
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
}
