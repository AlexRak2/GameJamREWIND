using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public float MaxHealth = 100;
    public float currentHealth;

    public float MaxMana = 20;
    public float currentMana;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        currentMana = MaxMana;
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
}
