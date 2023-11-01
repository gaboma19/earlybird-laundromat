using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyWash : WashingMachineState
{
    private WashingMachineController washingMachineController;
    public ReadyWash(GameObject _washingMachine, Animator _anim) :
        base(_washingMachine, _anim)
    {
        name = STATE.READY;
        washingMachineController = washingMachine.GetComponent<WashingMachineController>();
    }

    public override void Enter()
    {
        washingMachineController.isInteractable = true;

        base.Enter();
    }

    public override void Update()
    {
        if (washingMachineController.isInteractedWith)
        {
            Laundry selectedLaundry = Workshift.instance.GetSelectedLaundry();

            if (selectedLaundry.state == Laundry.STATE.DIRTY)
            {
                // anim.SetTrigger("Transition");
                // nextState = new LoadedWash(washingMachine, anim);
                // stage = EVENT.EXIT;

                Minigame.instance.SeparateClothes(selectedLaundry);
            }
            else
            {
                // show dialogue "selected laundry is not dirty"
            }
            washingMachineController.isInteractedWith = false;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}