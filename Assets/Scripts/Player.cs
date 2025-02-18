using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = .1f;
    private Animator animator;
    private bool isMoving;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float vertical = Input.GetAxisRaw("Vertical") * moveSpeed;

        isMoving = (horizontal != 0 || vertical != 0);

        //이동 애니메이션
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("Horizontal", isMoving ? horizontal : 0);
        animator.SetFloat("Vertical", isMoving ? vertical : 0);

        transform.position += new Vector3(horizontal, vertical) * Time.deltaTime;
    }
    
}
