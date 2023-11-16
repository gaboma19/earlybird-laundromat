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
    private List<Instruction> instructionList;
    private Instruction currentInstruction;
    [SerializeField] private SequenceTimer sequenceTimer;
    [SerializeField] private float sequenceDuration = 4f;
    private float sequenceTimeout;
    private int sequenceIndex;
    private int clothesIndex;
    // private float totalRollingAngle = 0f;
    private Vector2 previousMoveDirection = Vector2.zero;
    private Vector2 currentMoveDirection;
    private Vector2 moveDirectionDelta;
    private FoldingTableController foldingTable;
    private bool isActive = false;
    private bool isSequenceInProgress = false;
    private bool isOrigamiInProgress = false;
    public static event Action OnOrigamiStarted;
    public static event Action<FoldingTableController> OnOrigamiEnded;
    public static event Action OnInstructionCompleted;
    public static event Action OnSequenceCompleted;
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

    public Clothes GetCurrentClothes()
    {
        if (clothesIndex >= readyClothes.Count)
        {
            return readyClothes[^1];
        }

        return readyClothes[clothesIndex];
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
                currentMoveDirection = move.ReadValue<Vector2>();

                if (!isOrigamiInProgress)
                {
                    isOrigamiInProgress = true;
                    // Initialize the ready clothes index
                    clothesIndex = 0;
                }

                if (isOrigamiInProgress && clothesIndex >= readyClothes.Count)
                {
                    isOrigamiInProgress = false;

                    EndOrigami();
                }

                // Check if the player is not already in the middle of a sequence
                if (!isSequenceInProgress)
                {
                    // Set a flag to indicate that a sequence is in progress
                    isSequenceInProgress = true;
                    // Start a timer for the sequence timeout
                    sequenceTimeout = sequenceDuration;
                    // Initialize the sequence index
                    sequenceIndex = 0;
                }

                if (isSequenceInProgress)
                {
                    currentInstruction = instructionList[sequenceIndex];

                    // if (currentInstruction.direction == Instruction.DIRECTION.ROTATE)
                    // {
                    //     // Calculate the difference between current and previous moveDirection
                    //     moveDirectionDelta = currentMoveDirection - previousMoveDirection;
                    //     // Calculate the angle of the rolling motion
                    //     float rollingAngle = Mathf.Atan2(moveDirectionDelta.y, moveDirectionDelta.x) * Mathf.Rad2Deg;
                    //     // Update the total accumulated angle
                    //     totalRollingAngle += rollingAngle;

                    //     // Check for full circle input
                    //     if (Mathf.Abs(totalRollingAngle) >= 360f)
                    //     {
                    //         // Increment the sequence index
                    //         sequenceIndex++;

                    //         currentInstruction.isCompleted = true;
                    //         OnInstructionCompleted.Invoke();

                    //         // Reset the total angle
                    //         totalRollingAngle = 0f;
                    //     }
                    // }

                    if (currentInstruction.direction == Instruction.DIRECTION.UP)
                    {
                        if (currentMoveDirection.y > 0.5)
                        {
                            sequenceIndex++;

                            currentInstruction.isCompleted = true;
                            OnInstructionCompleted.Invoke();
                        }
                    }
                    if (currentInstruction.direction == Instruction.DIRECTION.DOWN)
                    {
                        if (currentMoveDirection.y < -0.5f)
                        {
                            sequenceIndex++;

                            currentInstruction.isCompleted = true;
                            OnInstructionCompleted.Invoke();
                        }
                    }
                    if (currentInstruction.direction == Instruction.DIRECTION.LEFT)
                    {
                        if (currentMoveDirection.x < -0.5f)
                        {
                            sequenceIndex++;

                            currentInstruction.isCompleted = true;
                            OnInstructionCompleted.Invoke();
                        }
                    }
                    if (currentInstruction.direction == Instruction.DIRECTION.RIGHT)
                    {
                        if (currentMoveDirection.x > 0.5f)
                        {
                            sequenceIndex++;

                            currentInstruction.isCompleted = true;
                            OnInstructionCompleted.Invoke();
                        }
                    }

                    // Check if the player completed the sequence
                    if (sequenceIndex >= instructionList.Count)
                    {
                        // Reset the sequence-related variables
                        isSequenceInProgress = false;
                        sequenceTimeout = 0f;
                        sequenceIndex = 0;
                        // Fold the next clothes in ready clothes
                        readyClothes[clothesIndex].state = Clothes.STATE.DONE;
                        clothesIndex++;

                        if (clothesIndex < readyClothes.Count)
                        {
                            instructionList = readyClothes[clothesIndex].foldingInstructions;
                        }

                        // Do something when the player successfully completes the sequence
                        Debug.Log("Sequence completed!");

                        OnSequenceCompleted.Invoke();
                    }
                }

                // Update the sequence timeout
                sequenceTimeout -= Time.deltaTime;
                sequenceTimer.DisplayTime(sequenceTimeout);

                // Check if the sequence timed out
                if (isSequenceInProgress && sequenceTimeout <= 0f)
                {
                    // Reset the sequence-related variables
                    isSequenceInProgress = false;
                    sequenceTimeout = 0f;
                    sequenceIndex = 0;
                    // Fold the next clothes in ready clothes
                    readyClothes[clothesIndex].state = Clothes.STATE.DONE;
                    clothesIndex++;
                    instructionList = readyClothes[clothesIndex].foldingInstructions;

                    // Do something when the sequence times out
                    Debug.Log("Sequence timed out.");

                    OnSequenceCompleted.Invoke();
                }
            }
        }
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
        laundry.clothes = readyClothes;
        foldingTable.loadedLaundry = laundry;
        // OnLaundryFolded.Invoke(laundry);

        OnOrigamiEnded.Invoke(foldingTable);
        isActive = false;
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
