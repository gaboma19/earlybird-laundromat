using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        dialogueBox.gameObject.SetActive(false);
    }
    public void SkipLine()
    {
        skipLineTriggered = true;
    }
}
