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
    public static event Action OnMinigameEnded;
    public PlayerInputActions playerControls;
    private InputAction move;
    public Laundry laundry;
    private List<Clothes> readyClothes;
    private List<Clothes> loadedClothes = new();
    private List<Clothes> keptClothes = new();
    private bool clothesProcessed = false;
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

    public void SeparateClothes(Laundry _laundry)
    {
        OnMinigameStarted?.Invoke();
        move.Enable();

        laundry = _laundry;
        readyClothes = laundry.clothes.FindAll(FindReady);
        Open();
    }

    private void Update()
    {
        if (isActiveAndEnabled)
        {
            if (IsClothesListEmpty())
            {
                EndMinigame();
            }

            Vector2 moveDirection = move.ReadValue<Vector2>();
            if (clothesProcessed == false)
            {
                if (moveDirection.y > 0.5f)
                {
                    LoadClothes();
                }
                else if (moveDirection.y < -0.5f)
                {
                    KeepClothes();
                }
            }
        }
    }

    private void LoadClothes()
    {
        clothesProcessed = true;
        readyClothes[0].state = Clothes.STATE.LOADED;
        loadedClothes.Add(readyClothes[0]);
        clothesWheel.AnimateLoad();
    }

    private void KeepClothes()
    {
        clothesProcessed = true;
        keptClothes.Add(readyClothes[0]);
        clothesWheel.AnimateKeep();
    }

    private bool IsClothesListEmpty()
    {
        return !readyClothes.Any();
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
        OnMinigameEnded?.Invoke();
        readyClothes = null;
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

        AnimationEnd.OnClothesProcessed += PopReadyClothes;
    }
}
