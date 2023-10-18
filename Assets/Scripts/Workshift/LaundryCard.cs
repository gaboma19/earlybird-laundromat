using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaundryCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI laundryText;
    private Image laundryIcon;
    [SerializeField] private Image dirtyIcon;
    [SerializeField] private Image washIcon;
    [SerializeField] private Image dryIcon;
    [SerializeField] private Image foldIcon;

    public void SetLaundry(Laundry laundry)
    {
        switch (laundry.state)
        {
            case Laundry.STATE.DIRTY:
                laundryIcon = dirtyIcon;
                break;
            case Laundry.STATE.WASH:
                laundryIcon = washIcon;
                break;
            case Laundry.STATE.DRY:
                laundryIcon = dryIcon;
                break;
            case Laundry.STATE.FOLD:
                laundryIcon = foldIcon;
                break;
        }
    }
}
