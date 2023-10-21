using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WashingMachineController : MonoBehaviour, IInteractable
{
    GameObject buttonPrompt;
    Animator anim;
    public bool isInteractedWith { get; set; }
    public bool isInteractable { get; set; }
    WashingMachineState currentState;
    public Laundry loadedLaundry { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        buttonPrompt = this.transform.Find("Button Prompt").gameObject;
        anim = this.GetComponent<Animator>();
        currentState = new ReadyWash(this.gameObject, anim);
    }

    void Update()
    {
        currentState = currentState.Process();

        // if (anim.GetCurrentAnimatorStateInfo(0).IsName("on"))
        // {
        //     isInteractable = false;
        //     HideInputPrompt();
        // }
        // else
        // {
        //     isInteractable = true;
        // }
    }

    public void Interact()
    {
        if (!isInteractable)
        {
            return;
        }

        isInteractedWith = true;

        // switch (state)
        // {
        //     case STATE.READY:
        //         OnLoadDirtyLaundry.Invoke();
        //         anim.SetTrigger("Transition");
        //         state = STATE.LOADED;
        //         break;
        //     case STATE.LOADED:
        //         OnLaundryWashed.Invoke();
        //         anim.SetTrigger("Transition");
        //         state = STATE.ON;
        //         break;
        //     case STATE.ON:
        //         // machine is not interactable until DONE
        //         anim.SetTrigger("Transition");
        //         break;
        //     case STATE.DONE:
        //         // event when wash cycle is finished
        //         break;
        // }

        // if DONE
        // OnWasherUnloaded
        // - tell workshift which Laundry is washed

        // elif ON 
        // - player can't use this machine

        // elif READY
        // - OnLaundryWashed
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
