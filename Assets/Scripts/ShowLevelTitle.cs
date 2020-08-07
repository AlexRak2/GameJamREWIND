using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowLevelTitle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] string[] levelString;
    [SerializeField] float displayTime;

    void Start()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        levelText.text = levelString[level-1];
        Invoke("HideLevelText", 7f);
    }

    void HideLevelText()
    {
        levelText.gameObject.SetActive(false);
    }
}
