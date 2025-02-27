using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles; // 장애물 프리팹
    [SerializeField] private float spawnInterval = 2f; // 생성 간격
    [SerializeField] private float widthPadding; // 장애물 간격
    [SerializeField] private Transform spawnPoint; // 생성 위치
    [SerializeField] private GameObject tilemap1;
    [SerializeField] private GameObject tilemap2;
    public static ObstacleManager instance;
    private Vector3 lastPosition; // 마지막으로 생성된 장애물 위치
    private Queue<GameObject> activeObstacles = new Queue<GameObject>(); // 생성된 장애물
    private int maxObstacles = 7;
    public int count = 0;
    private void Start()
    {
        instance = this;    
        lastPosition = spawnPoint.position;
        for(int i = 0; i < maxObstacles; i++)
        {
            SetRandomPosition();
        }
    }

    public void SetRandomPosition()
    {
        if (obstacles.Length == 0 || count >= maxObstacles) return;
        widthPadding =Random.Range(5f, 8f);
        int randomIndex = Random.Range(0, obstacles.Length);
        GameObject obstacle = Instantiate(obstacles[randomIndex]);

        lastPosition += new Vector3(widthPadding, 0, 0);
        obstacle.transform.position = lastPosition;
        activeObstacles.Enqueue(obstacle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
            activeObstacles.Dequeue();
            SetRandomPosition();

        }
        else if (collision.CompareTag("Ground"))
        {
            Vector3 position = collision.transform.position;
            Vector3 newPosition = position += new Vector3(48f, 0, 0);
            collision.transform.position = newPosition;
        }
    }

    public void Init()
    {
        while (activeObstacles.Count > 0)
        {
            Destroy(activeObstacles.Dequeue());
        }

        lastPosition = spawnPoint.position;
        count = 0;
        for (int i = 0; i < maxObstacles; i++)
        {
            SetRandomPosition();
        }
        tilemap1.transform.position = new Vector3(0, 0);
        tilemap2.transform.position = new Vector3(24, 0);
    }

}
