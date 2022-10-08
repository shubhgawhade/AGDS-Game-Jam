using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot1 : Attacks
{
    [SerializeField] private GameObject bullets;

    [SerializeField] private List<GameObject> reusableBullets;
    
    private Timer timer;

    private bool spawned;

    private void Awake()
    {
        timer = gameObject.AddComponent<Timer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        NumOfBullets = 3;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!spawned)
        {
            
            for (int i = 0; i < NumOfBullets; i++)
            {
                Quaternion rot = transform.rotation;
                switch (NumOfBullets)
                {
                    case 1:
                        // rot = transform.rotation;
                        break;
                        
                    case 2:
                        if (i == 0)
                        {
                            // print(transform.rotation);
                            
                            // rot.y -= 30;
                            rot = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 30, transform.eulerAngles.z);
                            // print(rot);
                        }
                        else
                        {
                            // rot.y += 30;
                            rot = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 30, transform.eulerAngles.z);
                        }
                        break;
                }
                
                if (!BulletsLeft())
                {
                    // print("EXP");
                    GameObject a = Instantiate(bullets, transform.position, rot);
                    a.GetComponent<Bullet>().player = transform.root.GetComponent<Rigidbody>();
                    reusableBullets.Add(a);
                }
                else
                {
                    foreach (GameObject a in reusableBullets)
                    {
                        if (!a.activeSelf)
                        {
                            a.GetComponent<Bullet>().player = transform.root.GetComponent<Rigidbody>();
                            a.transform.position = transform.position;
                            a.transform.rotation = rot;
                            a.SetActive(true);
                            break;
                        }
                    }
                }
            }
            
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

    private bool BulletsLeft()
    {
        foreach (GameObject a in reusableBullets)
        {
            if (!a.activeSelf)
            {
                return true;
            }
        }

        return false;
    }
    
}
