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
        SceneManager.LoadScene(1);   
    }

    public void DiscordRedirect() 
    {
        Application.OpenURL(discordLink);
    }
}
