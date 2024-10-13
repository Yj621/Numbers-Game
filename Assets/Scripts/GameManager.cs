using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI rankText;

    private int score;

    void Start()
    {
        scoreText.text = score.ToString();
        timeText.text = " ";
    }
    void Update()
    {
        if (UIController.Instance.isCleared)
        {
            // 게임이 클리어되었을 때만 점수를 저장
            if (score == 0) // 이미 점수를 가져왔다면 저장하지 않음
            {
                score = PlayerPrefs.GetInt("Score", 0);
                titleText.text = "Clear!";
                timeText.text = UIController.Instance.timerText.text;

                SaveHighScore(score, timeText.text); // 점수와 시간을 함께 저장
            }
        }
        else
        {
            titleText.text = "Game Over";
        }
    }

    public void SaveHighScore(int score, string time)
    {
        List<(int score, int timeInSeconds)> highScores = new List<(int score, int timeInSeconds)>();

        for (int i = 1; i <= 5; i++)
        {
            int savedScore = PlayerPrefs.GetInt($"HighScore{i}", 0);
            string savedTime = PlayerPrefs.GetString($"HighTime{i}", "00:00");

            // 시간을 초로 변환
            int savedTimeInSeconds = ConvertTimeToSeconds(savedTime);
            highScores.Add((savedScore, savedTimeInSeconds));
        }

        // 새로운 점수 추가
        int timeInSeconds = ConvertTimeToSeconds(time);
        highScores.Add((score, timeInSeconds));

        // 점수 및 시간 오름차순으로 정렬
        highScores.Sort((x, y) =>
        {
            // 시간 기준으로 비교 (많을수록 높은 점수)
            if (x.timeInSeconds != y.timeInSeconds)
                return y.timeInSeconds.CompareTo(x.timeInSeconds); // 높은 시간 우선
            return y.score.CompareTo(x.score); // 점수 기준으로 비교
        });

        // 상위 5개 저장
        for (int i = 0; i < 5; i++)
        {
            if (i < highScores.Count)
            {
                PlayerPrefs.SetInt($"HighScore{i + 1}", highScores[i].score);
                PlayerPrefs.SetString($"HighTime{i + 1}", ConvertSecondsToTime(highScores[i].timeInSeconds));
            }
        }
        PlayerPrefs.Save();
    }

    // 시간을 초로 변환하는 메서드
    private int ConvertTimeToSeconds(string time)
    {
        string[] parts = time.Split(':');
        int minutes = int.Parse(parts[0]);
        int seconds = int.Parse(parts[1]);
        return (minutes * 60) + seconds; // 초로 변환
    }

    // 초를 MM:SS 형식으로 변환하는 메서드
    private string ConvertSecondsToTime(int seconds)
    {
        int minutes = seconds / 60;
        seconds = seconds % 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void OnPlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void DisplayRanking()
    {
        string rankingDisplay = "";
        for (int i = 0; i < 5; i++)
        {
            int score = PlayerPrefs.GetInt($"HighScore{i + 1}", 0);
            string time = PlayerPrefs.GetString($"HighTime{i + 1}", "00:00");


            rankingDisplay += $"{i + 1}. {score} / {time}\n";
        }
        rankText.text = rankingDisplay;
    }

}
