using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpSpeed = 1f;

    [SerializeField] KeyCode fire;

    [SerializeField] bool grounded = false;
    float dirAtJump = 0f;
    bool startJump = false;
    bool haltMove = false;
    bool collided = false;
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
        if (1 == 1)
        {
            movement = new Vector2(Input.GetAxis("Horizontal"), 0f);
            print(movement);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            dirAtJump = Input.GetAxis("Horizontal");
            startJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (!haltMove)
        {
            MoveCharacter(movement);
        }

        if (startJump)
        {
            startJump = false;
            JumpCharacter(new Vector3(0f, 1f, 0f));
        }
    }

    void MoveCharacter(Vector2 direction)
    {
        float activeSpeed = speed;
        float currentDir = Input.GetAxis("Horizontal");

        if (!grounded) // Slows down movement speed if changing directions mid air
        {
            if ((currentDir > 0 && dirAtJump < 0) || (currentDir < 0 && dirAtJump > 0))
            {
                activeSpeed = speed * .25f;
            }
        }

        rb.MovePosition((Vector2)transform.position + (direction * activeSpeed * Time.deltaTime));

    }

    void JumpCharacter(Vector3 direction)
    {
        rb.velocity = (transform.up + (direction * jumpSpeed * Time.deltaTime));
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            grounded = true;
            haltMove = false;
            collided = false;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            grounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (!grounded)
            {
                haltMove = true;

                Invoke("RestoreMove", 1f);
                movement.x = 0f;
                collided = true;

                float magnitude = 30f;

                Vector3 force = collision.transform.position - transform.forward;

                force.Normalize();
                rb.AddForce(-force * magnitude);
            }
            else
            {

                //rb.MovePosition((Vector2)transform.position + ( -movement * speed*5 * Time.deltaTime));
            }
        }
    }

    void RestoreMove()
    {
        haltMove = false;
    }
}
