using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoneCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI counterText;
    int doneLaundry;
    int date;

    void Update()
    {
        doneLaundry = Workshift.instance.GetDoneLaundryCount();
        date = Calendar.instance.GetDate();
        counterText.text = doneLaundry + " / " + date;
    }
}
