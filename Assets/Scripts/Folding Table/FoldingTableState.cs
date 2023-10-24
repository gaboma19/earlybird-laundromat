using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldingTableState
{
    public enum STATE
    {
        READY, IN_USE
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE name;
    protected EVENT stage;
    protected GameObject foldingTable;
    protected Animator anim;
    protected FoldingTableState nextState;

    public FoldingTableState(GameObject _foldingTable, Animator _anim)
    {
        foldingTable = _foldingTable;
        anim = _anim;
        stage = EVENT.ENTER;
    }
    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public FoldingTableState Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
}
