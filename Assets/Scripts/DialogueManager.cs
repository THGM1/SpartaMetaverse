using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [SerializeField] private TextMeshPro text;

    private string[] dialogues;
    [SerializeField]private int index;
    private Action onDialogueEnd;
    public bool isTalking = false;
    public bool isEnd = true;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        // = GetComponentInChildren<TextMeshPro>();
        //text.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (text == null) return;
        if(text.IsActive() && Input.GetMouseButtonDown(0))
        {
            ShowNextDialogue();
        }
    }
    public void StartDialogue(string[] newDialogues, Action onEnd = null)
    {
        Debug.Log(text.name);
        isTalking = true;
        dialogues = newDialogues;
        index = 0;
        onDialogueEnd = onEnd;  
        text.gameObject.SetActive(true);
        ShowNextDialogue();
    }

    private void ShowNextDialogue()
    {
        if (index < dialogues.Length)
        {
 
            text.text = dialogues[index];
            index++;
        }
        else EndDialogue();
    }

    public void EndDialogue()
    {
        isEnd = true;
        isTalking = false;
        text.gameObject.SetActive(false);
        index = 0;
        onDialogueEnd?.Invoke();
    }
    public void SetText(TextMeshPro newText)
    {

    text = newText; }
}
