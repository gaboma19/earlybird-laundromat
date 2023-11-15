using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Spawn : MonoBehaviour
{
    private GameObject[] customers;
    public void SpawnCustomer()
    {
        GameObject nextCustomer = customers[Random.Range(0, customers.Length)];

        Instantiate(nextCustomer, transform.position, Quaternion.identity);
    }

    void Start()
    {
        customers = Resources.LoadAll<GameObject>("Customers");
    }

}
