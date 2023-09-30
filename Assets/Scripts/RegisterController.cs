using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RegisterController : MonoBehaviour, IInteractable
{
    public GameObject buttonPrompt;

    public void Interact()
    {
        Debug.Log("register interacted with");
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
