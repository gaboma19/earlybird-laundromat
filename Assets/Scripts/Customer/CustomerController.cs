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

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        player = GameObject.Find("Player");
        buttonPrompt = this.transform.Find("Button Prompt").gameObject;
        currentState = new Queue(this.gameObject, anim, player);

        // not all customers are queueing up for drop off service
        // in this case, their start state is Wash.
        // currentState = new Wash(this.gameObject, anim, player);
    }

    // Update is called once per frame
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
