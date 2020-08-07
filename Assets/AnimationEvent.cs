using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAnimationEvent()
    {
        int random = Random.Range(0, audioClips.Length);
        //print(audioClips[random]);
        audioSource.PlayOneShot(audioClips[random]);

    }
}
