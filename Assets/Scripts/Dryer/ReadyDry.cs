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

        base.Enter();
    }

    public override void Update()
    {
        if (dryerController.isInteractedWith)
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
            dryerController.isInteractedWith = false;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}