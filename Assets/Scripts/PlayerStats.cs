using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class PlayerStats : MonoBehaviour
{
    public float MaxHealth = 100;
    public float currentHealth;

    public float MaxMana = 20;
    public float currentMana;

    bool alive = true;
    bool levelDone = false;

    [SerializeField] int keys;
    [SerializeField] TextMeshProUGUI keyText;
    [SerializeField] GameObject gameOverScreen;
    Door exit;
    [SerializeField] AudioClip keyClip;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        exit = FindObjectOfType<Door>();
        currentHealth = MaxHealth;
        currentMana = MaxMana;
        SetKeyText();
    }

    private void SetKeyText()
    {
        if (keyText.gameObject.activeSelf)
        {
            keyText.text = ": " + keys + " / " + exit.GetReqKeys();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Making a function to use Mana so its cleaner
    public void UseMana(float used) 
    {
        currentMana -= used; 
    }

    public void LevelCompleted()
    {
        levelDone = true;
    }

    public void KillPlayer()
    {
        if (!levelDone)
        {
            gameOverScreen.SetActive(true);
            GetComponent<Player>().Freeze(true);
            Invoke("ReloadLevel", 2f);
        }

    }

    private void ReloadLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(level);
    }

    public void AddKey()
    {
        audioSource.PlayOneShot(keyClip);
        keys++;
        SetKeyText();

    }

    public int GetKeys()
    {
        return keys;
    }
}
