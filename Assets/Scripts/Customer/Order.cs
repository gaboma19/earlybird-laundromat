using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : State
{
    GameObject dialogueBox;
    CustomerController customerController;

    public Order(GameObject _customer, Animator _anim, GameObject _player) :
        base(_customer, _anim, _player)
    {
        name = STATE.ORDER;
        customerController = customer.GetComponent<CustomerController>();
        customerController.isInteractedWith = false;
        DialogueBoxController.OnDialogueEnded += EndOrder;
    }

    public override void Enter()
    {
        DialogueBoxController.instance.StartDialogue(customerController.dialogueAsset, 0, "Customer");

        base.Enter();
    }

    public override void Update()
    {
        if (customerController.isInteractedWith)
        {
            Debug.Log("i'll skip a line!");
            DialogueBoxController.instance.SkipLine();
        }
    }

    void EndOrder()
    {
        stage = EVENT.EXIT;
    }

    public override void Exit()
    {
        // customer leaves the laundromat
        // and is destroyed

        base.Exit();
    }
}
