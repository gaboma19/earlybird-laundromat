using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnDry : DryerState
{
    public static event Action<Laundry> OnLaundryDried;
    public static event Action<Laundry> OnLaundryDrying;
    private DryerController dryerController;
    [SerializeField] private float dryCycleDuration = 30f;
    private float dryCycleRemaining;
    private ProgressBar progressBar;
    public OnDry(GameObject _dryer, Animator _anim) :
        base(_dryer, _anim)
    {
        name = STATE.ON;
        dryerController = dryer.GetComponent<DryerController>();
        progressBar = dryerController.progressBar;
    }

    public override void Enter()
    {
        dryerController.isInteractable = false;
        dryerController.HideInputPrompt();

        OnLaundryDrying.Invoke(dryerController.loadedLaundry);

        dryerController.PlaySound();

        progressBar.StartProgressBar();
        dryCycleRemaining = dryCycleDuration;

        base.Enter();
    }

    public override void Update()
    {
        dryCycleRemaining -= Time.deltaTime;

        progressBar.DisplayProgress(dryCycleDuration, dryCycleRemaining);

        if (dryCycleRemaining <= 0.0f)
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
