using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Linq;

public class Minigame : MonoBehaviour
{
    public static Minigame instance;
    private Clothes selectedClothes;
    [SerializeField] private ClothesWheel clothesWheel;
    [SerializeField] private CanvasGroup minigame;
    public static event Action OnMinigameStarted;
    public static event Action OnMinigameEnded;
    public PlayerInputActions playerControls;
    private InputAction move;
    public Laundry laundry;
    public void Open()
    {
        minigame.gameObject.SetActive(true);
    }

    public void Close()
    {
        minigame.gameObject.SetActive(false);
    }

    public Clothes GetSelectedClothes()
    {
        return selectedClothes;
    }

    public void SeparateClothes(Laundry _laundry)
    {
        OnMinigameStarted?.Invoke();
        move.Enable();

        laundry = _laundry;
        Debug.Log(laundry.clothes[0].type);
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

            if (moveDirection.y > 0.5f)
            {
                LoadClothes(laundry.clothes[0]);
            }
            else if (moveDirection.y < -0.5f)
            {
                KeepClothes(laundry.clothes[0]);
            }
        }
    }

    private void LoadClothes(Clothes clothes)
    {

    }

    private void KeepClothes(Clothes clothes)
    {

    }

    private bool IsClothesListEmpty()
    {
        if (laundry == null)
        {
            return false;
        }
        else
        {
            return !laundry.clothes.Any();
        }
    }

    private void EndMinigame()
    {
        OnMinigameEnded?.Invoke();
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
    }
}
