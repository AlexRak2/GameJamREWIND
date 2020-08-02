using System;
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
    [SerializeField] int targets;
    [SerializeField] bool reachedEnd = false;


    void Start()
    {
        targets = positions.Count;
    }

    void Update()
    {
        if (Vector3.Distance(plant.transform.position, positions[currentTarget].position) <= 0.1f)
        {
            UpdateTarget();
        }

        if (!reachedEnd)
        {
            plant.transform.position = Vector3.MoveTowards(plant.transform.position, positions[currentTarget].position, speed * Time.deltaTime);
        }

    }

    private void UpdateTarget()
    {
        prevTarget = currentTarget;
        if (!rewinding)
        {
            currentTarget++;
        }
        else
        {
            currentTarget--;
        }

        if (currentTarget > targets-1 || currentTarget < 0)
        {
            reachedEnd = true;
            currentTarget = prevTarget;
        }
    }

    public void SetRewind(bool rewindIn)
    {
        rewinding = rewindIn;
        reachedEnd = false;
        UpdateTarget();
    }
}
