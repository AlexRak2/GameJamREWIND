using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float wallSafeDis = .45f;
    [SerializeField] Transform top;
    [SerializeField] Transform bot;


    [SerializeField] bool grounded = false;
    float dirAtJump = 0f;
    [SerializeField] bool closeRight = false;
    [SerializeField] bool closeLeft = false;
    bool startJump = false;
    
    Vector2 movement;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), 0f);

        if (Input.GetKeyDown(KeyCode.Space) && grounded && !startJump)
        {
            dirAtJump = Input.GetAxis("Horizontal");
            startJump = true;
        }
    }

    private void FixedUpdate()
    {
        CheckDistanceToWalls();

        if (movement.x > 0.1f || movement.x < -0.1f)
        {
            MoveCharacter(movement);
        }

        if (startJump)
        {
            startJump = false;
            JumpCharacter(new Vector3(0f, 1f, 0f));
        }
    }

    private void CheckDistanceToWalls()
    {
        int layerMask = 1 << 8;
        RaycastHit hitForward;
        if (Physics.Raycast(top.position, transform.TransformDirection(Vector3.forward), out hitForward, Mathf.Infinity, layerMask))
        {
            if (hitForward.distance < wallSafeDis)
            {
                closeRight = true;
            }
            else if (Physics.Raycast(bot.position, transform.TransformDirection(Vector3.forward), out hitForward, Mathf.Infinity, layerMask))
            {
                if (hitForward.distance < wallSafeDis)
                {
                    closeRight = true;
                }
                else
                {
                    closeRight = false;
                }
            }

        }

        RaycastHit hitBackward;
        if (Physics.Raycast(top.position, transform.TransformDirection(Vector3.back), out hitBackward, Mathf.Infinity, layerMask))
        {
            if (hitBackward.distance < wallSafeDis)
            {
                closeLeft = true;
            }
            else if (Physics.Raycast(bot.position, transform.TransformDirection(Vector3.back), out hitBackward, Mathf.Infinity, layerMask))
            {
                if (hitBackward.distance < wallSafeDis)
                {
                    closeLeft = true;
                }
                else
                {
                    closeLeft = false;
                }
            }
        }
    }

    void MoveCharacter(Vector2 direction)
    {
        float activeSpeed = speed;
        float currentDir = Input.GetAxis("Horizontal");

        // Stops movement clipping through objects
        if (currentDir < 0 && closeLeft)
        {
            direction.x = 0;
        }
        
        if (currentDir > 0 && closeRight)
        {
            direction.x = 0;
        }

        if (!grounded) // Slows down movement speed if changing directions mid air
        {
            if ((currentDir > 0 && dirAtJump < 0) || (currentDir < 0 && dirAtJump > 0) || (dirAtJump <= 0.1f && dirAtJump >= -0.1f))
            {
                activeSpeed = speed * .25f;
            }
        }

        rb.MovePosition((Vector2)transform.position + (direction * activeSpeed * Time.deltaTime));

    }

    void JumpCharacter(Vector3 direction)
    {
        rb.velocity = (transform.up + (direction * jumpSpeed));
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == 8)
        {
            grounded = true;

        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == 8)
        {
            grounded = false;
        }
    }
}
