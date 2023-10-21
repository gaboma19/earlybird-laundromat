using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaundryGrid : MonoBehaviour
{
    [SerializeField] private Transform grid;
    [SerializeField] private Transform laundryTemplate;

    private void Awake()
    {
        laundryTemplate.gameObject.SetActive(false);
    }

    private void UpdateVisual()
    {
        foreach (Transform child in grid)
        {
            if (child == laundryTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Laundry laundry in Workshift.instance.GetActiveLaundryList())
        {
            Transform laundryTransform = Instantiate(laundryTemplate, grid);
            laundryTransform.gameObject.SetActive(true);
            laundryTransform.GetComponent<LaundryCard>().SetLaundry(laundry);
        }
    }

    void Start()
    {
        UpdateVisual();

        Workshift.OnLaundrySpawned += UpdateVisual;
        Workshift.OnLaundrySelected += UpdateVisual;
        LoadedWash.OnLoadDirtyLaundry += UpdateVisual;
        OnWash.OnLaundryWashed += (_) => UpdateVisual();
        OnWash.OnLaundryWashing += UpdateVisual;
    }

}
