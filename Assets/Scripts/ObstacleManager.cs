using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles; // ��ֹ� ������
    [SerializeField] private float spawnInterval = 2f; // ���� ����
    [SerializeField] private float widthPadding; // ��ֹ� ����
    [SerializeField] private Transform spawnPoint; // ���� ��ġ
    [SerializeField] private GameObject tilemap1;
    [SerializeField] private GameObject tilemap2;
    public static ObstacleManager instance;
    private Vector3 lastPosition; // ���������� ������ ��ֹ� ��ġ
    private Queue<GameObject> activeObstacles = new Queue<GameObject>(); // ������ ��ֹ�
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
