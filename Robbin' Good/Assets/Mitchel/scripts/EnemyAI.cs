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
    private Vector3 playerPos;

    public LayerMask targetPlayer;
    public LayerMask obstacles;

    public GameObject player;

    public GuardStates currentState;

    [Header("Patrol")]
    public Transform[] checkPoints;
    public int destination = 0;
    Vector3 target;
    NavMeshAgent agent;
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


    }

    void Update()
    {

        Vision();
        SetCheckGuardState(currentState);
    }
    #endregion

    #region Switch
    public void SetCheckGuardState(GuardStates states)
    {

        switch (states)
        {
            case GuardStates.Patrol:

                UpdateDestination();
                IterateCheckpointInt();

                break;

            case GuardStates.Caution:

                break;

            case GuardStates.Seen:

                break;

            case GuardStates.Alert:

                playerPos = player.transform.position;

                Debug.Log("Spotted");
                agent.destination = playerPos;

                break;

            default:
                Debug.Log("No State Linked");
                break;
        }
    }
        #endregion

        #region IEnumerator Detector
    private IEnumerator Detection(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            currentState = GuardStates.Alert;
        }
    }
    #endregion

    #region Vision
    public void Vision()
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
                    StartCoroutine(Detection(3));
                }
            }
        }
    }
    #endregion

    #region Patrol Route
    void UpdateDestination()
    {
        target = checkPoints[destination].position;
        agent.SetDestination(target);
    }

    void IterateCheckpointInt()
    {
        if (Vector3.Distance(transform.position, target) < 1)
        {

            destination++;
            if (destination == checkPoints.Length)
            {
                destination = 0;
            }

        }
    }
    #endregion
}
