using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laundry
{
    private List<Clothes> clothes = new List<Clothes>();
    [SerializeField] private int maximumNumberOfClothes = 5;
    public enum STATE
    {
        DIRTY, LOADED_WASH, WASHING, WASHED, UNLOADED_WASH, LOADED_DRY, DRYING, DRIED, UNLOADED_DRY, LOADED_FOLD, FOLDING, FOLDED, DONE
    }
    public STATE state { get; set; }
    public bool isSelected { get; set; }

    public Laundry()
    {
        int clothesCount = Random.Range(1, maximumNumberOfClothes + 1);
        for (int i = 0; i < clothesCount; i++)
        {
            clothes.Add(new Clothes());
        }

        this.state = STATE.DIRTY;
    }
}
