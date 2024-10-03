using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        int score = PlayerPrefs.GetInt("Score", 0);
        scoreText.text = score.ToString();
    }

    void Update()
    {

    }

    public void OnPlayAgain()
    {
        SceneManager.LoadScene("GameScene");
        Debug.Log("test");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
