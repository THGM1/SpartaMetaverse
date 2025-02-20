using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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

    public void IncreaseScore()
    {
        score++;
        ScoreSpeed();
    }
    public void ScoreSpeed()
    {
        if (score % 10 == 0) level += .2f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        Init();
        ObstacleManager.instance.Init();
        MinigamePlayer.instance.Init();
    }

    public void Init()
    {
        score = 0;
        level = 1;
    }
}
