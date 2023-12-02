using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Linq;

public class Minigame : MonoBehaviour
{
    public static Minigame instance;
    [SerializeField] private ClothesWheel clothesWheel;
    [SerializeField] private CanvasGroup minigame;
    public static event Action OnMinigameStarted;
    public static event Action<WashingMachineController> OnMinigameEnded;
    public PlayerInputActions playerControls;
    private InputAction move;
    public Laundry laundry;
    private List<Clothes> readyClothes;
    private List<Clothes> loadedClothes = new();
    private List<Clothes> keptClothes = new();
    private bool clothesProcessed = false;
    private bool isActive = false;
    private WashingMachineController washingMachine;
    public static event Action<Laundry> OnLoadDirtyLaundry;
    public static event Action<Laundry> OnDiscardLaundry;
    public static event Action OnMinigameKilled;
    public void Open()
    {
        minigame.gameObject.SetActive(true);

        if (Calendar.instance.GetDate() == 1)
        {
            Tutorial.instance.ShowTutorial(new List<string> { "Load items into" });
        }
    }

    public void Close()
    {
        minigame.gameObject.SetActive(false);
    }

    public List<Clothes> GetReadyClothes()
    {
        return readyClothes;
    }

    public void SeparateClothes(Laundry _laundry, WashingMachineController _washingMachine)
    {
        isActive = true;
        move.Enable();

        laundry = _laundry;
        washingMachine = _washingMachine;
        readyClothes = laundry.clothes;

        OnMinigameStarted?.Invoke();
        Open();
    }

    private void Update()
    {
        if (isActive)
        {
            if (IsClothesListEmpty())
            {
                EndMinigame();
            }
            else if (laundry.state == Laundry.STATE.DISCARD)
            {
                KillMinigame();
            }
            else
            {
                Vector2 moveDirection = move.ReadValue<Vector2>();
                if (clothesProcessed == false)
                {
                    if (moveDirection.y < -0.5f)
                    {
                        KeepClothes();
                    }
                    else if (moveDirection.y > 0.5f)
                    {
                        LoadClothes();
                    }
                }
            }
        }
    }

    private void LoadClothes()
    {
        clothesProcessed = true;
        loadedClothes.Add(readyClothes[0]);

        if (readyClothes[0].type == Clothes.TYPE.TRASH)
        {
            laundry.customerController.patience.ApplyPenalty();

            if (!Tutorial.instance.hasPatienceTutorialShown)
            {
                Tutorial.instance.hasPatienceTutorialShown = true;
                Tutorial.instance.ShowTutorial(new List<string> { "Loading trash items" });
            }
        }

        clothesWheel.AnimateLoad();
    }

    private void KeepClothes()
    {
        clothesProcessed = true;
        keptClothes.Add(readyClothes[0]);

        if (readyClothes[0].type != Clothes.TYPE.TRASH)
        {
            laundry.customerController.patience.ApplyPenalty();
        }

        clothesWheel.AnimateKeep();
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

    private void EndMinigame()
    {
        if (!loadedClothes.Any())
        {
            OnDiscardLaundry.Invoke(laundry);
            washingMachine.loadedLaundry = null;
        }
        else
        {
            laundry.clothes = loadedClothes;
            washingMachine.loadedLaundry = laundry;
            OnLoadDirtyLaundry.Invoke(laundry);
        }

        OnMinigameEnded.Invoke(washingMachine);
        isActive = false;
        loadedClothes = new();
        keptClothes = new();
        Close();
    }

    private void PopReadyClothes()
    {
        readyClothes.RemoveAt(0);

        clothesProcessed = false;
    }

    private void KillMinigame()
    {
        OnMinigameKilled.Invoke();
        isActive = false;
        loadedClothes = new();
        keptClothes = new();
        Close();
    }

    private void Awake()
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

        UpAnimationEnd.OnClothesLoaded += PopReadyClothes;
        DownAnimationEnd.OnClothesKept += PopReadyClothes;

        Timer.OnTimerEnded += KillMinigame;
    }
}
