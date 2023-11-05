using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Order : CustomerState
{
    Vector2 lookDirection;
    Vector2 moveDirection;
    CustomerController customerController;
    public static event Action OnOrderPlaced;

    public Order(GameObject _customer, Animator _anim, GameObject _player) :
        base(_customer, _anim, _player)
    {
        name = STATE.ORDER;
        customerController = customer.GetComponent<CustomerController>();

        customerController.isInteractedWith = false;

        DialogueBoxController.OnDialogueEnded += EndOrder;

        FacePlayer();
    }

    public override void Enter()
    {
        DialogueBoxController.instance.StartDialogue(customerController.dialogueAsset, 0, "Customer");

        float random = UnityEngine.Random.Range(0f, 260f);
        moveDirection = new Vector2(Mathf.Cos(random), Mathf.Sin(random));

        base.Enter();
    }

    public override void Update()
    {
        // interaction should be polled on the dialogue box instead
        if (customerController.isInteractedWith)
        {
            DialogueBoxController.instance.SkipLine();
            customerController.isInteractedWith = false;
        }
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
        nextState = this;
        stage = EVENT.EXIT;
        OnOrderPlaced.Invoke();
    }

    void WalkRandom()
    {
        float speed = customerController.speed;
        Rigidbody2D rigidbody2d = customer.GetComponent<Rigidbody2D>();

        if (!Mathf.Approximately(moveDirection.x, 0.0f) || !Mathf.Approximately(moveDirection.y, 0.0f))
        {
            lookDirection.Set(moveDirection.x, moveDirection.y);
            lookDirection.Normalize();
        }

        anim.SetFloat("Look X", lookDirection.x);
        anim.SetFloat("Look Y", lookDirection.y);
        anim.SetFloat("Speed", moveDirection.magnitude);

        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * moveDirection.x * Time.deltaTime;
        position.y = position.y + speed * moveDirection.y * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public override void Exit()
    {
        // customer leaves the laundromat
        // and is destroyed
        WalkRandom();

        customerController.isInteractable = false;
        customerController.HideInputPrompt();

        DialogueBoxController.OnDialogueEnded -= EndOrder;

        base.Exit();
    }
}
