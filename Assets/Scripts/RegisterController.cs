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
    public void Interact()
    {
        // call WorkShift which creates customers
        OnWorkshiftStart.Invoke();

        // call DialogueBoxController to inform player
    }

    public bool CanInteract()
    {
        return true;
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
