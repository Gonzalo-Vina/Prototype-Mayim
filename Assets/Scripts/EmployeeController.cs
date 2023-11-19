using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmployeeController : MonoBehaviour
{
    private float posX, posY;

    NavMeshAgent agent;
    
    void Start()
    {
        posX = gameObject.transform.position.x;
        posY = gameObject.transform.position.y;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        
    }

    public void Move(Vector3 mousePosition)
    {
        agent.SetDestination(mousePosition);
    }
}
