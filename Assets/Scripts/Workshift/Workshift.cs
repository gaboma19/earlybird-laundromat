using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Linq;

public class Workshift : MonoBehaviour
{
    public static Workshift instance;
    private List<Laundry> activeLaundry = new List<Laundry>();
    private List<Laundry> doneLaundry = new List<Laundry>();
    [SerializeField] private Timer timer;
    [SerializeField] private Splash splash;
    public float customerTimeInterval;
    private float customerTimeRemaining;
    public static event Action OnLaundrySpawned;
    public PlayerInputActions playerControls;
    private InputAction selectLeft;
    private InputAction selectRight;
    public static event Action OnLaundrySelected;
    public static event Action OnLaundryRemoved;
    private Laundry selectedLaundry;
    public static event Action<decimal> OnScoreAdded;
    public static event Action<decimal> OnBonusScoreAdded;
    private int doneLaundryCount;
    private bool isEndedEarly;
    [SerializeField] private int bonusTimeInterval = 20;
    private int discardedLaundryCount;
    private int happyLaundryCount;
    public enum STATE
    {
        READY, STARTED, DONE
    }
    public STATE state;

    public int GetDoneLaundryCount()
    {
        return doneLaundryCount;
    }

    public int GetHappyLaundryCount()
    {
        return happyLaundryCount;
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

        state = STATE.READY;
        customerTimeRemaining = customerTimeInterval;

        RegisterController.OnWorkshiftStart += StartWorkShift;
        Timer.OnTimerEnded += EndWorkShift;
        Order.OnOrderPlaced += AddActiveLaundry;
        Minigame.OnLoadDirtyLaundry += LoadDirtyLaundry;
        Minigame.OnDiscardLaundry += DiscardLaundry;
        OnWash.OnLaundryWashing += SetLaundryState;
        OnWash.OnLaundryWashed += SetLaundryState;
        LoadedDry.OnLoadDryer += LoadWashedLaundry;
        OnDry.OnLaundryDrying += SetDryingLaundry;
        OnDry.OnLaundryDried += SetDriedLaundry;
        DoneWash.OnUnloadWasher += UnloadWashedLaundry;
        DoneDry.OnUnloadDryer += UnloadDriedLaundry;
        InUseFold.OnLaundryFolding += FoldDriedLaundry;
        InUseFold.OnLaundryFolded += SetFoldedLaundry;
        RegisterController.OnLaundryDone += SetDoneLaundry;
        Minigame.OnMinigameStarted += DisableSelect;
        Minigame.OnMinigameEnded += (_) => EnableSelect();
        Exit.OnDayStarted += ResetWorkShift;
        Wait.OnPatienceEnded += DiscardLaundry;

        playerControls = new PlayerInputActions();
    }
    private void OnEnable()
    {
        selectLeft = playerControls.Player.SelectLeft;
        selectRight = playerControls.Player.SelectRight;
        selectLeft.Enable();
        selectRight.Enable();

        selectLeft.performed += SelectLeftLaundry;
        selectRight.performed += SelectRightLaundry;
    }

    private void OnDisable()
    {
        selectLeft.Disable();
        selectRight.Disable();

        RegisterController.OnWorkshiftStart -= StartWorkShift;
        Timer.OnTimerEnded -= EndWorkShift;
        Order.OnOrderPlaced -= AddActiveLaundry;
        Minigame.OnLoadDirtyLaundry -= LoadDirtyLaundry;
        Minigame.OnDiscardLaundry -= DiscardLaundry;
        OnWash.OnLaundryWashing -= SetLaundryState;
        OnWash.OnLaundryWashed -= SetLaundryState;

        DoneWash.OnUnloadWasher -= UnloadWashedLaundry;
        LoadedDry.OnLoadDryer -= LoadWashedLaundry;
        OnDry.OnLaundryDrying -= SetDryingLaundry;
        OnDry.OnLaundryDried -= SetDriedLaundry;
        DoneDry.OnUnloadDryer -= UnloadDriedLaundry;
        InUseFold.OnLaundryFolding -= FoldDriedLaundry;
        InUseFold.OnLaundryFolded -= SetFoldedLaundry;
        RegisterController.OnLaundryDone -= SetDoneLaundry;
    }

    private void StartWorkShift()
    {
        isEndedEarly = false;
        timer.timerIsRunning = true;
        state = STATE.STARTED;
        splash.DisplaySplash("Open for business!");

        // AddTestLaundry(Laundry.STATE.UNLOADED_DRY);
    }

    private void EndWorkShift()
    {
        if (!isEndedEarly)
        {
            state = STATE.DONE;

            activeLaundry.Clear();
            selectedLaundry = null;
            OnLaundryRemoved.Invoke();
            splash.DisplaySplash("Closed for the day!");
            Exit.instance.ActivateWithDelay(4f);
        }
    }

    private void EndWorkShiftEarly()
    {
        if (discardedLaundryCount < 1)
        {
            decimal bonus = (decimal)Math.Floor(timer.GetTimeRemaining() / bonusTimeInterval);
            OnBonusScoreAdded.Invoke(bonus);
        }

        state = STATE.DONE;

        timer.StopTimer();

        splash.DisplaySplash("All laundry completed!");
        Exit.instance.ActivateWithDelay(4f);
    }

    private void ResetWorkShift()
    {
        state = STATE.READY;
        doneLaundryCount = 0;
        happyLaundryCount = 0;
        discardedLaundryCount = 0;
    }

    void Update()
    {
        if (state == STATE.STARTED)
        {
            if (happyLaundryCount == Spawn.instance.maximumCustomers)
            {
                EndWorkShiftEarly();

                // don't run EndWorkShift
                isEndedEarly = true;
            }

            if (customerTimeRemaining > 0)
            {
                customerTimeRemaining -= Time.deltaTime;
            }
            else
            {
                Spawn.instance.SpawnCustomer();
                customerTimeRemaining = customerTimeInterval;
            }
        }

        foreach (Laundry laundry in doneLaundry.ToList())
        {
            if (laundry.doneTimeRemaining > 0)
            {
                laundry.doneTimeRemaining -= Time.deltaTime;
            }
            else
            {
                int doneLaundryIndex = activeLaundry.IndexOf(laundry);
                activeLaundry.RemoveAt(doneLaundryIndex);
                doneLaundry.Remove(laundry);
                OnLaundryRemoved.Invoke();

                if (!activeLaundry.Any())
                {
                    selectedLaundry = null;
                }
            }
        }
    }

    private void AddActiveLaundry(CustomerController customer)
    {
        Laundry newLaundry = new Laundry
        {
            customerController = customer
        };

        if (selectedLaundry is null)
        {
            newLaundry.isSelected = true;
            selectedLaundry = newLaundry;
        }
        activeLaundry.Add(newLaundry);
        customer.laundry = newLaundry;
        OnLaundrySpawned.Invoke();
    }
    private void AddTestLaundry(Laundry.STATE _state)
    {
        Laundry newLaundry = new Laundry
        {
            state = _state
        };

        if (selectedLaundry is null)
        {
            newLaundry.isSelected = true;
            selectedLaundry = newLaundry;
        }
        activeLaundry.Add(newLaundry);
        OnLaundrySpawned.Invoke();
    }

    private void SelectLeftLaundry(InputAction.CallbackContext context)
    {
        if (selectedLaundry is null)
        {
            return;
        }

        selectedLaundry.isSelected = false;

        int selectedLaundryIndex = activeLaundry.IndexOf(selectedLaundry);
        if (selectedLaundryIndex == 0)
        {
            selectedLaundryIndex = activeLaundry.Count - 1;
        }
        else
        {
            selectedLaundryIndex--;
        }

        activeLaundry[selectedLaundryIndex].isSelected = true;
        selectedLaundry = activeLaundry[selectedLaundryIndex];
        OnLaundrySelected.Invoke();
    }

    private void SelectRightLaundry(InputAction.CallbackContext context)
    {
        if (selectedLaundry is null)
        {
            return;
        }

        selectedLaundry.isSelected = false;

        int selectedLaundryIndex = activeLaundry.IndexOf(selectedLaundry);
        if (selectedLaundryIndex == activeLaundry.Count - 1)
        {
            selectedLaundryIndex = 0;
        }
        else
        {
            selectedLaundryIndex++;
        }

        activeLaundry[selectedLaundryIndex].isSelected = true;
        selectedLaundry = activeLaundry[selectedLaundryIndex];
        OnLaundrySelected.Invoke();
    }

    private void DisableSelect()
    {
        selectLeft.Disable();
        selectRight.Disable();
    }

    private void EnableSelect()
    {
        selectLeft.Enable();
        selectRight.Enable();
    }

    public List<Laundry> GetActiveLaundryList()
    {
        return activeLaundry;
    }

    public Laundry GetSelectedLaundry()
    {
        return selectedLaundry;
    }

    private void LoadDirtyLaundry(Laundry laundry)
    {
        if (selectedLaundry is null)
        {
            return;
        }

        int selectedLaundryIndex = activeLaundry.IndexOf(selectedLaundry);
        activeLaundry[selectedLaundryIndex] = laundry;

        if (selectedLaundry.state == Laundry.STATE.DIRTY)
        {
            activeLaundry[selectedLaundryIndex].state = Laundry.STATE.LOADED_WASH;
        }

        selectedLaundry = activeLaundry[selectedLaundryIndex];
    }

    private void LoadWashedLaundry()
    {
        if (selectedLaundry is null)
        {
            return;
        }

        int selectedLaundryIndex = activeLaundry.IndexOf(selectedLaundry);

        if (selectedLaundry.state == Laundry.STATE.UNLOADED_WASH)
        {
            activeLaundry[selectedLaundryIndex].state = Laundry.STATE.LOADED_DRY;
        }

        selectedLaundry = activeLaundry[selectedLaundryIndex];
    }

    private void SetDriedLaundry(Laundry laundry)
    {
        int driedLaundryIndex = activeLaundry.IndexOf(laundry);
        activeLaundry[driedLaundryIndex].state = Laundry.STATE.DRIED;
    }

    private void SetDryingLaundry(Laundry laundry)
    {
        int dryingLaundryIndex = activeLaundry.IndexOf(laundry);
        activeLaundry[dryingLaundryIndex].state = Laundry.STATE.DRYING;
    }

    private void UnloadWashedLaundry(Laundry laundry)
    {
        int unloadedLaundryIndex = activeLaundry.IndexOf(laundry);
        activeLaundry[unloadedLaundryIndex].state = Laundry.STATE.UNLOADED_WASH;
    }

    private void UnloadDriedLaundry(Laundry laundry)
    {
        int unloadedLaundryIndex = activeLaundry.IndexOf(laundry);
        activeLaundry[unloadedLaundryIndex].state = Laundry.STATE.UNLOADED_DRY;
    }

    private void FoldDriedLaundry()
    {
        if (selectedLaundry is null)
        {
            return;
        }

        int selectedLaundryIndex = activeLaundry.IndexOf(selectedLaundry);

        if (selectedLaundry.state == Laundry.STATE.UNLOADED_DRY)
        {
            activeLaundry[selectedLaundryIndex].state = Laundry.STATE.FOLDING;
        }

        selectedLaundry = activeLaundry[selectedLaundryIndex];
    }

    private void SetFoldedLaundry(Laundry laundry)
    {
        int foldedLaundryIndex = activeLaundry.IndexOf(laundry);
        activeLaundry[foldedLaundryIndex].state = Laundry.STATE.FOLDED;
    }

    private void SetDoneLaundry()
    {
        if (selectedLaundry is null)
        {
            return;
        }

        int selectedLaundryIndex = activeLaundry.IndexOf(selectedLaundry);

        if (selectedLaundry.state == Laundry.STATE.FOLDED)
        {

            activeLaundry[selectedLaundryIndex].state = Laundry.STATE.DONE;
            doneLaundry.Add(activeLaundry[selectedLaundryIndex]);
            doneLaundryCount++;
            happyLaundryCount++;
            OnScoreAdded.Invoke(1m);
        }

        selectedLaundry = activeLaundry[selectedLaundryIndex];
    }

    private void DiscardLaundry(Laundry laundry)
    {
        int laundryIndex = activeLaundry.IndexOf(laundry);
        activeLaundry[laundryIndex].state = Laundry.STATE.DISCARD;

        doneLaundry.Add(laundry);
        doneLaundryCount++;
        discardedLaundryCount++;
    }

    private void SetLaundryState(Laundry laundry, Laundry.STATE state)
    {
        int laundryIndex = activeLaundry.IndexOf(laundry);
        activeLaundry[laundryIndex].state = state;
    }
}
