using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    public PlayerInputActions playerControls;
    private InputAction interact;
    private List<Transform> tutorials = new();
    private int tutorialIndex = 0;

    public void ShowTutorial(List<string> tutorialNames)
    {
        foreach (string name in tutorialNames)
        {
            tutorials.Add(transform.Find(name));
        }

        tutorials[0].gameObject.SetActive(true);

        EnableInteract();
    }

    private void EnableInteract()
    {
        interact.Enable();
        interact.performed += Progress;
    }

    private void Progress(InputAction.CallbackContext context)
    {
        tutorials[tutorialIndex].gameObject.SetActive(false);

        if (tutorialIndex != tutorials.Count - 1)
        {
            tutorials[tutorialIndex + 1].gameObject.SetActive(true);
            tutorialIndex++;
        }
        else
        {
            EndTutorial();
        }
    }

    private void EndTutorial()
    {
        tutorialIndex = 0;
        interact.Disable();
        interact.performed -= Progress;
    }

    private void KillTutorial()
    {
        tutorials[tutorialIndex].gameObject.SetActive(false);
        EndTutorial();
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

        playerControls = new PlayerInputActions();
        interact = playerControls.Player.Interact;
        Exit.OnDayEnded += KillTutorial;
    }
}
