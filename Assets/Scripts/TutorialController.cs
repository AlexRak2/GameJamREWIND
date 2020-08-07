using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialController : MonoBehaviour
{
    [TextArea]
    [SerializeField] string[] tutorialText;
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] float displayTime = 5f;
    float timer = 0f;

    void Start()
    {
        SetText(0);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= displayTime)
        {
            textBox.text = "";
        }
    }

    public void SetText(int textID)
    {
        textBox.text = tutorialText[textID];
        timer = 0f;
    }
}
