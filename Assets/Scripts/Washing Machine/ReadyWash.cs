using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyWash : WashingMachineState
{
    private readonly WashingMachineController washingMachineController;
    public ReadyWash(GameObject _washingMachine, Animator _anim) :
        base(_washingMachine, _anim)
    {
        name = STATE.READY;
        washingMachineController = washingMachine.GetComponent<WashingMachineController>();
    }

    public override void Enter()
    {
        washingMachineController.isInteractable = true;

        Minigame.OnMinigameEnded += EndReady;

        base.Enter();
    }

    public override void Update()
    {
        if (washingMachineController.isInteractedWith)
        {
            if (Workshift.instance.state == Workshift.STATE.STARTED)
            {
                Laundry selectedLaundry = Workshift.instance.GetSelectedLaundry();

                if (selectedLaundry.state == Laundry.STATE.DIRTY)
                {
                    washingMachineController.isInteractedWith = false;
                    Minigame.instance.SeparateClothes(selectedLaundry);
                }
                else
                {
                    // show dialogue "selected laundry is not dirty"
                }
            }
            else
            {
                // show dialogue "workshift not started"
            }
        }
    }

    void EndReady()
    {
        if (washingMachineController.isInteractedWith)
        {
            anim.SetTrigger("Transition");
            nextState = new LoadedWash(washingMachine, anim);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}