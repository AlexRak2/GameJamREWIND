using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroDialogue : MonoBehaviour
{
    [SerializeField] Text dText;

    [SerializeField] string[] IntroText;
    public float timer;
    public float timerDialogue = 15f;

    float EndIntro;
    int textIndex = 0;

    //Type Writer 
    public float delay = 0.1f;
    public string fullText;
    private string currentText = "";

    float textLimit = 0;

    public bool isVictory;
    // Start is called before the first frame update
    void Start()
    {
        EndIntro = IntroText.Length * timerDialogue;
        fullText = IntroText[textIndex];
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < timerDialogue)
        {
            if (textIndex <= IntroText.Length - 1 && textLimit < 1) 
            {
                StartCoroutine(ShowText());
                textLimit++;
            }
            
        }
        else 
        {
            if (textIndex <= IntroText.Length - 1)
            {
                print("Next");
                timerDialogue += 15;
                textIndex++;
                fullText = IntroText[textIndex];
                textLimit = 0;
            }
        }

        if (EndIntro <= timer ) 
        {
            if (isVictory)
            {
                StartCoroutine(LoadMainMenu());
            }
            else { StartCoroutine(LoadNextLevel()); }
        }
    }

    IEnumerator ShowText() 
    {
        for (int i = 0; i < fullText.Length; i++) 
        {
            currentText = fullText.Substring(0, i);
            dText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator LoadNextLevel()
    {
        print("Level Done");
        //audioSource.PlayOneShot(doorOpen);
        //FindObjectOfType<ShowLevelTitle>().StartFadeOut();
        yield return new WaitForSeconds(1.5f);
        //audioSource.PlayOneShot(doorClose);
        yield return new WaitForSeconds(3f);
        int level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(level + 1);
    }
    IEnumerator LoadMainMenu()
    {
        print("Level Done");
        //audioSource.PlayOneShot(doorOpen);
        //FindObjectOfType<ShowLevelTitle>().StartFadeOut();
        yield return new WaitForSeconds(1.5f);
        //audioSource.PlayOneShot(doorClose);
        yield return new WaitForSeconds(3f);
        int level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(0);
    }
}
