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
                break;
            case Clothes.TYPE.PANTS:
                break;
            case Clothes.TYPE.PANTIES:
                break;
            case Clothes.TYPE.BOXERS:
                break;
            case Clothes.TYPE.SOCKS:
                break;
        }
    }
}
