using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI timeText;

    void Start()
    {
        int score = PlayerPrefs.GetInt("Score", 0);
        scoreText.text = score.ToString();
        timeText.text = " ";
    }

    void Update()
    {
        if(UIController.Instance.isCleared)
        {
            titleText.text = "Clear!";
            //시간 표시
            timeText.text = UIController.Instance.timerText.text;
            string time = timeText.text;
            
            SaveHighScore(time);
        }
        else
        {
            titleText.text = "Game Over";
        }
    }
    void SaveHighScore(string time)
    {
        List<string> highScores = new List<string>();

        for(int i = 1; i<= 5; i++)
        {
            highScores.Add(PlayerPrefs.GetString($"HighScore{i}", ""));
        }

        highScores.Add(time);
        highScores.Sort();

        for (int i=0; i< 5; i++)
        {
                if(i < highScores.Count)
                {
                    PlayerPrefs.SetString($"HighScore{i + 1}", highScores[i]);
                }
        }
        PlayerPrefs.Save();
    }
    public void OnPlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuit()
    {
        Application.Quit();
    }

}
