using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RegisterController : MonoBehaviour, IInteractable
{
    public GameObject buttonPrompt;
    bool isInteractable;

    void Update()
    {
        if (isInteractable)
        {
            buttonPrompt.SetActive(true);
        }
        else
        {
            buttonPrompt.SetActive(false);
        }
    }

    public void Interact()
    {
        isInteractable = true;
    }

    public bool CanInteract()
    {
        return true;
    }
}
