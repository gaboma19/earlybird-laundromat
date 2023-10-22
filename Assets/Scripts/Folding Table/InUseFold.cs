using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InUseFold : FoldingTableState
{
    private FoldingTableController foldingTableController;
    public static event Action OnLaundryFolding;
    public static event Action<Laundry> OnLaundryFolded;

    public InUseFold(GameObject _foldingTable, Animator _anim) :
        base(_foldingTable, _anim)
    {
        name = STATE.IN_USE;
        foldingTableController = foldingTable.GetComponent<FoldingTableController>();
    }

    public override void Enter()
    {
        // foldingTableController.isInteractable = false;
        // foldingTableController.HideInputPrompt();

        OnLaundryFolding.Invoke();

        base.Enter();
    }

    public override void Update()
    {
        // implement folding clothes minigame
        // folding table is not interactable while in use
        // when minigame is completed, nextState is ReadyFold

        if (foldingTableController.isInteractedWith)
        {
            foldingTableController.isInteractedWith = false;

            OnLaundryFolded.Invoke(foldingTableController.loadedLaundry);
            foldingTableController.loadedLaundry = null;

            nextState = new ReadyFold(foldingTable, anim);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
