using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject boxOfParts;
    
    public Vector3 lastDeath;
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(boxOfParts, lastDeath, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
