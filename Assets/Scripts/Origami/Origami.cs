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
    private FoldingTableController foldingTable;
    private bool isActive = false;
    private bool isSequenceInProgress = false;
    private bool isOrigamiInProgress = false;
    public static event Action OnOrigamiStarted;
    public static event Action<FoldingTableController> OnOrigamiEnded;
    public static event Action OnInstructionCompleted;
    public static event Action OnSequenceCompleted;
    public static event Action OnOrigamiKilled;
    public PlayerInputActions playerControls;
    private InputAction up;
    private InputAction down;
    private InputAction left;
    private InputAction right;

    public void Open()
    {
        origami.gameObject.SetActive(true);

        if (Calendar.instance.GetDate() == 1)
        {
            Tutorial.instance.ShowTutorial(new List<string> { "Input folding instructions" });
        }
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
        up.Enable();
        down.Enable();
        left.Enable();
        right.Enable();

        laundry = _laundry;
        foldingTable = _foldingTable;
        readyClothes = laundry.clothes;
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
            else if (laundry.state == Laundry.STATE.DISCARD)
            {
                KillOrigami();
            }
            else
            {
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

                    if (currentInstruction.direction == Instruction.DIRECTION.UP)
                    {
                        if (up.triggered)
                        {
                            sequenceIndex++;

                            currentInstruction.isCompleted = true;
                            OnInstructionCompleted.Invoke();
                        }
                    }
                    if (currentInstruction.direction == Instruction.DIRECTION.DOWN)
                    {
                        if (down.triggered)
                        {
                            sequenceIndex++;

                            currentInstruction.isCompleted = true;
                            OnInstructionCompleted.Invoke();
                        }
                    }
                    if (currentInstruction.direction == Instruction.DIRECTION.LEFT)
                    {
                        if (left.triggered)
                        {
                            sequenceIndex++;

                            currentInstruction.isCompleted = true;
                            OnInstructionCompleted.Invoke();
                        }
                    }
                    if (currentInstruction.direction == Instruction.DIRECTION.RIGHT)
                    {
                        if (right.triggered)
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
                        // sequenceTimeout = 0f;
                        sequenceIndex = 0;
                        // Fold the next clothes in ready clothes
                        clothesIndex++;

                        if (clothesIndex < readyClothes.Count)
                        {
                            instructionList = readyClothes[clothesIndex].foldingInstructions;
                        }

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
                    clothesIndex++;

                    if (clothesIndex < readyClothes.Count)
                    {
                        instructionList = readyClothes[clothesIndex].foldingInstructions;
                    }

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

    private void EndOrigami()
    {
        laundry.clothes = readyClothes;
        foldingTable.loadedLaundry = laundry;

        OnOrigamiEnded.Invoke(foldingTable);
        isActive = false;
        Close();
    }

    private void KillOrigami()
    {
        OnOrigamiKilled.Invoke();
        isActive = false;
        Close();
    }

    void OnDisable()
    {
        up.Disable();
        down.Disable();
        left.Disable();
        right.Disable();
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

        up = playerControls.Player.OrigamiUp;
        down = playerControls.Player.OrigamiDown;
        left = playerControls.Player.OrigamiLeft;
        right = playerControls.Player.OrigamiRight;

        Timer.OnTimerEnded += KillOrigami;
    }
}
