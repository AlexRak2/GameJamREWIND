using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMouse : MonoBehaviour
{
    Vector3 mouse_pos;
    Vector3 object_pos;
    public float angle;

    void Update()
    {
        mouse_pos = Input.mousePosition;
        object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        angle = Mathf.Abs(angle);
        if (angle >= 90f)
        {
            angle = 180f;
        }
        else
        {
            angle = 0f;
        }
        transform.localRotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
    }
}
