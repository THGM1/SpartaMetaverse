using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedObject : MonoBehaviour
{
    Transform player;
    public float fixedPosition;
    private void Start()
    {
        player = GetComponentInParent<Transform>();
    }
    private void Update()
    {
        transform.position = new Vector3(player.position.x, fixedPosition, transform.position.z);
    }

}
