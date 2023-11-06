using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Linq;

public class Origami : MonoBehaviour
{
    public static Origami instance;
    [SerializeField] private CanvasGroup origami;
    [SerializeField] private ArrowGrid arrowGrid;
    public Laundry laundry;
    private List<Clothes> readyClothes;
    private List<Clothes> foldedClothes;
    private List<Instruction> instructionList;
    private FoldingTableController foldingTable;
    private bool isActive = false;
    private bool tableInUse = false;
    public static event Action OnOrigamiStarted;
    public static event Action OnOrigamiEnded;
    public PlayerInputActions playerControls;
    private InputAction move;

    public void Open()
    {
        origami.gameObject.SetActive(true);
    }

    public void Close()
    {
        origami.gameObject.SetActive(false);
    }

    public List<Instruction> GetInstructionList()
    {
        return instructionList;
    }

    public void FoldClothes(Laundry _laundry, FoldingTableController _foldingTable)
    {
        isActive = true;
        move.Enable();

        laundry = _laundry;
        foldingTable = _foldingTable;
        readyClothes = laundry.clothes.FindAll(FindReady);
        instructionList = readyClothes[0].foldingInstructions;

        OnOrigamiStarted?.Invoke();
        Open();
    }

    void Update()
    {
        if (isActive)
        {
            if (IsClothesListEmpty())
            {
                EndOrigami();
            }
            else
            {
                Vector2 moveDirection = move.ReadValue<Vector2>();
                if (tableInUse == false)
                {
                    tableInUse = true;

                    // given instructionsList
                    // player needs to input the instructions in sequence
                    // within some timeframe, like Auron's overdrive minigame from FFX

                    // switch (instructionList[0].direction)
                    // {
                    //     case Instruction.DIRECTION.UP:
                    //         break;
                    //     case Instruction.DIRECTION.DOWN:
                    //         break;
                    //     case Instruction.DIRECTION.LEFT:
                    //         break;
                    //     case Instruction.DIRECTION.RIGHT:
                    //         break;
                    //     case Instruction.DIRECTION.ROTATE:
                    //         break;
                    // }
                }
            }
        }
    }

    private void OruClothes()
    {
        // if (moveDirection.y > 0.5)
        // {
        //     Debug.Log("up");
        // }
        // else if (moveDirection.y < -0.5f)
        // {
        //     Debug.Log("down");
        // }
        // else if (moveDirection.x > 0.5f)
        // {
        //     Debug.Log("right");
        // }
        // else if (moveDirection.x < -0.5f)
        // {
        //     Debug.Log("left");
        // }
    }

    private bool IsClothesListEmpty()
    {
        if (readyClothes == null)
        {
            return true;
        }
        else
        {
            return !readyClothes.Any();
        }
    }

    private bool FindReady(Clothes clothes)
    {
        if (clothes.state == Clothes.STATE.READY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void EndOrigami()
    {
        laundry.clothes = foldedClothes;
        foldingTable.loadedLaundry = laundry;
        // OnLaundryFolded.Invoke(laundry);

        OnOrigamiEnded.Invoke();
        isActive = false;
        foldedClothes = new();
        Close();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        playerControls = new PlayerInputActions();
        move = playerControls.Player.Move;
    }
}
