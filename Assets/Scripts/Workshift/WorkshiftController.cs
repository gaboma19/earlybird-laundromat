using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Workshift : MonoBehaviour
{
    List<Laundry> activeLaundry = new List<Laundry>();
    public Timer timer;
    public float customerTimeInterval;
    public GameObject customer;

    private void Awake()
    {
        RegisterController.OnWorkshiftStart += StartWorkShift;
        Timer.OnTimerEnded += EndWorkShift;
    }

    void StartWorkShift()
    {
        timer.timerIsRunning = true;
        StartCoroutine(SpawnCustomer());
    }

    void EndWorkShift()
    {
        StopCoroutine(SpawnCustomer());

        // show a "shift ended" UI element
    }

    IEnumerator SpawnCustomer()
    {
        Instantiate(customer, new Vector3(0, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(customerTimeInterval);
    }



    // spawn a customer every t seconds

    // keeps track of customer Laundry objects

    // keeps track of points / score / currency

    // calls UI controller for displaying Laundry cards, timer, and score counter

    // play “getting it done” - game loop.

}
