using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldingTableController : MonoBehaviour, IInteractable
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

    void Update()
    {
        // no longer interactable if being used by a customer
    }

    public void Interact()
    {
        // open folding clothes minigame
        // with selected Laundry
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
