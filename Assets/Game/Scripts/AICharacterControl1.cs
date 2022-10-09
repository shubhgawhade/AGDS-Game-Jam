using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using Random = UnityEngine.Random;

public class AICharacterControl1 : MonoBehaviour
{
    private Shoot1 shoot;
    
    [SerializeField] private GameObject healthDrops;
    [SerializeField] private GameObject player;
    
    public ThirdPersonCharacter character;
    private AISpawner ais;
    public int health;
    
    private Rigidbody rb;
    private Timer timer;
    private bool isAttacking;

    public NavMeshAgent agent { get; private set; } // the navmesh agent required for the
                                    // // the character we are controlling
    public Transform target; // target to aim for


    private void Start()
    {
        player = PlayerMovement.Player;
        shoot = GetComponent<Shoot1>();

        if (GetComponent<ThirdPersonCharacter>())
        {
            character = GetComponent<ThirdPersonCharacter>();
        }
        
        rb = GetComponent<Rigidbody>();
        timer = gameObject.AddComponent<Timer>();
        ais = Camera.main.GetComponent<AISpawner>();

        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updatePosition = true;
    }

    private void OnEnable()
    {
        target = null;
    }

    private void Update()
    {

        /*
        if (dist < 8)
        {
            isAttacking = true;
            agent.updatePosition = false;
        }
        else if (isAttacking && dist < 12)
        {
            target = player.transform;
            isAttacking = false;
        }

        if (target)
        {
            float dist = (target.transform.position - transform.position).magnitude;
            if (dist < 1f && target != player)
            {
            print("SWITCH" + "" + target);
                agent.updatePosition = false;
                target = null;
            }
        }
        */

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
            agent.updatePosition = true;
            // print("A");
        }
        else
        { 
            float distFromPlayer = (player.transform.position - transform.position).magnitude;
            
            if (target.gameObject != player)
            {
                shoot.enabled = false;

                float dist = (target.transform.position - transform.position).magnitude;
                if (dist < 1f)
                {
                    print("SWITCH" + "" + target);
                    if (gameObject.activeSelf)
                    {
                        StartCoroutine(Wait());
                    }
                }

                if (distFromPlayer < 12)
                {
                    target = player.transform;
                }
                
            }
            else
            {
                // Stop and attack player when in range
                if (player != null && health > 0)
                {
                    
                    // print(distFromPlayer);
                    if (distFromPlayer > 7 && distFromPlayer < 8)
                    {
                        target = player.transform;
                        StartCoroutine(WaitBeforeAttack());
                    }
                    else if(distFromPlayer > 8)
                    {
                        print("LEAVE");
                        shoot.enabled = false;
                        
                        if (distFromPlayer > 12)
                        {
                            target = ais.patrolLocs[Random.Range(0, ais.patrolLocs.Length)];
                        }
                    }
                }
                
            }
        }

        if (GetComponent<ThirdPersonCharacter>())
        {
            character.Move(agent.desiredVelocity, false, false);
        }
            
        if (agent.updatePosition && GetComponent<NavMeshAgent>().enabled)
        {
            agent.SetDestination(target.position);
        }
    }

    IEnumerator WaitBeforeAttack()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<NavMeshAgent>().enabled = false;
        agent.updatePosition = false;
        Shoot();
    }

    private void Shoot()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            print(gameObject);
            transform.LookAt(target);
            shoot.enabled = true;
        }

        GetComponent<NavMeshAgent>().enabled = true;
        agent.updatePosition = true;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        agent.updatePosition = false;
        target = null;
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
        timer.started = false;
        GetComponent<NavMeshAgent>().enabled = true;

        if (!GameManager.isDead)
        {
            Instantiate(healthDrops, transform.position, Quaternion.identity);
        }
    }
}
