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

        base.Enter();
    }

    public override void Update()
    {
        if (foldingTableController.isInteractedWith)
        {
            foldingTableController.isInteractedWith = false;

            Laundry selectedLaundry = Workshift.instance.GetSelectedLaundry();
            if (selectedLaundry.state == Laundry.STATE.UNLOADED_DRY)
            {
                foldingTableController.loadedLaundry = selectedLaundry;
                nextState = new InUseFold(foldingTable, anim);
                stage = EVENT.EXIT;
            }
            else
            {
                // show dialogue "selected laundry is not ready for folding"
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
