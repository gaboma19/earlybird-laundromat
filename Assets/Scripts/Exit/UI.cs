using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject UIPrefab;
    void Start()
    {
        GameObject existingUI = GameObject.FindGameObjectWithTag("UI");
        if (existingUI == null)
        {
            Instantiate(UIPrefab);
        }
    }
}
