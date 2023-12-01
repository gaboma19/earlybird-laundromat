using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public static Exit instance;
    [SerializeField] GameObject sprite;
    [SerializeField] Black black;
    [SerializeField] Splash splash;
    private bool isActive = false;

    public void ActivateWithDelay(float time)
    {
        Invoke("Activate", time);
    }

    public void Activate()
    {
        sprite.SetActive(true);
        isActive = true;
        if (Calendar.instance.GetDate() == 1)
        {
            Tutorial.instance.ShowTutorial(new List<string> { "Good work attendant" });
        }
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
            DestroyAllCustomers();
            Deactivate();
            Tutorial.instance.KillTutorial();

            StartCoroutine(LoadExteriorScene());
        }
    }

    private void DestroyAllCustomers()
    {
        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
        foreach (GameObject go in customers)
        {
            Destroy(go);
        }
    }

    IEnumerator LoadExteriorScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Exterior");

        // Wait until the asynchronous scene fully loads
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
    }
}
