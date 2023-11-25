using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Order : CustomerState
{
    Vector2 lookDirection;
    CustomerController customerController;
    Rigidbody2D rigidbody2D;
    public static event Action<CustomerController> OnOrderPlaced;

    public Order(GameObject _customer, Animator _anim, GameObject _player) :
        base(_customer, _anim, _player)
    {
        name = STATE.ORDER;
        customerController = customer.GetComponent<CustomerController>();
        rigidbody2D = customerController.GetComponent<Rigidbody2D>();
        customerController.isInteractedWith = false;

        DialogueBoxController.OnDialogueEnded += EndOrder;
        Timer.OnTimerEnded += Leave;
    }

    public override void Enter()
    {
        FacePlayer();
        DialogueBoxController.instance.StartDialogue(customerController.dialogueAsset, 0, "Customer");

        rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;

        base.Enter();
    }

    public override void Update()
    {

    }

    void FacePlayer()
    {
        lookDirection = player.transform.position - customer.transform.position;
        lookDirection.Normalize();

        anim.SetFloat("Look X", lookDirection.x);
        anim.SetFloat("Look Y", lookDirection.y);
        anim.SetFloat("Speed", 0);
    }

    void EndOrder()
    {
        nextState = new Wait(customer, anim, player);
        stage = EVENT.EXIT;
        OnOrderPlaced.Invoke(customerController);
    }

    void Leave()
    {
        nextState = new Leave(customer, anim, player);
        stage = EVENT.EXIT;
        QueuePointsController.instance.SetQueuePointAvailable(customerController.queueIndex);
    }

    public override void Exit()
    {
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        customerController.isInteractable = false;
        customerController.HideInputPrompt();

        DialogueBoxController.OnDialogueEnded -= EndOrder;
        Timer.OnTimerEnded -= Leave;

        base.Exit();
    }
}
