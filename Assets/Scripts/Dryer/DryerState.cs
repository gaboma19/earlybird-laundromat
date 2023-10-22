using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryerState
{
    public enum STATE
    {
        READY, LOADED, ON, DONE
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE name;
    protected EVENT stage;
    protected GameObject dryer;
    protected Animator anim;
    protected DryerState nextState;

    public DryerState(GameObject _dryer, Animator _anim)
    {
        dryer = _dryer;
        anim = _anim;
        stage = EVENT.ENTER;
    }
    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public DryerState Process()
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
