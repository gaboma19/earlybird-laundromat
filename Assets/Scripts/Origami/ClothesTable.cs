using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesTable : MonoBehaviour
{
    [SerializeField] private Transform table;
    [SerializeField] private Transform clothesTemplate;
    [SerializeField] private Origami origami;

    private void Awake()
    {
        clothesTemplate.gameObject.SetActive(false);
    }

    void Start()
    {
        UpdateVisual();
        Origami.OnSequenceCompleted += UpdateVisual;
    }

    void UpdateVisual()
    {
        foreach (Transform child in table)
        {
            if (child == clothesTemplate) continue;
            Destroy(child.gameObject);
        }

        Clothes currentClothes = origami.GetCurrentClothes();

        Transform clothesTransform = Instantiate(clothesTemplate, table);
        clothesTransform.gameObject.SetActive(true);
        clothesTransform.GetComponent<ClothesToken>().SetClothes(currentClothes);
    }
}
