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
        washingMachineController.progressBar.StopProgressBar();

        Minigame.OnMinigameEnded += EndReady;

        base.Enter();
    }

    public override void Update()
    {
        if (Calendar.instance.GetDate() > 2)
        {
            if (washingMachineController.randValue > 0.8f)
            {
                washingMachineController.SetBroken();
                nextState = new BrokenWash(washingMachine, anim);
                stage = EVENT.EXIT;
            }
        }

        if (washingMachineController.isInteractedWith)
        {
            washingMachineController.isInteractedWith = false;

            if (Workshift.instance.state == Workshift.STATE.STARTED)
            {
                Laundry selectedLaundry = Workshift.instance.GetSelectedLaundry();

                if (selectedLaundry.state == Laundry.STATE.DIRTY)
                {
                    Minigame.instance.SeparateClothes(selectedLaundry, washingMachineController);
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

    void EndReady(WashingMachineController _washingMachineController)
    {
        if (washingMachineController == _washingMachineController)
        {
            if (washingMachineController.loadedLaundry == null)
            {
                anim.SetTrigger("Off");
                nextState = new ReadyWash(washingMachine, anim);
                stage = EVENT.EXIT;
            }
            else
            {
                anim.SetTrigger("Transition");
                nextState = new LoadedWash(washingMachine, anim);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        Minigame.OnMinigameEnded -= EndReady;
        base.Exit();
    }
}
