using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles; // ��ֹ� ������
    [SerializeField] private float spawnInterval = 2f; // ���� ����
    [SerializeField] private float widthPadding; // ��ֹ� ����
    [SerializeField] private Transform spawnPoint; // ���� ��ġ
    private Vector3 lastPosition; // ���������� ������ ��ֹ� ��ġ

    private void Start()
    {
        lastPosition = spawnPoint.position;
        StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            SetRandomPosition();
            yield return new WaitForSeconds(spawnInterval);

        }
    }
    public void SetRandomPosition()
    {
        if (obstacles.Length == 0) return;
        widthPadding =Random.Range(3f, 8f);
        int randomIndex = Random.Range(0, obstacles.Length);
        GameObject obstacle = Instantiate(obstacles[randomIndex]);
        lastPosition += new Vector3(widthPadding, 0, 0);
        obstacle.transform.position = lastPosition;

    }
}
