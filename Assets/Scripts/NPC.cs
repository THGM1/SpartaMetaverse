using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NPC : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // �̵� �ӵ�
    [SerializeField] private float moveDistance = 1f; // �� ���� �̵��� �Ÿ�
    [SerializeField] private float moveDelay = 1.5f; // �̵� ����
    [SerializeField] private LayerMask wallLayer;
    private float moveTimer = 0f;
    private bool isMoving = false;
    private bool isClear = false;
    public bool IsClear { get { return isClear; } }
    private Vector2 targetPosition;
    private Vector2[] directions = new Vector2[] 
    { 
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };
    [SerializeField] private Sprite[] directionSprite;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro text;
    private MovableObjectCount movableObject;
    string[] npcDialogues;
    string[] startDialgoues = { "�̴� ������ �ϰ� �ͳ�?", "���� �翡�� ��� ġ���ְ�.","Ŭ������ ���� �� �� �־�." };
    string[] clearDialogues = { "����", "���ڸ� ���� �̴ϰ����� ������ �� �־�." };

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshPro>();
        movableObject = FindObjectOfType<MovableObjectCount>();
        targetPosition = transform.position;
        npcDialogues = startDialgoues;
    }
    private void Update()
    {
        if (DialogueManager.instance.isTalking) return;

        moveTimer += Time.deltaTime;
        if (!isMoving && moveTimer >= moveDelay) // �̵� ������ ������ ���� �̵� �õ�
        {
            SetDirection();
            moveTimer = 0f; // Ÿ�̸� �ʱ�ȭ
        }

        if (isMoving)
        {
            MoveToTarget();
        }
        Request();
        if (!isClear) npcDialogues = startDialgoues;
        else npcDialogues = clearDialogues;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !DialogueManager.instance.isTalking)
        {
            DialogueManager.instance.SetText(text);
            DialogueManager.instance.StartDialogue(npcDialogues, () => DialogueManager.instance.isTalking = false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && DialogueManager.instance.isTalking)
        {
            DialogueManager.instance.EndDialogue();
        }
    }
    private void SetDirection()
    {
        int attempts = 0;
        while (attempts < 10) // 10������ �õ�
        {
            int directionIndex = Random.Range(0, directions.Length);
            Vector2 randomDirection = directions[directionIndex];
            Vector2 newPosition = (Vector2)transform.position + randomDirection * moveDistance;
            Debug.DrawRay(transform.position, randomDirection * moveDistance, Color.red, 0.5f);
            if (!Physics2D.Raycast(transform.position, randomDirection, moveDistance* 2, wallLayer))
            {
                targetPosition = newPosition;
                ChangeSprite(directionIndex); // ��������Ʈ ����
                isMoving = true;
                return;
            }
            attempts++;
        }
    }

    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
        {
            isMoving = false;
        }
    }

    private void ChangeSprite(int directionIndex)
    {
        if (spriteRenderer != null)
        {
            if (directionIndex == 3)
            {
                spriteRenderer.sprite = directionSprite[2];
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.sprite = directionSprite[directionIndex];
                if (directionIndex == 2) spriteRenderer.flipX = false;
            }
        }
    }

    private void Request()
    {
        if (movableObject.Count == 0)
        {
            //�̴ϰ��� Ȱ��ȭ
            npcDialogues = clearDialogues;
            isClear = true;
        }
        else isClear = false;
    }

}
