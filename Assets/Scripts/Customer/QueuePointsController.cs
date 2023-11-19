using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QueuePointsController : MonoBehaviour
{
    public static QueuePointsController instance;
    private List<QueuePoint> queuePoints = new();

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

        foreach (Transform child in transform)
        {
            queuePoints.Add(child.GetComponent<QueuePoint>());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector2 NextAvailablePosition(CustomerController customer)
    {
        Vector2 position = queuePoints[0].transform.position;

        for (int i = 0; i < queuePoints.Count; i++)
        {
            if (queuePoints[i].isAvailable)
            {
                position = queuePoints[i].transform.position;

                queuePoints[i].isAvailable = false;
                customer.queueIndex = i;

                break;
            }
        }

        return position;
    }

    public void SetQueuePointAvailable(int index)
    {
        queuePoints[index].isAvailable = true;
    }
}
