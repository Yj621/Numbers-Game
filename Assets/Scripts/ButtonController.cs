using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject buttonPrefabs;
    public Transform layout;
    public int btnCount = 48;

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

            Text buttonText = newButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = numbers[i].ToString();
            }
        }
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
