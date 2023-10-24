using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldingTableController : MonoBehaviour, IInteractable
{
    GameObject buttonPrompt;
    Animator anim;
    public bool isInteractable { get; set; }
    public bool isInteractedWith { get; set; }
    FoldingTableState currentState;
    public Laundry loadedLaundry { get; set; }

    void Start()
    {
        buttonPrompt = this.transform.Find("Button Prompt").gameObject;
        anim = this.GetComponent<Animator>();
        currentState = new ReadyFold(this.gameObject, anim);
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
