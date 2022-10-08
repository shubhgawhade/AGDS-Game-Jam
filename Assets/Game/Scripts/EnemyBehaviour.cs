using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyBehaviour : MonoBehaviour
{
    private Timer timer;
    
    public int health;

    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = gameObject.AddComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 1)
        {
            Destroy(gameObject);
        }
        
        if (timer.Finished)
        {
            timer.started = false;
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<AICharacterControl>().agent.updatePosition = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("PlayerBullets"))
        {
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<AICharacterControl>().agent.updatePosition = false;
            timer.Duration = 1f;
            timer.Run();
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.back * 5, ForceMode.Impulse);
        }
    }
}
