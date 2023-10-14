using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laundry : MonoBehaviour
{
    List<Clothes> clothes = new List<Clothes>();
    public int maximumNumberOfClothes = 5;

    void Start()
    {
        int clothesCount = Random.Range(1, maximumNumberOfClothes + 1);
        for (int i = 0; i < clothesCount; i++)
        {
            clothes.Add(new Clothes());
        }
    }
}
