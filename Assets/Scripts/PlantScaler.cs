using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantScaler : MonoBehaviour
{
    [SerializeField] GameObject plant;

    [SerializeField] float speed = 0.1f;
    [SerializeField] float startScale = 1f;
    [SerializeField] float endScale = 2f;
    [SerializeField] float rotSpeed = 0f;
    float tarScale = 2f;
    [SerializeField] bool rewinding = false;
    [SerializeField] bool reachedEnd = false;
    [SerializeField] AudioClip[] growing;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateTarget();
    }

    void Update()    
    {
        Vector3 curScale = plant.transform.localScale;

        if ((curScale.x >= tarScale && !rewinding) || (curScale.x <= tarScale && rewinding))
        {
            reachedEnd = true;
            audioSource.Stop();
        }

        if (!reachedEnd)
        {
            plant.transform.localScale = new Vector3(curScale.x + speed * Time.deltaTime, curScale.y, curScale.z);
            plant.transform.Rotate(Vector3.left, rotSpeed * Time.deltaTime);
        }
    }


    private void UpdateTarget()
    {
        audioSource.clip = growing[Random.Range(0,1)];
        audioSource.Play();
        reachedEnd = false;

        if (rewinding)
        {
            rotSpeed = rotSpeed * -1f;

            speed = speed * -1f;

            tarScale = startScale;
        }
        else 
        {
            rotSpeed = Mathf.Abs(rotSpeed);

            speed = Mathf.Abs(speed);

            tarScale = endScale;
        }

    }

    public void SetRewind(bool rewindIn)
    {
        rewinding = rewindIn;
        reachedEnd = false;
        UpdateTarget();
    }
}
