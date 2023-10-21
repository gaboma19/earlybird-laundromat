using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneWash : WashingMachineState
{
    private WashingMachineController washingMachineController;
    public DoneWash(GameObject _washingMachine, Animator _anim) :
        base(_washingMachine, _anim)
    {
        name = STATE.DONE;
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
            nextState = new ReadyWash(washingMachine, anim);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
