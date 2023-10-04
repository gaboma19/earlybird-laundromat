using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : State
{
    public Order((GameObject _customer, Animator _anim, Transform _player) :
        base(_customer, _anim, _player)
    {
        name = STATE.ORDER;
    }

    public override void Enter()
    {
        // begin dialogue

        base.Enter();
    }

    public override void Update()
    {
        // continue dialogue
        // until player finishes dialogue
        // no nextState
        // stage = EVENT.EXIT

        base.Update();
    }

    public override void Exit()
    {
        // customer leaves the laundromat

        base.Exit();
    }
}
