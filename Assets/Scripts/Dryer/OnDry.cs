using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnDry : DryerState
{
    public static event Action<Laundry> OnLaundryDried;
    public static event Action<Laundry> OnLaundryDrying;
    private DryerController dryerController;
    [SerializeField] private float dryCycleTime = 60f;
    public OnDry(GameObject _dryer, Animator _anim) :
        base(_dryer, _anim)
    {
        name = STATE.ON;
        dryerController = dryer.GetComponent<DryerController>();
    }

    public override void Enter()
    {
        dryerController.isInteractable = false;
        dryerController.HideInputPrompt();

        OnLaundryDrying.Invoke(dryerController.loadedLaundry);

        base.Enter();
    }

    public override void Update()
    {
        dryCycleTime -= Time.deltaTime;
        if (dryCycleTime <= 0.0f)
        {
            anim.SetTrigger("Transition");
            OnLaundryDried.Invoke(dryerController.loadedLaundry);
            dryerController.isInteractable = true;
            nextState = new DoneDry(dryer, anim);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
