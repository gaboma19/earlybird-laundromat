using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject muteButton;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject unmuteButton;
    public void PlayGame()
    {
        SceneManager.LoadScene("Laundromat");
    }

    public void SelectMuteButton()
    {
        eventSystem.SetSelectedGameObject(muteButton);
    }

    public void SelectPlayButton()
    {
        eventSystem.SetSelectedGameObject(playButton);
    }

    public void SelectUnmuteButton()
    {
        eventSystem.SetSelectedGameObject(unmuteButton);
    }
}
