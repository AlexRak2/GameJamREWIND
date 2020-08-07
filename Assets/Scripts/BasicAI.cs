using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
    public Spells spells;
    public float smallRange;
    public float rewindRange;
    public float currentRange;
    public float normalSpeed;
    public float rewindSpeed;
    public float currentSpeed;
    public float respawnTime;
    public float timer;
    public bool alive;

    public GameObject player;
    private Vector3 oldPos;

    [SerializeField] AudioClip[] enemySFX;
    AudioSource audioSource;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spells = player.GetComponent<Spells>();
        oldPos = transform.position;
    }
    // Update is called once per frame
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
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);
        }
        else if (alive)
        {
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
            //alive = false;
            other.gameObject.GetComponent<PlayerStats>().KillPlayer();
        }
    }
}
