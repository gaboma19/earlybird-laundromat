using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachineController : MonoBehaviour, IInteractable
{
    GameObject buttonPrompt;
    Animator anim;
    public float time = 1.0f;
    public bool isInteractable { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        buttonPrompt = this.transform.Find("Button Prompt").gameObject;
        anim = this.GetComponent<Animator>();
        isInteractable = true;
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("on"))
        {
            isInteractable = false;
        }
        else
        {
            isInteractable = true;
        }
    }

    public void Interact()
    {
        anim.SetTrigger("Transition");
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
