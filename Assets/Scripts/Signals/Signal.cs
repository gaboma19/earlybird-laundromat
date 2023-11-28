using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour
{
    [SerializeField] GameObject[] allArrows;
    SignalZone.ZONE currentZone;
    [SerializeField] GameObject[] currentArrows;
    public void SetSignals(SignalZone.ZONE zone, GameObject[] arrows)
    {
        DeactivateAllSignals();

        Laundry selectedLaundry = Workshift.instance.GetSelectedLaundry();
        if (selectedLaundry != null)
        {
            Laundry.STATE state = selectedLaundry.state;
            currentZone = zone;
            currentArrows = arrows;

            switch (zone)
            {
                case SignalZone.ZONE.FRONT_DESK:
                    if (state == Laundry.STATE.DIRTY ||
                        state == Laundry.STATE.LOADED_WASH ||
                        state == Laundry.STATE.WASHING ||
                        state == Laundry.STATE.WASHED ||
                        state == Laundry.STATE.UNLOADED_WASH ||
                        state == Laundry.STATE.LOADED_DRY ||
                        state == Laundry.STATE.DRYING ||
                        state == Laundry.STATE.DRIED)
                    {
                        ActivateArrow("Arrow Right");
                    }
                    else if (state == Laundry.STATE.FOLDING ||
                             state == Laundry.STATE.UNLOADED_DRY)
                    {
                        ActivateArrow("Arrow Up");
                    }
                    break;

                case SignalZone.ZONE.WASHING_MACHINES:
                    if (state == Laundry.STATE.UNLOADED_WASH ||
                        state == Laundry.STATE.LOADED_DRY ||
                        state == Laundry.STATE.DRYING ||
                        state == Laundry.STATE.DRIED ||
                        state == Laundry.STATE.UNLOADED_DRY ||
                        state == Laundry.STATE.FOLDING)
                    {
                        ActivateArrow("Arrow Up");
                    }
                    else if (state == Laundry.STATE.FOLDED)
                    {
                        ActivateArrow("Arrow Left");
                    }
                    break;

                case SignalZone.ZONE.DRYERS:
                    if (state == Laundry.STATE.DIRTY ||
                        state == Laundry.STATE.LOADED_WASH ||
                        state == Laundry.STATE.WASHING ||
                        state == Laundry.STATE.WASHED)
                    {
                        ActivateArrow("Arrow Down");
                    }
                    else if (state == Laundry.STATE.UNLOADED_DRY ||
                             state == Laundry.STATE.FOLDING ||
                             state == Laundry.STATE.FOLDED)
                    {
                        ActivateArrow("Arrow Left");
                    }
                    break;

                case SignalZone.ZONE.FOLDING_TABLES:
                    if (state == Laundry.STATE.DIRTY ||
                        state == Laundry.STATE.LOADED_WASH ||
                        state == Laundry.STATE.WASHING ||
                        state == Laundry.STATE.WASHED ||
                        state == Laundry.STATE.FOLDED)
                    {
                        ActivateArrow("Arrow Down");
                    }
                    else if (state == Laundry.STATE.UNLOADED_WASH ||
                             state == Laundry.STATE.LOADED_DRY ||
                             state == Laundry.STATE.DRYING ||
                             state == Laundry.STATE.DRIED)
                    {
                        ActivateArrow("Arrow Right");
                    }
                    break;
            }
        }
    }

    private void SetSignals()
    {
        SetSignals(currentZone, currentArrows);
    }

    private GameObject GetObjectByName(GameObject[] objects, string name)
    {
        foreach (GameObject o in objects)
        {
            if (o.name == name)
            {
                return o;
            }
        }

        return null;
    }

    private void ActivateArrow(string name)
    {
        GameObject arrow = GetObjectByName(currentArrows, name);
        arrow.SetActive(true);
    }

    private void DeactivateAllSignals()
    {
        foreach (GameObject o in allArrows)
        {
            o.SetActive(false);
        }
    }

    void Awake()
    {
        Workshift.OnLaundrySelected += SetSignals;
        Workshift.OnLaundrySpawned += SetSignals;
        Workshift.OnLaundryRemoved += SetSignals;
        DoneWash.OnUnloadWasher += (_) => SetSignals();
        DoneDry.OnUnloadDryer += (_) => SetSignals();
        InUseFold.OnLaundryFolded += (_) => SetSignals();
        Timer.OnTimerEnded += DeactivateAllSignals;

        currentZone = SignalZone.ZONE.FRONT_DESK;
    }
}
