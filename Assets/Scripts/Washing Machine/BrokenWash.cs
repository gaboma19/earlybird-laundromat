using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenWash : WashingMachineState
{
    private readonly WashingMachineController washingMachineController;
    public BrokenWash(GameObject _washingMachine, Animator _anim) :
        base(_washingMachine, _anim)
    {
        name = STATE.BROKEN;
        washingMachineController = washingMachine.GetComponent<WashingMachineController>();
    }

    public override void Enter()
    {
        washingMachineController.isInteractable = false;

        Timer.OnTimerEnded += EndBroken;

        base.Enter();
    }

    public override void Update()
    {

    }

    void EndBroken()
    {
        washingMachineController.SetFixed();
        nextState = new ReadyWash(washingMachine, anim);
        stage = EVENT.EXIT;
    }

    public override void Exit()
    {
        Timer.OnTimerEnded -= EndBroken;
        base.Exit();
    }
}
