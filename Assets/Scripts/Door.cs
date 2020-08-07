using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] int requiredKeys = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetReqKeys()
    {
        return requiredKeys;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger && other.gameObject.tag == "Player")
        {
            int amountKeys = other.gameObject.GetComponent<PlayerStats>().GetKeys();
            if (amountKeys >= requiredKeys)
            {
                print("Level Done");
                int level = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(level + 1);
            }
        }
    }
}
