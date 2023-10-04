using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RegisterController : MonoBehaviour, IInteractable
{
    public GameObject buttonPrompt;
    public GameObject customer;

    public void Interact()
    {
        Instantiate(customer, new Vector3(0, 0, 0), Quaternion.identity);
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
