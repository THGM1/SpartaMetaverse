using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = .1f;

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") * moveSpeed * .1f;
        float vertical = Input.GetAxisRaw("Vertical") * moveSpeed * .1f;
        if(horizontal != 0 || vertical != 0)
        {
            Vector3 move = new Vector3(horizontal, vertical, 0);
            transform.Translate(move);
        }
        
    }
}
