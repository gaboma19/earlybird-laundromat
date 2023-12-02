using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenDry : DryerState
{
  private readonly DryerController dryerController;
  public BrokenDry(GameObject _dryer, Animator _anim) :
      base(_dryer, _anim)
  {
    name = STATE.BROKEN;
    dryerController = dryer.GetComponent<DryerController>();
  }

  public override void Enter()
  {
    dryerController.isInteractable = false;

    Timer.OnTimerEnded += EndBroken;

    base.Enter();
  }

  public override void Update()
  {

  }

  void EndBroken()
  {
    dryerController.SetFixed();
    nextState = new ReadyDry(dryer, anim);
    stage = EVENT.EXIT;
  }

  public override void Exit()
  {
    Timer.OnTimerEnded -= EndBroken;
    base.Exit();
  }
}
