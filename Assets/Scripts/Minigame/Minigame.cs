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
    public static event Action<Laundry, Laundry.STATE> OnDiscardLaundry;
    public void Open()
    {
        minigame.gameObject.SetActive(true);
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
        readyClothes = laundry.clothes.FindAll(FindReady);

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
        readyClothes[0].state = Clothes.STATE.DONE;
        loadedClothes.Add(readyClothes[0]);
        clothesWheel.AnimateLoad();
    }

    private void KeepClothes()
    {
        clothesProcessed = true;
        readyClothes[0].state = Clothes.STATE.READY;
        keptClothes.Add(readyClothes[0]);
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

    private void EndMinigame()
    {
        if (!loadedClothes.Any())
        {
            OnDiscardLaundry.Invoke(laundry, Laundry.STATE.DISCARD);
        }
        else
        {
            laundry.clothes = loadedClothes;
            laundry.SetClothesToReady();
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
    }
}
