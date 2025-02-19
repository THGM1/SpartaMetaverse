using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePlayer : MonoBehaviour
{
    Animator animator = null;
    Rigidbody2D rb = null;

    public float jumpForce = 5f;
    public float forwardSpeed = 3f;
    public bool isDead = false;
    float deathCooldown = 0f;
    bool isJump = false;

    private void Start()
    {
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
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                isJump = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = rb.velocity;
        velocity.x = forwardSpeed;
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
        }
    }
}
