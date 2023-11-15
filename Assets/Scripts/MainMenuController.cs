using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject muteButton;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject unmuteButton;
    [SerializeField] private GameObject backButton;
    public PlayerInputActions playerControls;
    private InputAction navigate;
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

    public void SelectBackButton()
    {
        eventSystem.SetSelectedGameObject(backButton);
    }

    private GameObject GetFirstActiveButton()
    {
        GameObject firstActiveButton = null;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeSelf == true)
            {
                firstActiveButton = gameObject.transform.GetChild(i).gameObject;
                break;
            }
        }

        return firstActiveButton;
    }

    void Update()
    {
        if (eventSystem.currentSelectedGameObject == null)
        {
            Vector2 navigateDirection = navigate.ReadValue<Vector2>();

            if (!Mathf.Approximately(navigateDirection.x, 0.0f) || !Mathf.Approximately(navigateDirection.y, 0.0f))
            {
                eventSystem.SetSelectedGameObject(GetFirstActiveButton());
            }
        }
    }

    void Start()
    {
        playerControls = new PlayerInputActions();
        navigate = playerControls.UI.Navigate;
        navigate.Enable();
    }
}
