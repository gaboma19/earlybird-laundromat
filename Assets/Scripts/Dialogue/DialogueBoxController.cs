using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using TMPro;

public class DialogueBoxController : MonoBehaviour
{
    public static DialogueBoxController instance;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] CanvasGroup dialogueBox;
    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;
    bool skipLineTriggered;
    public PlayerInputActions playerControls;
    private InputAction interact;

    public void SkipLine(InputAction.CallbackContext context)
    {
        skipLineTriggered = true;
    }

    public void StartDialogue(DialogueAsset dialogueAsset, int startPosition, string name)
    {
        nameText.text = name;
        dialogueBox.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(RunDialogue(dialogueAsset, startPosition));
    }

    IEnumerator RunDialogue(DialogueAsset dialogueAsset, int startPosition)
    {
        string[] dialogue = dialogueAsset.dialogue;
        skipLineTriggered = false;
        OnDialogueStarted?.Invoke();
        EnableInteract();
        for (int i = startPosition; i < dialogue.Length; i++)
        {
            dialogueText.text = dialogue[i];
            while (skipLineTriggered == false)
            {
                // Wait for the current line to be skipped
                yield return null;
            }
            skipLineTriggered = false;
        }
        OnDialogueEnded?.Invoke();
        DisableInteract();
        dialogueBox.gameObject.SetActive(false);
    }

    private void EnableInteract()
    {
        interact.Enable();
        interact.performed += SkipLine;
    }

    private void DisableInteract()
    {
        interact.Disable();
        interact.performed -= SkipLine;
    }

    private void StopDialogue()
    {
        StopAllCoroutines();
        dialogueBox.gameObject.SetActive(false);
    }

    private void Awake()
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

        Timer.OnTimerEnded += StopDialogue;
    }
}
