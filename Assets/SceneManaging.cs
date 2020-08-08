using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManaging : MonoBehaviour
{
    public string discordLink;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame() 
    {
        Application.Quit();
    }

    public void PlayGame() 
    {
        StartCoroutine(LoadNextLevel());
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
        public void DiscordRedirect() 
    {
        Application.OpenURL(discordLink);
    }
}
