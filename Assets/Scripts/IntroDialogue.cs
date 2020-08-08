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
    // Start is called before the first frame update
    void Start()
    {
        EndIntro = IntroText.Length * timerDialogue;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < timerDialogue)
        {
            if (textIndex <= IntroText.Length - 1) 
            {
                dText.text = IntroText[textIndex];
            }
            
        }
        else 
        {
            if (textIndex <= IntroText.Length - 1)
            {
                print(textIndex + " | " + IntroText.Length);
                timerDialogue += 15;
                textIndex++;
            }
        }
        if (EndIntro <= timer ) 
        {
            StartCoroutine(LoadNextLevel());
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
}
