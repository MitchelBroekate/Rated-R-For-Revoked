using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;

    public LayerMask targetPlayer;
    public LayerMask obstacles;

    public GameObject player;

    public enum GuardStates
    {
        Patrol,
        Caution,
        Seen,
        Alert
    }

    public GuardStates currentState;

    void Start()
    {
        currentState = GuardStates.Patrol;

    }

    void Update()
    {
       

        Vector3 playerTarget = player.transform.position - transform.position;

        if (Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2 )
        {
            float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToTarget < viewRadius)
            {
                if(Physics.Raycast(transform.position, playerTarget, distanceToTarget, obstacles) == false)
                {
                    Debug.Log("Holy shieee");
                }
            }
        }
    }

    public void SetCheckGuardState(GuardStates newEnum)
    {
        Enum updatedGuardState = newEnum;

        switch (updatedGuardState)
        {
            case GuardStates.Patrol:
                
                break;
        }
    }
}
