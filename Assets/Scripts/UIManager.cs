using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI bestScore;
    [SerializeField] TextMeshProUGUI currentScore;
    public static UIManager instance;
    private int scoreTxt;
    private void Start()
    {
        instance = this;
        panel.SetActive(true);
        bestScore.gameObject.SetActive(true);
        currentScore.gameObject.SetActive(false);
    }
    public void panelActive(bool active)
    {
        panel.SetActive(active);
        bestScore.gameObject.SetActive(active);
        currentScore.gameObject.SetActive(active);
    }

    public void StartGame()
    {
        GameManager.instance.StartGame();
        panel.SetActive(false);
        bestScore.gameObject.SetActive(false);
        currentScore.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        panelActive(true);
    }

    public void DisplayScore()
    {
        currentScore.gameObject.SetActive(true);
        currentScore.text = "현재 점수: " + GameManager.instance.score.ToString();
    }

}
