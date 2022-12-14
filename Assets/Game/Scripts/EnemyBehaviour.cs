using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{
    private Timer timer;
    private AISpawner ais;
    private AICharacterControl aicc;
    
    public int health;
    public Transform target;
    
    private Rigidbody rb;
    public NavMeshAgent agent { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = gameObject.AddComponent<Timer>();
        
        ais = Camera.main.GetComponent<AISpawner>();
        if (gameObject.GetComponent<AICharacterControl>() != null)
        {
            aicc = GetComponent<AICharacterControl>();
        }

        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updatePosition = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (aicc)
        {
            target = aicc.target;
        }
        
        if (health < 1)
        {
            health = 100;
            gameObject.SetActive(false);
            // Destroy(gameObject);
        }
        
        if (timer.Finished)
        {
            timer.started = false;
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<AICharacterControl>().agent.updatePosition = true;
        }
        
        
        if (!target)
        {
            target = ais.patrolLocs[Random.Range(0, ais.patrolLocs.Length)];
            print("A");

            if ((target.transform.position - transform.position).magnitude < 1)
            {
                    
            }
        }
            
        if (agent.updatePosition)
        {
            agent.SetDestination(target.position);
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
            rb.AddForce(Vector3.back * 20, ForceMode.Impulse);
        }
    }
}
