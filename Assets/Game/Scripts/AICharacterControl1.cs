using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using Random = UnityEngine.Random;

public class AICharacterControl1 : MonoBehaviour
{
    [SerializeField] private GameObject healthDrops;
    [SerializeField] private GameObject player;
    
    public ThirdPersonCharacter character;
    private AISpawner ais;
    public int health;
    
    private Rigidbody rb;
    private Timer timer;

    public NavMeshAgent agent { get; private set; } // the navmesh agent required for the
                                    // // the character we are controlling
    public Transform target; // target to aim for

    private void Start()
    {
        if (GetComponent<ThirdPersonCharacter>())
        {
            character = GetComponent<ThirdPersonCharacter>();
        }
        
        rb = GetComponent<Rigidbody>();
        timer = gameObject.AddComponent<Timer>();
        ais = Camera.main.GetComponent<AISpawner>();

        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updatePosition = true;
    }


    private void Update()
    {
        if (health < 1)
        {
            health = 100;
            gameObject.SetActive(false);
            // Destroy(gameObject);
        }
        
        if (timer.Finished)
        {
            timer.started = false;
            agent.updatePosition = true;
            target = player.transform;
            GetComponent<NavMeshAgent>().enabled = true;
        }
        
        
        if (!target)
        {
            target = ais.patrolLocs[Random.Range(0, ais.patrolLocs.Length)];
            print("A");

            if ((target.transform.position - transform.position).magnitude < 1)
            {
                    
            }
        }

        if (GetComponent<ThirdPersonCharacter>())
        {
            character.Move(agent.desiredVelocity, false, false);
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
            player = collision.gameObject.GetComponent<Bullet>().player.gameObject;
            GetComponent<NavMeshAgent>().enabled = false;
            agent.updatePosition = false;
            timer.Duration = 1f;
            timer.Run();
            rb.velocity = Vector3.zero;
            // rb.AddForce(Vector3.back * 20, ForceMode.Impulse);
        }
    }

    private void OnDisable()
    {
        if (!GameManager.isDead)
        {
            Instantiate(healthDrops, transform.position, Quaternion.identity);
        }
    }
}
