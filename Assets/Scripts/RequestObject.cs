using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RequestObject : MonoBehaviour
{
    NPC npc;
    public string[] dialogues;
    TextMeshPro text;
    bool isEnd = false;
    private void Start()
    {
        npc = FindAnyObjectByType<NPC>();   
        text = GetComponentInChildren<TextMeshPro>();

    }
    private void Update()
    {
        if (isEnd && Input.GetMouseButtonDown(0))
            SceneManager.LoadScene("GameScene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (npc.IsClear && collision.gameObject.CompareTag("Player") && !DialogueManager.instance.isTalking)
        {
            DialogueManager.instance.SetText(text);
            DialogueManager.instance.StartDialogue(dialogues, () => DialogueManager.instance.isTalking = false);
            if (DialogueManager.instance.isEnd) isEnd = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (npc.IsClear && collision.gameObject.CompareTag("Player") && DialogueManager.instance.isTalking)
        {
            DialogueManager.instance.EndDialogue();
        }
    }
}
