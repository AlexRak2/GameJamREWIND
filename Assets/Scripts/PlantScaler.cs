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
    float tarScale = 2f;
    [SerializeField] bool rewinding = false;
    [SerializeField] bool reachedEnd = false;


    void Start()
    {

        UpdateTarget();
    }

    void Update()    
    {
        Vector3 curScale = plant.transform.localScale;

        if ((curScale.x >= tarScale && !rewinding) || (curScale.x <= tarScale && rewinding))
        {
            reachedEnd = true;
        }

        if (!reachedEnd)
        {
            plant.transform.localScale = new Vector3(curScale.x + speed * Time.deltaTime, curScale.y, curScale.z);
        }
    }


    private void UpdateTarget()
    {
        reachedEnd = false;

        if (rewinding)
        {
            speed = speed * -1f;

            tarScale = startScale;
        }
        else 
        {
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
