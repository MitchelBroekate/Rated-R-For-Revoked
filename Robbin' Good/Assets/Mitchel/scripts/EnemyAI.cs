using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    #region Variables
    [Header("Vision/State")]
    public float viewRadius;
    public float viewAngle;

     public Vector3 playerDetection;
    public Vector3 aiDistance;

    public LayerMask targetPlayer;
    public LayerMask obstacles;

    public GameObject player;

    private IEnumerator coroutine;

    public GuardStates currentState;

    [Header("Patrol")]
    public Transform[] checkPoints;
    public int destination = 0;
    NavMeshAgent agent;
    float moveSpeed;
    #endregion

    #region enum
    public enum GuardStates
    {
        Patrol,
        Caution,
        Seen,
        Alert
    }
    #endregion

    #region "Update and start"
    void Start()
    {
        currentState = GuardStates.Patrol;

        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

        SetCheckGuardState(currentState);
    }

    void Update()
    {

        Vector3 playerTarget = player.transform.position - transform.position;

        if (Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToTarget < viewRadius)
            {
                if (Physics.Raycast(transform.position, playerTarget, distanceToTarget, obstacles) == false)
                {
                    playerDetection = player.transform.position;
                    StopAllCoroutines();
                    coroutine = Detection(3);
                    StartCoroutine(coroutine);
                }
            }
            if (distanceToTarget > viewRadius)
            {

            }
        }

        SetCheckGuardState(currentState);
    }
    #endregion

    #region Switch
    public void SetCheckGuardState(GuardStates states)
    {

        switch (states)
        {
            case GuardStates.Patrol:

                if (checkPoints.Length == 0)
                {
                    return;
                }
                agent.destination = checkPoints[destination].position;

                destination = (destination + 1) % checkPoints.Length;

                if(currentState != GuardStates.Patrol)
                {
                
                }

                break;

            case GuardStates.Caution:

                break;

            case GuardStates.Seen:

                break;

            case GuardStates.Alert:

                Debug.Log("Spotted");
                agent.destination = playerDetection + ;

                break;

            default:
                Debug.Log("No State Linked");
                break;
        }
    }
        #endregion

        #region IEnumerator
    private IEnumerator Detection(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            currentState = GuardStates.Alert;
        }
    }
    #endregion

}
