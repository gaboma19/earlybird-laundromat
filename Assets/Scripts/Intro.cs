using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Intro : MonoBehaviour
{
    public PlayerInputActions playerControls;
    private InputAction interact;
    [SerializeField] GameObject tutorial1;
    [SerializeField] GameObject tutorial2;

    void Start()
    {
        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Progress;
    }

    void Awake()
    {
        tutorial1.gameObject.SetActive(true);
        playerControls = new PlayerInputActions();
    }

    private void Progress(InputAction.CallbackContext context)
    {
        if (tutorial2.activeInHierarchy)
        {
            EndTutorial();
        }
        else if (tutorial1.activeInHierarchy)
        {
            tutorial1.SetActive(false);
            tutorial2.SetActive(true);
        }
    }

    private void EndTutorial()
    {
        interact.Disable();
        interact.performed -= Progress;
        gameObject.SetActive(false);
    }
}
