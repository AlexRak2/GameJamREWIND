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

    Animator plantAnimator;
    float angle;

    void Start()
    {
        plantAnimator = plant.GetComponent<Animator>();
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

        CalculateAngle();

    }

    private void CalculateAngle()
    {
        Vector2 curPos = new Vector2();
        curPos.x = transform.position.x;
        curPos.y = transform.position.y;
        Vector2 tarPos = new Vector2();
        tarPos.x = positions[currentTarget].position.x;
        tarPos.y = positions[currentTarget].position.y;

        Vector2 angPos = new Vector2();
        angPos.x = curPos.x - tarPos.x;
        angPos.y = curPos.y - tarPos.y;
        angle = Mathf.Atan2(angPos.y, angPos.x) * Mathf.Rad2Deg;
        print(angle);
        plantAnimator.SetFloat("Angle", angle);

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
