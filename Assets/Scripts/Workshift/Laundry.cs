using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laundry
{
    public List<Clothes> clothes = new List<Clothes>();
    [SerializeField] private int maximumNumberOfClothes = 8;
    public enum STATE
    {
        DIRTY, LOADED_WASH, WASHING, WASHED, UNLOADED_WASH, LOADED_DRY, DRYING, DRIED, UNLOADED_DRY, FOLDING, FOLDED, DONE, DISCARD
    }
    public STATE state { get; set; }
    public bool isSelected { get; set; }
    public float doneTimeRemaining = 2f;

    public Laundry()
    {
        int clothesCount = Random.Range(4, maximumNumberOfClothes + 1);
        for (int i = 0; i < clothesCount; i++)
        {
            Clothes newClothes = new Clothes();
            clothes.Add(newClothes);

            if (newClothes.type == Clothes.TYPE.SOCKS)
            {
                clothes.Add(newClothes);
            }
        }

        this.state = STATE.DIRTY;
    }

    public void SetClothesToReady()
    {
        foreach (Clothes c in clothes)
        {
            c.state = Clothes.STATE.READY;
        }
    }
}
