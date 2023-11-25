using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private List<IInteractable> _interactablesInRange = new List<IInteractable>();
    public PlayerInputActions playerControls;
    private InputAction interact;

    private void Awake()
    {
        playerControls = new PlayerInputActions();

        Minigame.OnMinigameStarted += DisableInteract;
        Minigame.OnMinigameEnded += (_) => EnableInteract();
        Minigame.OnMinigameKilled += EnableInteract;
        Origami.OnOrigamiStarted += DisableInteract;
        Origami.OnOrigamiEnded += (_) => EnableInteract();
        Origami.OnOrigamiKilled += EnableInteract;
        DialogueBoxController.OnDialogueStarted += DisableInteract;
        DialogueBoxController.OnDialogueEnded += EnableInteract;
    }

    private void OnEnable()
    {
        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
    }

    private void OnDisable()
    {
        interact.Disable();
    }

    private void DisableInteract()
    {
        interact.Disable();
    }

    private void EnableInteract()
    {
        interact.Enable();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (_interactablesInRange.Count > 0)
        {
            var interactable = _interactablesInRange[0];
            interactable.Interact();
            if (!interactable.CanInteract())
            {
                _interactablesInRange.Remove(interactable);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null && interactable.CanInteract())
        {
            _interactablesInRange.Add(interactable);

            if (_interactablesInRange.Count < 2)
            {
                interactable.ShowInputPrompt();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            if (_interactablesInRange.Contains(interactable))
            {
                _interactablesInRange.Remove(interactable);
                interactable.HideInputPrompt();
            }
        }
    }
}
