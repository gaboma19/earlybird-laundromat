using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothesToken : MonoBehaviour
{
    [SerializeField] private Image shirtImage;
    [SerializeField] private Image pantsImage;
    [SerializeField] private Image pantiesImage;
    [SerializeField] private Image boxersImage;
    [SerializeField] private Image socksImage;

    public void SetClothes(Clothes clothes)
    {
        switch (clothes.type)
        {
            case Clothes.TYPE.SHIRT:
                InstantiateShirt(clothes);
                break;
            case Clothes.TYPE.PANTS:
                InstantiatePants(clothes);
                break;
            case Clothes.TYPE.PANTIES:
                InstantiatePanties(clothes);
                break;
            case Clothes.TYPE.BOXERS:
                InstantiateBoxers(clothes);
                break;
            case Clothes.TYPE.SOCKS:
                InstantiateSocks(clothes);
                break;
        }
    }

    private void InstantiateShirt(Clothes clothes)
    {
        switch (clothes.color)
        {
            case Clothes.COLOR.LIGHT:
                Instantiate(shirtImage, this.transform);
                break;
            case Clothes.COLOR.DARK:
                Instantiate(shirtImage, this.transform);
                break;
        }
    }

    private void InstantiatePants(Clothes clothes)
    {
        switch (clothes.color)
        {
            case Clothes.COLOR.LIGHT:
                Instantiate(pantsImage, this.transform);
                break;
            case Clothes.COLOR.DARK:
                Instantiate(pantsImage, this.transform);
                break;
        }
    }

    private void InstantiatePanties(Clothes clothes)
    {
        switch (clothes.color)
        {
            case Clothes.COLOR.LIGHT:
                Instantiate(pantiesImage, this.transform);
                break;
            case Clothes.COLOR.DARK:
                Instantiate(pantiesImage, this.transform);
                break;
        }
    }

    private void InstantiateBoxers(Clothes clothes)
    {
        switch (clothes.color)
        {
            case Clothes.COLOR.LIGHT:
                Instantiate(boxersImage, this.transform);
                break;
            case Clothes.COLOR.DARK:
                Instantiate(boxersImage, this.transform);
                break;
        }
    }

    private void InstantiateSocks(Clothes clothes)
    {
        switch (clothes.color)
        {
            case Clothes.COLOR.LIGHT:
                Instantiate(socksImage, this.transform);
                break;
            case Clothes.COLOR.DARK:
                Instantiate(socksImage, this.transform);
                break;
        }
    }
}
