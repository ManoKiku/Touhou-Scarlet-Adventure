using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public Action onDialogueEnd;

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;
    private Queue<DialogueLine> lines;
    public bool isDialogueActive = false;
    public float typingSpeed = 0.2f;
    public Animator animator;

    private void Awake()
    {
        Instance = this;

        lines = new Queue<DialogueLine>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;

        animator.Play("Show");

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        onDialogueEnd?.Invoke();
        isDialogueActive = false;
        animator.Play("Hide");
    }
}