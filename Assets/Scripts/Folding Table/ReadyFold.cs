using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyFold : FoldingTableState
{
    private FoldingTableController foldingTableController;

    public ReadyFold(GameObject _foldingTable, Animator _anim) :
        base(_foldingTable, _anim)
    {
        name = STATE.READY;
        foldingTableController = foldingTable.GetComponent<FoldingTableController>();
    }

    public override void Enter()
    {
        foldingTableController.isInteractable = true;

        Origami.OnOrigamiEnded += EndReady;

        base.Enter();
    }

    public override void Update()
    {
        if (foldingTableController.isInteractedWith)
        {
            foldingTableController.isInteractedWith = false;

            if (Workshift.instance.state == Workshift.STATE.STARTED)
            {
                Laundry selectedLaundry = Workshift.instance.GetSelectedLaundry();

                if (selectedLaundry.state == Laundry.STATE.UNLOADED_DRY)
                {
                    Origami.instance.FoldClothes(selectedLaundry, foldingTableController);
                }
                else
                {
                    // show dialogue "selected laundry is not ready for folding"
                }
            }
        }
    }

    private void EndReady(FoldingTableController _foldingTableController)
    {
        if (foldingTableController == _foldingTableController)
        {
            nextState = new InUseFold(foldingTable, anim);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
