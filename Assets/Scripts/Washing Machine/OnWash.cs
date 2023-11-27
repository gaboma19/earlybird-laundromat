using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnWash : WashingMachineState
{
    public static event Action<Laundry, Laundry.STATE> OnLaundryWashed;
    public static event Action<Laundry, Laundry.STATE> OnLaundryWashing;
    private WashingMachineController washingMachineController;
    [SerializeField] private float washCycleDuration = 20f;
    private float washCycleRemaining;
    private ProgressBar progressBar;

    public OnWash(GameObject _washingMachine, Animator _anim) :
        base(_washingMachine, _anim)
    {
        name = STATE.ON;
        washingMachineController = washingMachine.GetComponent<WashingMachineController>();
        progressBar = washingMachineController.progressBar;
    }

    public override void Enter()
    {
        washingMachineController.isInteractable = false;
        washingMachineController.HideInputPrompt();

        OnLaundryWashing.Invoke(washingMachineController.loadedLaundry, Laundry.STATE.WASHING);

        washingMachineController.PlaySound();

        progressBar.StartProgressBar();
        washCycleRemaining = washCycleDuration;

        base.Enter();
    }

    public override void Update()
    {
        washCycleRemaining -= Time.deltaTime;

        progressBar.DisplayProgress(washCycleDuration, washCycleRemaining);

        if (washCycleRemaining <= 0.0f)
        {
            anim.SetTrigger("Transition");
            OnLaundryWashed.Invoke(washingMachineController.loadedLaundry, Laundry.STATE.WASHED);
            washingMachineController.isInteractable = true;
            nextState = new DoneWash(washingMachine, anim);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
