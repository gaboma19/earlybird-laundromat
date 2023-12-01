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
    public bool hasPatienceTutorialShown = false;

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
        if (tutorials.Count > 0)
        {
            tutorials[0].gameObject.SetActive(false);
            tutorials.RemoveAt(0);
            if (tutorials.Count > 0)
            {
                tutorials[0].gameObject.SetActive(true);
            }
        }
        else
        {
            EndTutorial();
        }
    }

    private void EndTutorial()
    {
        tutorials.Clear();
        interact.Disable();
        interact.performed -= Progress;
    }

    private void KillTutorial()
    {
        if (tutorials.Count > 0)
        {
            tutorials[0].gameObject.SetActive(false);
        }

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
