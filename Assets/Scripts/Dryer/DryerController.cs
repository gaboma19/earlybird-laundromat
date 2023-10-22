using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryerController : MonoBehaviour, IInteractable
{
    GameObject buttonPrompt;
    Animator anim;
    public bool isInteractable { get; set; }
    public bool isInteractedWith { get; set; }
    DryerState currentState;
    public Laundry loadedLaundry { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        buttonPrompt = this.transform.Find("Button Prompt").gameObject;
        anim = this.GetComponent<Animator>();
        currentState = new ReadyDry(this.gameObject, anim);
    }

    // Update is called once per frame
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