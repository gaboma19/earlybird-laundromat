using UnityEngine;
using System.Collections;

public class ShotZone : MonoBehaviour
{
    public Shot targetShot;
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            targetShot.CutToShot();
        }
    }
}
