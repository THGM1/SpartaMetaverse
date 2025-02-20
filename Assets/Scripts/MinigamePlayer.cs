using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePlayer : MonoBehaviour
{
    public static MinigamePlayer instance;
    Animator animator = null;
    Rigidbody2D rb = null;
    private Vector3 startPosition;
    public float jumpForce = 5f;
    public float forwardSpeed = 3f;
    public bool isDead = false;
    float deathCooldown = 0f;
    bool isJump = false;
    public int jumpCount = 0;
    bool isGrounded = true;
    private void Start()
    {
        instance = this;
        startPosition = transform.position;
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("IsDie", false);
    }
    private void Update()
    {
        if (isDead)
        {
            if (deathCooldown < 0f)
            {
                if (Input.anyKeyDown)
                {
                    //Àç½ÃÀÛ
                }
            }
            else deathCooldown -= Time.deltaTime;
        }
        else
        {
            
            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && jumpCount < 2)
            {
                rb.velocity = Vector3.zero;
                jumpCount++;
                isJump = true;
            }

        }
    }
    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = rb.velocity;
        velocity.x = forwardSpeed * GameManager.instance.level;

        if (isJump)
        {
            velocity.y += jumpForce;
            isJump = false;
        }

        rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            animator.SetBool("IsDie", true);
            isDead = true;
            deathCooldown = 1f;
            GameManager.instance.GameOver();
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }

    public void Init()
    {
        transform.position = startPosition;
        rb.velocity = Vector2.zero;
        isDead = false;
        jumpCount = 0;
        isJump = false;
        animator.SetBool("IsDie", false);
    }
}
