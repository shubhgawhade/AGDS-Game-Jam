using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    private TextMeshProUGUI timerUI;

    private float time;
    
    private void Awake()
    {
        time = 0;
        timerUI = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isDead)
        {
            time += Time.deltaTime;
            timerUI.text = time.ToString("#00:00.00 s");
        }
    }
}
