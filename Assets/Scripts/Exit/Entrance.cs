using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    public static Entrance instance;
    private bool isActive = false;
    [SerializeField] GameObject sprite;

    public void Activate()
    {
        sprite.SetActive(true);
        isActive = true;
    }

    public void Deactivate()
    {
        sprite.SetActive(false);
        isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive)
        {
            Deactivate();
            StartCoroutine(LoadLaundromatScene());
        }
    }

    IEnumerator LoadLaundromatScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Laundromat");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        Apartment.OnDayStarted += Activate;
    }

    void OnDisable()
    {
        Apartment.OnDayStarted -= Activate;
    }
}
