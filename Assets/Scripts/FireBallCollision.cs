using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallCollision : MonoBehaviour
{
    [SerializeField] ParticleSystem fireBallHit;
    ParticleSystem PSystem;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    void Start()
    {
        PSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {

        int collNum = PSystem.GetSafeCollisionEventSize();
        PSystem.GetCollisionEvents(other, collisionEvents);
        Instantiate(fireBallHit, collisionEvents[collNum - 1].intersection, Quaternion.identity);
        Destroy(this.gameObject, 0.06f);

        if (other.gameObject.GetComponent<BasicAI>() != null)
        {
            other.gameObject.GetComponent<BasicAI>().alive = false;
        }
    }
}
