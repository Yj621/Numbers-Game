using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ranking : MonoBehaviour
{
    public GameObject rankingPanel;
    public TextMeshProUGUI rankText;

    void Start()
    {
        rankingPanel.SetActive(false);
    }

    private void OnEnable()
    {
        DisplayRanking();
    }
    void DisplayRanking()
    {
        string rankingDisplay = "";
        for (int i = 0; i <= 5; i++)
        {
            string time = PlayerPrefs.GetString($"HighScore{i}", "00:00");
            rankingDisplay += $"{i + 1}. {time}\n";
        }
        rankText.text = rankingDisplay;
    }
}
