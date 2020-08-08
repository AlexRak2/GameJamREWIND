using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool open = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !open)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            open = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && open)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            open = false;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
