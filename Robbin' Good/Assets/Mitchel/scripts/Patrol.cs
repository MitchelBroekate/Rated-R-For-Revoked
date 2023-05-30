using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public Transform[] checkPoints;
    int destination = 0;
    NavMeshAgent  agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

        NextPoint();        
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            NextPoint();
        }
        
    }

    void NextPoint()
    {
        if(checkPoints.Length == 0)
        {
            return;
        }
            agent.destination = checkPoints[destination].position;

            destination = (destination + 1) % checkPoints.Length;
        
    }
}
