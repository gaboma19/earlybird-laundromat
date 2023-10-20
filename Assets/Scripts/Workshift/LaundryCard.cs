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
    [SerializeField] private Image washIcon;
    [SerializeField] private Image dryIcon;
    [SerializeField] private Image foldIcon;
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
                break;
            case Laundry.STATE.WASH:
                Instantiate(washIcon, iconContainer);
                break;
            case Laundry.STATE.DRY:
                Instantiate(dryIcon, iconContainer);
                break;
            case Laundry.STATE.FOLD:
                Instantiate(foldIcon, iconContainer);
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
