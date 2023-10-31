using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesWheel : MonoBehaviour
{
    [SerializeField] private Transform wheel;
    [SerializeField] private Transform clothesTemplate;
    [SerializeField] private Minigame minigame;
    public Laundry laundry;

    private void Awake()
    {
        clothesTemplate.gameObject.SetActive(false);
    }

    void Start()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in wheel)
        {
            if (child == clothesTemplate) continue;
            Destroy(child.gameObject);
        }

        List<Clothes> clothesList = laundry.GetClothesList();
        int clothesCount = clothesList.Count;

        Clothes selectedClothes = minigame.GetSelectedClothes();
        int selectedClothesIndex = clothesList.IndexOf(selectedClothes);

        List<Clothes> visibleClothes = new List<Clothes>();

        for (int i = selectedClothesIndex - 1; i <= selectedClothesIndex + 1; i++)
        {
            if (i < 0)
            {
                visibleClothes.Add(clothesList[clothesCount - 1]);
            }
            else if (i > clothesCount - 1)
            {
                visibleClothes.Add(clothesList[0]);
            }
            else
            {
                visibleClothes.Add(clothesList[i]);
            }
        }

        foreach (Clothes clothes in visibleClothes)
        {
            Transform clothesTransform = Instantiate(clothesTemplate, wheel);
            clothesTransform.gameObject.SetActive(true);
            clothesTransform.GetComponent<ClothesToken>().SetClothes(clothes);
        }
    }
}
