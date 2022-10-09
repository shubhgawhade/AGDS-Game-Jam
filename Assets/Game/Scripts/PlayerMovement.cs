using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;

    public static GameObject Player;
    
    [SerializeField] private GameObject mouseLoc;
    
    public Vector3 currentDestination;
    private float startTime;
    private Vector3 dir;
    private Rigidbody rb;

    private bool deathLoc;
    public bool move;
    public float movementSpeed;
    public float rotationSpeed;
    public float rateOfLoss;
    public float damageRate;

    private NavMeshAgent agent;

    private Vector3 startPos;
    
    RaycastHit hit;

    private void Awake()
    {
        Player = gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        currentDestination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            // print((hit.point - transform.position).magnitude);
            if ((hit.point - transform.position).magnitude < 15)
            {
                mouseLoc.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
                // mouseLoc.transform.position = hit.point;
            }
            
            if (Input.GetMouseButtonDown(0) && !hit.collider.CompareTag("NotWalkable"))
            {
                move = true;
                currentDestination = new Vector3(hit.point.x, 0, hit.point.z);
                startTime = Time.time;
            }
        }

        if (transform.localScale.y > 0.1f)
        {
            Move();
        }
        else
        {
            if (!deathLoc)
            {
                GameManager.lastDeath.Add(transform.position);
                GameManager.DistanceTravelled = Mathf.Abs((startPos - transform.position).magnitude);
                print(GameManager.DistanceTravelled);
                deathLoc = true;
            }
            move = false;
            currentDestination = transform.position;
            GameManager.isDead = true;
        }
    }

    private void Move()
    {   
        dir = currentDestination - transform.position;
        // print(dir.magnitude);
        
        float distanceTime = Mathf.Clamp((Time.time - startTime) * movementSpeed, 0, movementSpeed * 2);
        if (dir.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // agent.SetDestination(currentDestination);

            transform.position = Vector3.Lerp(transform.position, currentDestination, distanceTime / dir.magnitude);
            rb.MovePosition(transform.position + movementSpeed * Time.deltaTime * dir);
        }
        else
        {
            move = false;
        }

        if (move)
        {
            transform.localScale -= new Vector3(0, rateOfLoss, 0);
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        // print(collision.collider.name);
        if (collision.collider.CompareTag("Health"))
        {
            Destroy(collision.collider.gameObject);
            AddHealth add = gameObject.AddComponent<AddHealth>();
            add.RateOfReplenish = 0.02f;
        }
        
        if(collision.collider.CompareTag("EnemyBullets"))
        {
            collision.gameObject.SetActive(false);
            transform.localScale -= new Vector3(0, 0.003f, 0);
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.collider.CompareTag("Enemy") || collisionInfo.collider.CompareTag("Robot") && transform.localScale.y > 0.1f)
        {
            transform.localScale -= new Vector3(0, damageRate, 0);
        }
        
    }
}
