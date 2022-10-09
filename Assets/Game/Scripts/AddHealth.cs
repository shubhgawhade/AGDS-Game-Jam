using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : MonoBehaviour
{
    private Timer timer;
    public float RateOfReplenish;
    private float rateOfReplenish = 0.002f;

    // Start is called before the first frame update
    void Start()
    {
        rateOfReplenish = RateOfReplenish;
        // print(rateOfReplenish);
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
        transform.localScale += new Vector3(0, rateOfReplenish, 0);
    }
}
