using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : State
{
    GameObject dialoguePanel;
    CustomerController customerController;

    public Order(GameObject _customer, Animator _anim, GameObject _player) :
        base(_customer, _anim, _player)
    {
        name = STATE.ORDER;
        dialoguePanel = GameObject.Find("Canvas/Dialogue Panel");
        Debug.Log(dialoguePanel);
        customerController = customer.GetComponent<CustomerController>();
        customerController.isInteractedWith = false;
    }

    public override void Enter()
    {
        dialoguePanel.SetActive(true);

        base.Enter();
    }

    public override void Update()
    {
        // continue dialogue
        // until player finishes dialogue
        // there is no nextState
        // stage = EVENT.EXIT
    }

    public override void Exit()
    {
        // customer leaves the laundromat
        // and is destroyed

        base.Exit();
    }
}
