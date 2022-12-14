using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject boxOfParts;
 
    public static bool isDead;
    public static List<Vector3> lastDeath = new List<Vector3>();
    public static bool firstRun = true;
    public static float DistanceTravelled;
    public static float MaxDistanceTravelled;
    public static float MaxTimeSurvived;


    private bool wait;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI timeSurvivedText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI timeCounter;
    [SerializeField] private GameObject timeCounterBG;

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
            gameOverUI.SetActive(true);
            timeCounter.gameObject.SetActive(false);
            timeCounterBG.gameObject.SetActive(false);
            timeSurvivedText.text = "Time Survived: " + timeCounter.text;
            distanceText.text = "Distance Travelled: " + DistanceTravelled.ToString(".00m");
            
            StartCoroutine(Wait(1f));
            if (Input.GetMouseButtonDown(0) && wait)
            {
                firstRun = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }   
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        wait = true;
    }
}
