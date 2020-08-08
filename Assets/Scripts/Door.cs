using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] int requiredKeys = 1;
    [SerializeField] AudioClip doorOpen;
    [SerializeField] AudioClip doorClose;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
                StartCoroutine(LoadNextLevel());
            }
        }
    }

    IEnumerator LoadNextLevel()
    {
        print("Level Done");
        audioSource.PlayOneShot(doorOpen);
        FindObjectOfType<ShowLevelTitle>().StartFadeOut();
        yield return new WaitForSeconds(1.5f);
        audioSource.PlayOneShot(doorClose);
        yield return new WaitForSeconds(3f);
        int level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(level + 1);
    }
}
