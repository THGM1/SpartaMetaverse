using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform carryPosition;
    private GameObject carriedObject;
    private GameObject nearbyObject;
    [SerializeField] private float moveSpeed = .1f;
    private Animator animator;
    private bool isMoving;
    private Vector2 lastDirection = Vector2.down;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Move();
        HandleObjectPickup();
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovableObject") && carriedObject == null)
        {
            nearbyObject = collision.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovableObject"))
        {
            nearbyObject = null;
        }
    }

    private void PickUp()
    {
        if (nearbyObject != null)
        {
            carriedObject = nearbyObject;
            carriedObject.transform.position = carryPosition.position;
            carriedObject.transform.SetParent(carryPosition);
            carriedObject.GetComponent<Rigidbody2D>().isKinematic = true;
            nearbyObject = null;
        }
    }

    private void DropObject()
    {
        carriedObject.transform.SetParent(null);
        carriedObject.GetComponent<Rigidbody2D>().isKinematic=false;

        Vector2 dropPosition = (Vector2)transform.position + lastDirection;

        if (!Physics2D.OverlapCircle(dropPosition, 0.5f, LayerMask.GetMask("Wall")))
        {
            carriedObject.transform.position = dropPosition;
        }
        else
        {
            carriedObject.transform.position = transform.position;
        }
        carriedObject = null;
    }

    private void HandleObjectPickup()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (carriedObject == null)
            {
                PickUp();
            }
            else DropObject();
        }
    }

}
