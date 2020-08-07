using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
    public Spells spells;
    [Range(0,10)]
    public float smallRange;
    [Range(0, 15)]
    public float rewindRange;
    float currentRange;
    [Range(0, 15)]
    public float normalSpeed;
    [Range(0, 15)]
    public float rewindSpeed;
    float currentSpeed;
    [Range(0, 6)]
    public float respawnTime;
    float timer;
    public bool alive;
    bool chargingStarted = false;

    public GameObject player;
    private Vector3 oldPos;

    [SerializeField] AudioClip[] enemySFX;
    AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        spells = player.GetComponent<Spells>();
        oldPos = transform.position;
        audioSource.loop = true;
        audioSource.clip = enemySFX[Random.Range(0, 1)];
        audioSource.Play();
    }

    void Update()
    {
        if(spells.rewindCasting)
        {
            currentRange = rewindRange;
            currentSpeed = rewindSpeed;
            if (!alive)
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            currentRange = smallRange;
            currentSpeed = normalSpeed;
        }
        if (!alive)
        {
            if(timer > respawnTime)
            {
                alive = true;
                transform.position = oldPos;
                timer = 0;
            }
        }

        if (Vector3.Distance(player.transform.position, transform.position) < currentRange && alive)
        {
            if (!chargingStarted)
            {
                chargingStarted = true;
                audioSource.Stop();
                audioSource.clip = enemySFX[Random.Range(0, 1)]; // replace with charging sound.
                audioSource.Play();
            }
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);
        }
        else if (alive)
        {
            if (chargingStarted)
            {
                audioSource.Stop();
                audioSource.clip = enemySFX[Random.Range(0, 1)];
                audioSource.Play();
            }
            chargingStarted = false;
            transform.position = Vector3.MoveTowards(transform.position, oldPos, currentSpeed * Time.deltaTime);
        }
        else
        {
            // transform.rotation = Vector3.RotateTowards(new Vector3(0,0,0), new Vector3(0, 90, 0), 1);
            // Play death animation and turn of collider?
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, smallRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rewindRange);
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && alive)
        {
            other.gameObject.GetComponent<PlayerStats>().KillPlayer();
        }
    }
}
