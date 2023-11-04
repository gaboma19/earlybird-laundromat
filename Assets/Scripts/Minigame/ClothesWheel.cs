using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesWheel : MonoBehaviour
{
    [SerializeField] private Transform wheel;
    [SerializeField] private Transform clothesTemplate;
    [SerializeField] private Minigame minigame;
    private Transform firstClothesTransform;
    public void AnimateLoad()
    {
        if (firstClothesTransform != null)
        {
            firstClothesTransform.GetComponent<ClothesToken>().AnimateLoad();
        }
    }
    public void AnimateKeep()
    {
        if (firstClothesTransform != null)
        {
            firstClothesTransform.GetComponent<ClothesToken>().AnimateKeep();
        }
    }
    private void Awake()
    {
        clothesTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        UpdateVisual();

        UpAnimationEnd.OnClothesLoaded += UpdateVisual;
        DownAnimationEnd.OnClothesKept += UpdateVisual;
        Minigame.OnMinigameStarted += UpdateVisual;
    }

    private void UpdateVisual()
    {
        foreach (Transform child in wheel)
        {
            if (child == clothesTemplate) continue;
            Destroy(child.gameObject);
        }

        List<Clothes> clothesList = minigame.GetReadyClothes();
        Debug.Log(clothesList.Count + " clothes in wheel");

        if (clothesList != null)
        {
            for (int i = 0; i < clothesList.Count; i++)
            {
                Transform clothesTransform = Instantiate(clothesTemplate, wheel);
                clothesTransform.gameObject.SetActive(true);
                clothesTransform.GetComponent<ClothesToken>().SetClothes(clothesList[i]);

                if (i == 0)
                {
                    firstClothesTransform = clothesTransform;
                }
            }
        }
    }
}
