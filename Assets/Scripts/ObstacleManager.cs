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
    private Vector3 lastPosition; // ���������� ������ ��ֹ� ��ġ
    private Queue<GameObject> activeObstacles = new Queue<GameObject>(); // ������ ��ֹ�
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
