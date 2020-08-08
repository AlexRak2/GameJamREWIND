using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowLevelTitle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] string[] levelString;
    [SerializeField] float displayTime;
    [SerializeField] GameObject loadOverlay;

    UnityEngine.UI.Image blackImage;
    bool fadingDone = false;
    Color tempColor;
    bool fadingout = false;
    [SerializeField] float fadeAmnt = 0.3f;


    void Start()
    {
        loadOverlay.SetActive(true);
        blackImage = loadOverlay.GetComponent< UnityEngine.UI.Image> ();
        int level = SceneManager.GetActiveScene().buildIndex;
        levelText.text = levelString[level-1];
        Invoke("HideLevelText", displayTime);
        
    }

    public void StartFadeOut()
    {
        fadingout = true;
        loadOverlay.gameObject.SetActive(true);
        print("test");
        fadingDone = false;

    }

    private void Update()
    {
        tempColor = blackImage.color;

        if (!fadingout)
        {
            FadeIn();

        }

        if (fadingout)
        {
            FadeOut();
        }

    }

    private void FadeIn()
    {
        if (!fadingDone)
        {
            Color tempColor = blackImage.color;
            tempColor.a = tempColor.a - (fadeAmnt * Time.deltaTime);
            blackImage.color = tempColor;
        }

        if (tempColor.a <= 0f)
        {
            fadingDone = true;
            loadOverlay.gameObject.SetActive(false);
        }
    }

    private void FadeOut()
    {
        if (!fadingDone)
        {
            Color tempColor = blackImage.color;
            tempColor.a = tempColor.a + ((fadeAmnt*2) * Time.deltaTime);
            blackImage.color = tempColor;
        }

        if (tempColor.a >= 1f)
        {
            fadingDone = true;
        }
    }

    void HideLevelText()
    {
        levelText.gameObject.SetActive(false);
    }
}
