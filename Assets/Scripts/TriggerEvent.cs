using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] int eventID;
    TutorialController tutControl;
    void Start()
    {
        tutControl = FindObjectOfType<TutorialController>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutControl.SetText(eventID);
        }
    }
}
