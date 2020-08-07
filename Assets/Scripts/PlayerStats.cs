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

    [SerializeField] int keys;
    [SerializeField] TextMeshProUGUI keyText;
    Door exit;


    // Start is called before the first frame update
    void Start()
    {
        exit = FindObjectOfType<Door>();
        currentHealth = MaxHealth;
        currentMana = MaxMana;
        SetKeyText();
    }

    private void SetKeyText()
    {
        keyText.text = "Keys: " + keys + " / " + exit.GetReqKeys();
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

    public void KillPlayer()
    {
        SceneManager.LoadScene(0);
    }

    public void AddKey()
    {
        keys++;
        SetKeyText();

    }

    public int GetKeys()
    {
        return keys;
    }
}
