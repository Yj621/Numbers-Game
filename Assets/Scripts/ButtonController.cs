using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

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

    public void OnButtonClick(Button clickedButton, int buttonNum) //Ŭ���� ��ư�� ��ư ���ڸ� ������
    {
        GetComponent<AudioSource>().PlayOneShot(ClickSound);
        int score = UIController.instance.Score;

        grid.enabled = false; 

        if (firstButton == null)
        {
            firstButtonNum = buttonNum;
            firstButton = clickedButton;
        }
        else
        {
            if(firstButtonNum == buttonNum && clickedButton!= firstButton)
            {
                Destroy(firstButton.gameObject);
                Destroy(clickedButton.gameObject);
                UIController.instance.IncreaseScore(); // UIController�� ���� ���� �޼��� ȣ��
                GetComponent<AudioSource>().PlayOneShot(SucessSound);
            }
            else //�߸������� ���µǰ� ����
            {
                firstButton = clickedButton;
                firstButtonNum = buttonNum;
            }
        }

    }

    void Update()
    {
    }
}
