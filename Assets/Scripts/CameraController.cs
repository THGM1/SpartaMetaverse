using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    [SerializeField] private float cameraSpeed = .1f;
    [SerializeField] float playerXPos;
    [SerializeField] float playerYPos;
    [SerializeField] Vector2 minPos;
    [SerializeField] Vector2 maxPos;

    Camera camera;

    private void Awake()
    {
        instance = this;
        LimitCamera();
    }
    private void Start()
    {
        camera = GetComponent<Camera>();
    }
    private void Update()
    {
        if (Time.timeScale > 0)
        {
            CameraMove();
            LimitCamera();
        }
    }
    private void FixedUpdate()
    {
        if (Time.timeScale == 0)
        {
            CameraMove();
            LimitCamera();
        }
    }
    private void CameraMove()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 targetPosition = player.transform.position;
        targetPosition.z = -1;
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }

    private void LimitCamera()
    {
        Vector3 position = transform.position;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 playerPos = player.transform.position;

        //플레이어 제한
        position.x = Mathf.Clamp(position.x, playerPos.x - playerXPos, playerPos.x + playerXPos);
        position.y = Mathf.Clamp(position.y, playerPos.y - playerYPos, playerPos.y + playerYPos);
        //맵 제한
        if (SceneManager.GetActiveScene().name == "MainScene")
            position.x = Mathf.Clamp(position.x, minPos.x, maxPos.x);
        
        position.y = Mathf.Clamp(position.y, minPos.y, maxPos.y);
        transform.position = position;
    }
}
