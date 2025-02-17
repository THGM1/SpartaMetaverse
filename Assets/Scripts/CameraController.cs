using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    [SerializeField] private float cameraSpeed = .1f;
    [SerializeField] float playerXPos;
    [SerializeField] float playerYPos;
    Camera camera;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        camera = GetComponent<Camera>();
    }
    private void Update()
    {
        CameraMove();
        LimitCamera();
    }
    private void CameraMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal * cameraSpeed * Time.deltaTime, vertical * cameraSpeed * Time.deltaTime, 0f);
        this.transform.Translate(move);
    }

    private void LimitCamera()
    {
        Vector3 position = transform.position;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 playerPos = player.transform.position;

        position.x = Mathf.Clamp(position.x, playerPos.x - playerXPos, playerPos.x + playerXPos);
        position.y = Mathf.Clamp(position.y, playerPos.y - playerYPos, playerPos.y + playerYPos);

        transform.position = position;
    }
}
