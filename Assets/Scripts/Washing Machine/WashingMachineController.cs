using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachineController : MonoBehaviour, IInteractable
{
    GameObject buttonPrompt;
    Animator anim;
    public bool isInteractedWith { get; set; }
    public bool isInteractable { get; set; }
    WashingMachineState currentState;
    public Laundry loadedLaundry { get; set; }
    public List<Clothes> loadedClothes;

    void Start()
    {
        buttonPrompt = this.transform.Find("Button Prompt").gameObject;
        anim = this.GetComponent<Animator>();
        loadedClothes = new();
        currentState = new ReadyWash(this.gameObject, anim);
    }

    void Update()
    {
        currentState = currentState.Process();
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
}
