using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Bullet : Attacks
{
    public Rigidbody player;
    
    public Rigidbody rb;

    public float bulletSpeed;
    public float bulletLife;
    
    private Timer timer;
    
    private void Awake()
    {
        timer = gameObject.AddComponent<Timer>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.AddForce(player.velocity + transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    private void OnEnable()
    {
        if (player != null)
        {
            rb.AddForce(player.velocity + transform.forward * bulletSpeed, ForceMode.Impulse);
        }
        timer.Duration = bulletLife;
        timer.Run();
    }

    private void Update()
    {
        if (timer.Finished)
        {
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // print(other.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Robot") || collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<AICharacterControl1>().health -= Damage;
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        timer.started = false;
        rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}
