using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject buttonPrefabs;
    public Transform layout;
    public int btnCount;

    void Start()
    {
        btnCount = 47;
        for (int i = 0; i < btnCount; i++)
        {
            GameObject newButton = Instantiate(buttonPrefabs, layout);


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
