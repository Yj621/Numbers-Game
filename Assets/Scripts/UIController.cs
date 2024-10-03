using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    public float timer = 60f;

    private int score;
    public int Score
    {
        get
        {
            return score;
        }
    }

    void Start()
    {
        instance = this;
        score = int.Parse(scoreText.text);
    }
    public void IncreaseScore()
    {
        score += 2;
        scoreText.text = score.ToString();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        
        TextUpdate();

        if(timer <= 0)
        {
            PlayerPrefs.SetInt("Score", score); //Ű ���� �������

            SceneManager.LoadScene("GameOver");
        }
    }
    public void TextUpdate()
    {
        // Ÿ�̸Ӹ� ��:�� (MM:SS) �������� ��ȯ
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        // �Ҽ��� ��° �ڸ����� ǥ��
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
