using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Workshift : MonoBehaviour
{
    public static Workshift instance;
    private List<Laundry> activeLaundry = new List<Laundry>();
    [SerializeField] private Timer timer;
    public float customerTimeInterval;
    [SerializeField] private GameObject customer;
    public static event Action OnLaundrySpawned;
    [SerializeField] private GameObject spawnPoint;
    public PlayerInputActions playerControls;
    private InputAction selectLeft;
    private InputAction selectRight;
    public static event Action OnLaundrySelected;
    private Laundry selectedLaundry;

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

        RegisterController.OnWorkshiftStart += StartWorkShift;
        Timer.OnTimerEnded += EndWorkShift;
        Order.OnOrderPlaced += AddActiveLaundry;
        LoadedWash.OnLoadDirtyLaundry += LoadDirtyLaundry;
        OnWash.OnLaundryWashing += SetWashingLaundry;
        OnWash.OnLaundryWashed += SetWashedLaundry;

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
        LoadedWash.OnLoadDirtyLaundry -= LoadDirtyLaundry;
        OnWash.OnLaundryWashing -= SetWashingLaundry;
        OnWash.OnLaundryWashed -= SetWashedLaundry;
    }

    private void StartWorkShift()
    {
        timer.timerIsRunning = true;
        StartCoroutine(SpawnCustomer());
    }

    private void EndWorkShift()
    {
        StopCoroutine(SpawnCustomer());

        // show a "shift ended" UI element
    }

    IEnumerator SpawnCustomer()
    {
        while (true)
        {
            Instantiate(customer, spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(customerTimeInterval);
        }
    }

    private void AddActiveLaundry()
    {
        Laundry newLaundry = new Laundry();

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

    public List<Laundry> GetActiveLaundryList()
    {
        return activeLaundry;
    }

    public Laundry GetSelectedLaundry()
    {
        return selectedLaundry;
    }

    private void SetWashingLaundry()
    {
        if (selectedLaundry is null)
        {
            return;
        }

        int selectedLaundryIndex = activeLaundry.IndexOf(selectedLaundry);

        if (selectedLaundry.state == Laundry.STATE.LOADED_WASH)
        {
            activeLaundry[selectedLaundryIndex].state = Laundry.STATE.WASHING;
        }

        selectedLaundry = activeLaundry[selectedLaundryIndex];
    }

    private void LoadDirtyLaundry()
    {
        if (selectedLaundry is null)
        {
            return;
        }

        int selectedLaundryIndex = activeLaundry.IndexOf(selectedLaundry);

        if (selectedLaundry.state == Laundry.STATE.DIRTY)
        {
            activeLaundry[selectedLaundryIndex].state = Laundry.STATE.LOADED_WASH;
        }
        else
        {
            // show dialogue "selected laundry is not dirty"
        }

        selectedLaundry = activeLaundry[selectedLaundryIndex];
    }

    private void SetWashedLaundry(Laundry laundry)
    {
        int washedLaundryIndex = activeLaundry.IndexOf(laundry);
        activeLaundry[washedLaundryIndex].state = Laundry.STATE.WASHED;
    }

    // keeps track of points / score / currency

    // play “getting it done” - game loop.

}
