using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public enum STATE
    {
        QUEUE, ORDER, CHAT, WASH, DRY, FOLD
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name;
    protected EVENT stage;
    protected GameObject customer;
    protected Animator anim;
    protected GameObject player;
    protected State nextState;

    public State(GameObject _customer, Animator _anim, GameObject _player)
    {
        customer = _customer;
        anim = _anim;
        player = _player;
        stage = EVENT.ENTER;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
}
