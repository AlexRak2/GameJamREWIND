using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField] FaceMouse faceMouse;
    [SerializeField] float maxAim = 85f;
    [SerializeField] float minAim = -85f;
    Vector3 mouse_pos;
    Vector3 object_pos;
    float angle;

    void Update()
    {
        mouse_pos = Input.mousePosition;
        object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;

        if (faceMouse.angle == 0f)
        {

        }
        else
        {
            angle = -angle +180f;
        }

        transform.localRotation = Quaternion.Euler(new Vector3(-angle, 0f, 0f));
    }
}
