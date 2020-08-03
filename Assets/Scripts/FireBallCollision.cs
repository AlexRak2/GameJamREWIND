using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallCollision : MonoBehaviour
{
    [SerializeField] ParticleSystem FireBallHit;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        print("Working" + other.gameObject);

        Destroy(this.gameObject, 0.06f);
        Instantiate(FireBallHit, other.transform.position, Quaternion.identity);

    }
}
