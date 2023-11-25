using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Spawn : MonoBehaviour
{
    public static Spawn instance;
    private GameObject[] customers;
    private int customersQueueing = 0;
    public Vector2 exitPoint;
    public int maximumCustomers = 1;
    public void SpawnCustomer()
    {
        if (customersQueueing < maximumCustomers)
        {
            GameObject nextCustomer = customers[Random.Range(0, customers.Length)];

            Instantiate(nextCustomer, transform.position, Quaternion.identity);

            customersQueueing++;
        }
    }

    public void DequeueCustomer()
    {
        customersQueueing--;
    }

    private void SetMaximumCustomers()
    {
        maximumCustomers = Calendar.instance.GetDate();
        customersQueueing = 0;

        if (maximumCustomers > 10)
        {
            maximumCustomers = 10;
        }
    }

    void Start()
    {
        customers = Resources.LoadAll<GameObject>("Customers");
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        exitPoint = transform.Find("Exit Point").position;

        Exit.OnDayStarted += SetMaximumCustomers;
    }

}
