﻿using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float wallSafeDis = .45f;
    [SerializeField] float startFallDist = 1f;
    [SerializeField] float fallTime = 0.5f;
    [SerializeField] Transform top;
    [SerializeField] Transform bot;


    [SerializeField] bool grounded = false;
    float dirAtJump = 0f;
    [SerializeField] bool closeRight = false;
    [SerializeField] bool closeLeft = false;
    bool startJump = false;
    bool falling;
    bool bonusJump = true;
    
    Vector2 movement;
    Rigidbody rb;
    Spells spells;
    bool frozen = false;
    Vector3 storedForce;
    FaceMouse faceMouse;

    AudioSource audioSource;
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip landSFX;

    [SerializeField] Animator animationController;
    float fallLimit = 0;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        faceMouse = GetComponentInChildren<FaceMouse>();
        rb = GetComponent<Rigidbody>();
        spells = GetComponent<Spells>();
    }

    void Start()
    {

    }

    public void Freeze(bool freeze)
    {
        if (freeze)
        {
            frozen = true;
            movement = new Vector2(0f, 0f);
            storedForce = rb.velocity;
            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.useGravity = false;
        }
        else
        {
            frozen = false;
            rb.velocity = storedForce;
            rb.useGravity = true;
        }
    }

    void Update()
    {
        if (!frozen)
        {
            movement = new Vector2(Input.GetAxis("Horizontal"), 0f);

            if (Input.GetKeyDown(KeyCode.Space) && grounded && !startJump)
            {
                dirAtJump = Input.GetAxis("Horizontal");
                startJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && bonusJump)
            {
                bonusJump = false;
                storedForce = (transform.up + (new Vector3(0f,1f,0f) * jumpSpeed));
            }
        }

    }

    private void FixedUpdate()
    {
        CheckDistanceToWalls();

        if (movement.x > 0.1f || movement.x < -0.1f)
        {
            MoveCharacter(movement);
            animationController.SetBool("IsWalking", true);


        }
        else 
        {
            animationController.SetBool("IsWalking", false);
            if (!frozen)
            {
                //faceMouse.FaceDir(true);
            }
        }

        if (startJump)
        {
            audioSource.PlayOneShot(jumpSFX);
            startJump = false;
            falling = true;
            animationController.ResetTrigger("IsFalling");
            animationController.SetTrigger("IsJumping");
            JumpCharacter(new Vector3(0f, 1f, 0f));
            
        }

        if (falling) 
        {
            Invoke("CheckDistanceToFloor", fallTime);

        }
    }

    private void CheckDistanceToFloor()
    {
        RaycastHit hitFall;
        if (Physics.Raycast(bot.position, Vector3.down, out hitFall)) ;
        {
            //print(hitFall.distance);

            if (hitFall.distance < startFallDist && fallLimit < 1) 
            {
                fallLimit++;
                falling = false;
                animationController.SetTrigger("IsFalling");
                fallLimit = 0;
            }
            else
            {
            }
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

        if (movement.x < 0)
        {
            faceMouse.FaceDir(false);
        }
        else
        {
            faceMouse.FaceDir(true);
        }


        if (!grounded) // Slows down movement speed if changing directions mid air
        {
            if ((currentDir > 0 && dirAtJump < 0) || (currentDir < 0 && dirAtJump > 0) || (dirAtJump <= 0.1f && dirAtJump >= -0.1f))
            {
                activeSpeed = speed * .75f;
            }
        }

        rb.MovePosition((Vector2)transform.position + (direction * activeSpeed * Time.deltaTime));

    }

    void JumpCharacter(Vector3 direction)
    {
        rb.velocity = (transform.up + (direction * jumpSpeed));
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 8)
        {
            if (!grounded)
            audioSource.PlayOneShot(landSFX,0.1f);

        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == 8)
        {
            grounded = true;
            bonusJump = true;

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
