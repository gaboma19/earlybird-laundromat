using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyDry : DryerState
{
    private DryerController dryerController;
    public ReadyDry(GameObject _dryer, Animator _anim) :
        base(_dryer, _anim)
    {
        name = STATE.READY;
        dryerController = dryer.GetComponent<DryerController>();
    }

    public override void Enter()
    {
        dryerController.isInteractable = true;
        dryerController.progressBar.StopProgressBar();

        base.Enter();
    }

    public override void Update()
    {
        if (Calendar.instance.GetDate() > 2)
        {
            if (dryerController.randValue > 0.8f)
            {
                dryerController.SetBroken();
                nextState = new BrokenDry(dryer, anim);
                stage = EVENT.EXIT;
            }
        }

        if (dryerController.isInteractedWith)
        {
            dryerController.isInteractedWith = false;

            if (Workshift.instance.state == Workshift.STATE.STARTED)
            {
                if (Workshift.instance.GetSelectedLaundry().state == Laundry.STATE.UNLOADED_WASH)
                {
                    anim.SetTrigger("Transition");
                    nextState = new LoadedDry(dryer, anim);
                    stage = EVENT.EXIT;
                }
                else
                {
                    // show dialogue "selected laundry is not ready for drying"
                }
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
