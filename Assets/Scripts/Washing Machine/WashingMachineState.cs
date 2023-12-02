using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachineState
{
    public enum STATE
    {
        READY, LOADED, ON, DONE, BROKEN
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE name;
    protected EVENT stage;
    protected GameObject washingMachine;
    protected Animator anim;
    protected WashingMachineState nextState;

    public WashingMachineState(GameObject _washingMachine, Animator _anim)
    {
        washingMachine = _washingMachine;
        anim = _anim;
        stage = EVENT.ENTER;
    }
    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public WashingMachineState Process()
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
