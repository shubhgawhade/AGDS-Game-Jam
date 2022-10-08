using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Attacks
{
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
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        // print(other.name);
    }
}
