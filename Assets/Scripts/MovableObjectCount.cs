using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObjectCount : MonoBehaviour
{
    public int count =0 ;
    public int Count {  get { return count; } }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MovableObject"))
        {
            count++;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MovableObject"))
        {
            count--;
        }
    }
}
