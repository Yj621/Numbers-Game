using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    public float timer = 60f;
    public bool isCleared = false;

    private int score;
    public int Score
    {
        get {return score;}
    }
    private static UIController instance;
    public static UIController Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        instance = this;
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
            PlayerPrefs.SetInt("Score", score); //키 벨류 저장장소

            SceneManager.LoadScene("GameEndScene");
            isCleared = false;
        }
        
    }
    public void TextUpdate()
    {
        // 타이머를 분:초 (MM:SS) 형식으로 변환
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        // 소수점 둘째 자리까지 표시
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
