using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Key : MonoBehaviour
{


    // Local variables
    [SerializeField] float spinAmount = 100f;
    [SerializeField] float bounceAmount = 0.5f;
    [SerializeField] float maxHeight = 2.5f;
    [SerializeField] float minHeight = 1.2f;
    bool movingUp = true;

    [SerializeField] ParticleSystem ParticleAwake;
    [SerializeField] ParticleSystem PickUp;

    bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        ParticleAwake.Play();
        maxHeight = maxHeight + transform.position.y;
        minHeight = minHeight + transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        RotateAndBounce();
    }

    public void Flip()
    {
        movingUp = !movingUp;
        spinAmount = -spinAmount;
    }

    private void RotateAndBounce()
    {
        float rotateAmount = spinAmount * Time.deltaTime;
        float localBounce;

        this.transform.Rotate(0, rotateAmount, 0, Space.Self);

        if (this.transform.position.y >= maxHeight && movingUp)
        {
            movingUp = false;
        }

        if (this.transform.position.y <= minHeight && !movingUp)
        {
            movingUp = true;
        }

        if (movingUp)
        {
            localBounce = bounceAmount;
        }
        else
        {
            localBounce = -bounceAmount;
        }

        float liftAmount = localBounce * Time.deltaTime;
        this.transform.position = this.transform.position + new Vector3(0, liftAmount, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger && other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>().AddKey();
            ParticleAwake.Stop();
            Transform target = FindObjectOfType<Player>().transform;
            var pickup = Instantiate(PickUp, target.position, Quaternion.identity, target);
            pickup.transform.localPosition = new Vector3(1.5f, 2.9f, 0f);
            Destroy(gameObject);
        }
    }
}
