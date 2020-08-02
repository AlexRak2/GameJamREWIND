using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMovement : MonoBehaviour
{
    [SerializeField] GameObject plant;
    [SerializeField] float speed = 0.1f;
    [SerializeField] List<Transform> positions = new List<Transform>();
    [SerializeField] bool rewinding = false;
    [SerializeField] int currentTarget = 1;
    [SerializeField] int prevTarget = 0;


    void Start()
    {
        
    }

    void Update()
    {
        if (!rewinding)
        {
            plant.transform.position = Vector3.MoveTowards(plant.transform.position, positions[currentTarget].position, speed * Time.deltaTime);
        }
        else
        {
            plant.transform.position = Vector3.MoveTowards(plant.transform.position,, positions[prevTarget].position, speed * Time.deltaTime);
        }
    }

    public void SetRewind(bool rewindIn)
    {
        rewinding = rewindIn;
    }
}
