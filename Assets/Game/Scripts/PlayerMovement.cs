using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject mouseLoc;
    
    public Vector3 currentDestination;
    private float startTime;
    private Vector3 dir;

    public bool move;
    public float movementSpeed;
    public float rotationSpeed;
    public float rateOfLoss;

    private NavMeshAgent agent;
    
    RaycastHit hit;
    
    // Start is called before the first frame update
    void Start()
    {
        currentDestination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            // print((hit.point - transform.position).magnitude);
            if ((hit.point - transform.position).magnitude < 9)
            {
                mouseLoc.transform.position = hit.point;
            }
            
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Ground"))
            {
                move = true;
                currentDestination = new Vector3(hit.point.x, 0, hit.point.z);
                startTime = Time.time;
            }
        }

        if (transform.localScale.y > 0.1f)
        {
            Move();
        }
        else
        {
            move = false;
            currentDestination = transform.position;
        }
    }

    private void Move()
    {   
        dir = currentDestination - transform.position;
        // print(dir.magnitude);
        
        float distanceTime = (Time.time - startTime) * movementSpeed;
        if (dir.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // agent.SetDestination(currentDestination);

            transform.position = Vector3.Lerp(transform.position, currentDestination, distanceTime / dir.magnitude);
            transform.position += movementSpeed * Time.deltaTime * dir;
        }
        else
        {
            move = false;
        }

        if (move)
        {
            transform.localScale -= new Vector3(0, rateOfLoss, 0);
        }
    }
}
