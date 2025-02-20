using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI bestScoreTxt;
    [SerializeField] TextMeshProUGUI currentScoreTxt;
    [SerializeField] TextMeshProUGUI gameOverTxt;
    [SerializeField] TextMeshProUGUI infoTxt;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button startBtn;

    public static UIManager instance;

    private void Start()
    {
        instance = this;
        PanelActive(true);
        gameOverTxt.gameObject.SetActive(false);
        restartBtn.gameObject.SetActive(false);
        startBtn.gameObject.SetActive(true);
    }
    public void PanelActive(bool active)
    {
        panel.SetActive(active);
        bestScoreTxt.gameObject.SetActive(active);
        DisplayBestScore();
        if (active)
        {
            currentScoreTxt.transform.localPosition = new Vector3(117f, -70f, 0);
        }
        else currentScoreTxt.transform.localPosition = new Vector3(-239.4305f, 179f, 0);
    }

    public void StartGame()
    {
        GameManager.instance.StartGame();
        PanelActive(false);
    }

    public void GameOver()
    {
        PanelActive(true);
        gameOverTxt.gameObject.SetActive(true);
        restartBtn.gameObject .SetActive(true);
        startBtn.gameObject .SetActive(false);
        infoTxt.gameObject .SetActive(false);

    }

    public void Restart()
    {
        GameManager.instance.Restart();
        PanelActive(false);
    }
    public void DisplayScore()
    {
        currentScoreTxt.gameObject.SetActive(true);
        currentScoreTxt.text = "점수: " + GameManager.instance.score.ToString();
    }

    public void DisplayBestScore()
    {
        bestScoreTxt.text = "최고 점수: " + GameManager.instance.bestScore.ToString();  
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
