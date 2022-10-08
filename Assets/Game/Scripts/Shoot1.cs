using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot1 : MonoBehaviour
{
    [SerializeField] private GameObject bullets;

    private Timer timer;

    private bool spawned;

    private void Awake()
    {
        timer = gameObject.AddComponent<Timer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            // Instantiate(bullets, transform.position, Quaternion.identity);
            spawned = true;

            timer.Duration = 1f;
            timer.Run();
        }
        else
        {
            if (timer.Finished)
            {
                // Destroy(timer);
                timer.started = false;
                spawned = false;
            }
        }
    }
}
