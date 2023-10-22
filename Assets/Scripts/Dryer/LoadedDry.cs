using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadedDry : DryerState
{
    private DryerController dryerController;
    public static event Action OnLoadDryer;
    public LoadedDry(GameObject _dryer, Animator _anim) :
        base(_dryer, _anim)
    {
        name = STATE.LOADED;
        dryerController = dryer.GetComponent<DryerController>();
    }

    public override void Enter()
    {
        OnLoadDryer.Invoke();

        dryerController.loadedLaundry = Workshift.instance.GetSelectedLaundry();

        base.Enter();
    }

    public override void Update()
    {
        if (dryerController.isInteractedWith)
        {
            anim.SetTrigger("Transition");
            dryerController.isInteractedWith = false;
            nextState = new OnDry(dryer, anim);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
