using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour, IInteractable
{
    Animator anim;
    GameObject player;
    CustomerState currentState;
    public bool isInteractedWith { get; set; }
    public bool isInteractable { get; set; }
    GameObject buttonPrompt;
    public float speed = 10.0f;
    public DialogueAsset dialogueAsset;
    public int queueIndex;
    public Vector2 queuePosition;
    public Patience patience;
    public Laundry laundry;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        player = GameObject.Find("Player");
        buttonPrompt = this.transform.Find("Button Prompt").gameObject;
        currentState = new Queue(this.gameObject, anim, player);
    }

    void FixedUpdate()
    {
        currentState = currentState.Process();
    }

    public void Interact()
    {
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

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
