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
        foldingTableController.isInteractable = false;
        foldingTableController.HideInputPrompt();

        OnLaundryFolding.Invoke();

        Origami.OnOrigamiEnded += EndInUse;
        Wait.OnPatienceEnded += PatienceEndInUse;

        base.Enter();
    }

    public override void Update()
    {
        if (foldingTableController.isInteractedWith)
        {
            foldingTableController.isInteractedWith = false;

            Origami.instance.FoldClothes(foldingTableController.loadedLaundry, foldingTableController);
        }
    }

    private void EndInUse(FoldingTableController _foldingTableController)
    {
        if (foldingTableController == _foldingTableController)
        {
            OnLaundryFolded.Invoke(foldingTableController.loadedLaundry);

            if (Calendar.instance.GetDate() == 1 && foldingTableController.loadedLaundry.state == Laundry.STATE.FOLDED)
            {
                Tutorial.instance.ShowTutorial(new List<string> { "Take the folded laundry" });
            }

            nextState = new ReadyFold(foldingTable, anim);
            stage = EVENT.EXIT;
        }
    }

    private void PatienceEndInUse(Laundry _laundry)
    {
        if (foldingTableController.loadedLaundry == _laundry)
        {
            nextState = new ReadyFold(foldingTable, anim);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        Origami.OnOrigamiEnded -= EndInUse;
        foldingTableController.loadedLaundry = null;

        base.Exit();
    }
}
