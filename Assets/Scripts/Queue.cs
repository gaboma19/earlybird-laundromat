using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : State
{
    public Queue(GameObject _customer, Animator _anim, Transform _player) :
        base(_customer, _anim, _player)
    {
        name = STATE.QUEUE;
    }

    public override void Enter()
    {
        // customer enters the laundromat

        base.Enter();
    }

    public override void Update()
    {
        // if (customer.isInteractedWith())
        // {
        //     nextState = new Order(customer, anim, player);
        //     stage = EVENT.EXIT;
        // }

        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
