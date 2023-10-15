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
    private bool isInteractable;

    void Start()
    {
        isInteractable = true;
    }
    public void Interact()
    {
        OnWorkshiftStart.Invoke();
        isInteractable = false;
        HideInputPrompt();

        // call DialogueBoxController to inform player
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
