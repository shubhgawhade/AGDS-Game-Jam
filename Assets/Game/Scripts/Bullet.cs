using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Attacks
{
    public Rigidbody player;
    
    private Rigidbody rb;

    public float bulletSpeed;
    public float bulletLife;
    
    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = bulletLife;
        timer.Run();
        rb.AddForce(player.velocity + transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    private void Update()
    {
        if (timer.Finished)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // print(other.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<EnemyBehaviour>().health -= Damage;
            Destroy(gameObject);
        }
    }
}
