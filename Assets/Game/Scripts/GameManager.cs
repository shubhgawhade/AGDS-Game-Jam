using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject boxOfParts;
 
    public static bool isDead;
    public static List<Vector3> lastDeath = new List<Vector3>();

    public static bool firstRun = true;
    
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        
        if (!firstRun)
        {
            foreach (Vector3 loc in lastDeath)
            {
                // print("AAA");
                Instantiate(boxOfParts, loc, Quaternion.identity);
                firstRun = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isDead)
        {
            firstRun = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }   
    }
}
