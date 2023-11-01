using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    public static Minigame instance;
    private Clothes selectedClothes;
    [SerializeField] private ClothesWheel clothesWheel;
    [SerializeField] private CanvasGroup minigame;
    public void Open()
    {
        minigame.gameObject.SetActive(true);
    }

    public void Close()
    {
        minigame.gameObject.SetActive(false);
    }

    public Clothes GetSelectedClothes()
    {
        return selectedClothes;
    }

    public void SeparateClothes(Laundry laundry)
    {
        clothesWheel.laundry = laundry;

        Open();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
