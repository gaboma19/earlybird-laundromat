using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnWash : WashingMachineState
{
    public static event Action<Laundry, Laundry.STATE> OnLaundryWashed;
    public static event Action<Laundry, Laundry.STATE> OnLaundryWashing;
    private WashingMachineController washingMachineController;
    [SerializeField] private float washCycleTime = 20f;
    public OnWash(GameObject _washingMachine, Animator _anim) :
        base(_washingMachine, _anim)
    {
        name = STATE.ON;
        washingMachineController = washingMachine.GetComponent<WashingMachineController>();
    }

    public override void Enter()
    {
        washingMachineController.isInteractable = false;
        washingMachineController.HideInputPrompt();

        OnLaundryWashing.Invoke(washingMachineController.loadedLaundry, Laundry.STATE.WASHING);

        base.Enter();
    }

    public override void Update()
    {
        washCycleTime -= Time.deltaTime;
        if (washCycleTime <= 0.0f)
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
