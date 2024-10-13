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

            int buttonNum = numbers[i];  // ���� ������ buttonNum�� ����
            Button button = newButton.GetComponent<Button>();

            if (button != null)
            {
                int newButtonNum = buttonNum;
                button.onClick.AddListener(() => OnButtonClick(button, newButtonNum));  // Ŭ�� �̺�Ʈ ���
            }
        }
        grid = GetComponentInChildren<GridLayoutGroup>(); //grid�� ���� Ŭ���� ��ư���� �������� �� ������ �������
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, list.Count); //������ ���ڸ� ��ư text�� �־���
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
                UIController.Instance.IncreaseScore(); // UIController�� ���� ���� �޼��� ȣ��
                GetComponent<AudioSource>().PlayOneShot(SucessSound);

                // ���� Ŭ���� üũ ȣ��
                GameClearCheck();
            }
            else // �߸� ������ ���µǰ� ����
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
            UIController.Instance.isCleared = true; // ���� Ŭ���� ���� ����

            // ���� ������ PlayerPrefs�� ����
            PlayerPrefs.SetInt("Score", UIController.Instance.Score);

            // GameManager�� SaveHighScore �޼ҵ� ȣ��
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.SaveHighScore(UIController.Instance.Score, UIController.Instance.timerText.text); // ���� ������ �ð��� �ѱ�
            }

            // GameEndScene���� ��ȯ
            SceneManager.LoadScene("GameEndScene");
        }
    }


    void Update()
    {
        // ��� ��ư�� ��������� üũ
        GameClearCheck();
    }
}
