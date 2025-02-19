using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private float time;
    public float level = 1;

    public int score = 0;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Time.timeScale = 0f;
    }
    private void Update()
    {
        time += Time.deltaTime;
        UIManager.instance.DisplayScore();
    }
    public void StartGame()
    {
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        UIManager.instance.GameOver();
    }

}
