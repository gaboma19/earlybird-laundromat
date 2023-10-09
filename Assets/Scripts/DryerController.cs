using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryerController : MonoBehaviour, IInteractable
{
    GameObject buttonPrompt;
    Animator anim;
    public bool isInteractable { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        buttonPrompt = this.transform.Find("Button Prompt").gameObject;
        anim = this.GetComponent<Animator>();
        isInteractable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("on"))
        {
            isInteractable = false;
            HideInputPrompt();
        }
        else
        {
            isInteractable = true;
        }
    }

    public void Interact()
    {
        if (isInteractable)
        {
            anim.SetTrigger("Transition");
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
