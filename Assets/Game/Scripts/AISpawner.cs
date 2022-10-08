using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    public Transform[] patrolLocs;

    [SerializeField] private GameObject[] enemies;

    [SerializeField] private List<GameObject> spawnedRobots;

    private Timer timer;

    public float AIDelay;
    public bool spawnAI;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = AIDelay;
        timer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnAI)
        {
            Transform loc = spawnPoints[Random.Range(0, spawnPoints.Length)];
            print(loc);
            
            if (!VisibleInViewport(loc))
            {
                //Instantiate AI
                
                if (!RobotsLeft())
                {
                    print("EXP");
                    GameObject a = Instantiate(enemies[Random.Range(0, enemies.Length)], loc.position, Quaternion.identity);
                    spawnedRobots.Add(a);
                }
                else
                {
                    print("ADD");
                    foreach (GameObject a in spawnedRobots)
                    {
                        if (!a.activeSelf)
                        {
                            a.transform.position = loc.position;
                            a.transform.rotation = Quaternion.identity;
                            a.SetActive(true);
                            break;
                        }
                    }
                }
            }
            spawnAI = false;
            timer.Run();
        }

        if (timer.Finished)
        {
            spawnAI = true;
            print("SPAWN");
            timer.started = false;
        }
    }

    private bool VisibleInViewport(Transform loc)
    {
        Vector3 respectToCamera = Camera.main.WorldToViewportPoint(loc.position);
        print(respectToCamera);
        if (respectToCamera.x > 0 && respectToCamera.y > 0)
        {
            return true;
        }

        return false;
    }
    
    private bool RobotsLeft()
    {
        foreach (GameObject a in spawnedRobots)
        {
            if (!a.activeSelf)
            {
                return true;
            }
        }

        return false;
    }
}
