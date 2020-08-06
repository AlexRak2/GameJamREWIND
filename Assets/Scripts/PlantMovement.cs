using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMovement : MonoBehaviour
{
    [SerializeField] GameObject plant;
    [SerializeField] GameObject plantHead;
    [SerializeField] GameObject plantMid;
    [SerializeField] GameObject plantTail;

    [SerializeField] float speed = 0.1f;
    [SerializeField] List<Transform> positions = new List<Transform>();
    [SerializeField] bool rewinding = false;
    [SerializeField] int currentTarget = 1;
    [SerializeField] int prevTarget = 0;
    [SerializeField] int targets;
    [SerializeField] bool reachedEnd = false;

    Animator plantAnimator;
    float angle;
    float headBob = 1000f;


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

        CalculateAngle();

        if (!reachedEnd)
        {
            plant.transform.position = Vector3.MoveTowards(plant.transform.position, positions[currentTarget].position, speed * Time.deltaTime);
            Vector3 direction = plant.transform.position - positions[currentTarget].position;
            //Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, 1f, 1f);
            //plant.transform.rotation = Quaternion.LookRotation(newDir);
            //plantHead.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 180f));
            //plant.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, headBob * Time.deltaTime));
            //plantMid.transform.position = Vector3.MoveTowards(plant.transform.position, positions[currentTarget].position, speed * Time.deltaTime);

        }
    }

    private void CalculateAngle()
    {
        Vector2 curPos = new Vector2();
        curPos.x = plantHead.transform.position.x;
        curPos.y = plantHead.transform.position.y;
        Vector2 tarPos = new Vector2();
        if (!rewinding)
        {
            tarPos.x = positions[2].position.x;
            tarPos.y = positions[2].position.y;
        }
        else
        {
            tarPos.x = positions[2].position.x;
            tarPos.y = positions[2].position.y;
        }

        Vector2 angPos = new Vector2();
        angPos.x = curPos.x - tarPos.x;
        angPos.y = curPos.y - tarPos.y;
        angle = Mathf.Atan2(angPos.y, angPos.x) * Mathf.Rad2Deg;
        //if (angle > 45f)
        //{
        //    angle = angle * 0.5f;
        //}
        //if (angle < -45)
        //{
        //    angle = angle * 0.5f;
        //}
        angle = 0f;
        plantAnimator.SetFloat("Angle", angle*0.25f);
        print(angle);

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
