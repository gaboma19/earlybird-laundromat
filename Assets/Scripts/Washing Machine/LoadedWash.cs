using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadedWash : WashingMachineState
{
    private WashingMachineController washingMachineController;
    public LoadedWash(GameObject _washingMachine, Animator _anim) :
        base(_washingMachine, _anim)
    {
        name = STATE.LOADED;
        washingMachineController = washingMachine.GetComponent<WashingMachineController>();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if (washingMachineController.isInteractedWith)
        {
            anim.SetTrigger("Transition");
            washingMachineController.isInteractedWith = false;
            nextState = new OnWash(washingMachine, anim);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
