using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LaundryCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI laundryText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Image dirtyIcon;
    [SerializeField] private Image washLoadedIcon;
    [SerializeField] private Image washingIcon;
    [SerializeField] private Image washedIcon;
    [SerializeField] private Image washUnloadedIcon;
    [SerializeField] private Image dryLoadedIcon;
    [SerializeField] private Image dryingIcon;
    [SerializeField] private Image driedIcon;
    [SerializeField] private Image dryUnloadedIcon;
    [SerializeField] private Image foldingIcon;
    [SerializeField] private Image foldedIcon;
    [SerializeField] private Image doneIcon;
    [SerializeField] private Image selectionIndicator;
    public void SetLaundry(Laundry laundry)
    {
        foreach (Transform child in iconContainer)
        {
            Destroy(child.gameObject);
        }

        switch (laundry.state)
        {
            case Laundry.STATE.DIRTY:
                Instantiate(dirtyIcon, iconContainer);
                laundryText.text = "Dirty";
                break;
            case Laundry.STATE.LOADED_WASH:
                Instantiate(washLoadedIcon, iconContainer);
                laundryText.text = "Loaded";
                break;
            case Laundry.STATE.WASHING:
                Instantiate(washingIcon, iconContainer);
                laundryText.text = "Washing";
                break;
            case Laundry.STATE.WASHED:
                Instantiate(washedIcon, iconContainer);
                laundryText.text = "Washed";
                break;
            case Laundry.STATE.UNLOADED_WASH:
                Instantiate(washUnloadedIcon, iconContainer);
                laundryText.text = "Unloaded";
                break;
            case Laundry.STATE.LOADED_DRY:
                Instantiate(dryLoadedIcon, iconContainer);
                laundryText.text = "Loaded";
                break;
            case Laundry.STATE.DRYING:
                Instantiate(dryingIcon, iconContainer);
                laundryText.text = "Drying";
                break;
            case Laundry.STATE.DRIED:
                Instantiate(driedIcon, iconContainer);
                laundryText.text = "Dried";
                break;
            case Laundry.STATE.UNLOADED_DRY:
                Instantiate(dryUnloadedIcon, iconContainer);
                laundryText.text = "Unloaded";
                break;
            case Laundry.STATE.FOLDING:
                Instantiate(foldingIcon, iconContainer);
                laundryText.text = "Folding";
                break;
            case Laundry.STATE.FOLDED:
                Instantiate(foldedIcon, iconContainer);
                laundryText.text = "Folded";
                break;
            case Laundry.STATE.DONE:
                Instantiate(doneIcon, iconContainer);
                laundryText.text = "Done";
                break;
        }

        if (laundry.isSelected)
        {
            selectionIndicator.gameObject.SetActive(true);
        }
        else
        {
            selectionIndicator.gameObject.SetActive(false);
        }
    }
}
