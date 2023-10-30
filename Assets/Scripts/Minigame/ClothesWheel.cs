using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesWheel : MonoBehaviour
{
    [SerializeField] private Transform wheel;
    [SerializeField] private Transform clothesTemplate;
    public Laundry laundry;
    private float radius = 1f;

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

        int clothesCount = laundry.GetClothesList().Count;
        float angleSection = Mathf.PI * 2f / clothesCount;

        for (var i = 0; i < clothesCount; i++)
        {
            float angle = i * angleSection;
            Vector3 clothesPosition = this.transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle) * radius, 0);
            Transform clothesTransform = Instantiate(clothesTemplate, clothesPosition, Quaternion.identity);
        }
    }
}
