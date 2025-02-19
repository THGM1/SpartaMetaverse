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
    private Vector3 lastPosition; // 마지막으로 생성된 장애물 위치
    private Queue<GameObject> activeObstacles = new Queue<GameObject>(); // 생성된 장애물
    private int maxObstacles = 7;
    public int count = 0;
    private void Start()
    {
        lastPosition = spawnPoint.position;
        for(int i = 0; i < maxObstacles; i++)
        {
            SetRandomPosition();
        }
    }

    public void SetRandomPosition()
    {
        if (obstacles.Length == 0 || count >= maxObstacles) return;
        widthPadding =Random.Range(3f, 8f);
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
    }


}
