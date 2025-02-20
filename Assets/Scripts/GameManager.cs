using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float level = 1;
    private const string BestScoreKey = "BestScore";
    public int score = 0;
    public int bestScore = 0;
    public bool isPause = false;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Time.timeScale = 0f;
        isPause = true;
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainScene") Time.timeScale = 1f;
        else UIManager.instance.DisplayScore();
    }
    public void StartGame()
    {
        Time.timeScale = 1f;
        isPause = false;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        isPause = true;
        UpdateScore();
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
        isPause = false;
        Init();
        ObstacleManager.instance.Init();
        MinigamePlayer.instance.Init();
    }

    public void Init()
    {
        score = 0;
        level = 1;
    }

    void UpdateScore()
    {
        if(bestScore < score)
        {
            bestScore = score;
            PlayerPrefs.SetInt(BestScoreKey, bestScore);
        }
    }
}
