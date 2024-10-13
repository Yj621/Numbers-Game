using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public GridLayoutGroup grid;

    public GameObject buttonPrefabs;
    public Transform layout;
    public int btnCount = 48;

    public AudioClip ClickSound;
    public AudioClip SucessSound;

    private int firstButtonNum = 0;
    private Button firstButton = null;

    void Start()
    {

        List<int> numbers = new List<int>();

        for (int i = 1; i <= btnCount / 2; i++)
        {
            numbers.Add(i);
            numbers.Add(i);
        }
        Shuffle(numbers);

        for (int i = 0; i < btnCount; i++)
        {
            GameObject newButton = Instantiate(buttonPrefabs, layout);

            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = numbers[i].ToString();
            }

            int buttonNum = numbers[i];  // 지역 변수로 buttonNum을 저장
            Button button = newButton.GetComponent<Button>();

            if (button != null)
            {
                int newButtonNum = buttonNum;
                button.onClick.AddListener(() => OnButtonClick(button, newButtonNum));  // 클릭 이벤트 등록
            }
        }
        grid = GetComponentInChildren<GridLayoutGroup>(); //grid를 꺼서 클릭된 버튼들이 없어지며 빈 공간을 만들어줌
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, list.Count); //랜덤한 숫자를 버튼 text에 넣어줌
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void OnButtonClick(Button clickedButton, int buttonNum)
    {
        GetComponent<AudioSource>().PlayOneShot(ClickSound);
        int score = UIController.Instance.Score;

        grid.enabled = false;

        if (firstButton == null)
        {
            firstButtonNum = buttonNum;
            firstButton = clickedButton;
        }
        else
        {
            if (firstButtonNum == buttonNum && clickedButton != firstButton)
            {
                Destroy(firstButton.gameObject);
                Destroy(clickedButton.gameObject);
                UIController.Instance.IncreaseScore(); // UIController의 점수 증가 메서드 호출
                GetComponent<AudioSource>().PlayOneShot(SucessSound);

                // 게임 클리어 체크 호출
                GameClearCheck();
            }
            else // 잘못 누르면 리셋되게 해줌
            {
                firstButton = clickedButton;
                firstButtonNum = buttonNum;
            }
        }
    }

    void GameClearCheck()
    {
        Button[] remainButtons = layout.GetComponentsInChildren<Button>();
        if (remainButtons.Length == 0)
        {
            UIController.Instance.isCleared = true; // 게임 클리어 상태 설정

            // 현재 점수를 PlayerPrefs에 저장
            PlayerPrefs.SetInt("Score", UIController.Instance.Score);

            // GameManager의 SaveHighScore 메소드 호출
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.SaveHighScore(UIController.Instance.Score, UIController.Instance.timerText.text); // 현재 점수와 시간을 넘김
            }

            // GameEndScene으로 전환
            SceneManager.LoadScene("GameEndScene");
        }
    }


    void Update()
    {
        // 모든 버튼이 사라졌는지 체크
        GameClearCheck();
    }
}
