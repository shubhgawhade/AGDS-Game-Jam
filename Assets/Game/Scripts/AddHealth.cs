using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : MonoBehaviour
{
    private Timer timer;
    
    private float RateOfReplenish = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 1f;
        timer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.Finished || transform.localScale.y > 0.9f)
        {
            Destroy(this);
            Destroy(timer);
        }
        transform.localScale += new Vector3(0, RateOfReplenish, 0);
    }
}
