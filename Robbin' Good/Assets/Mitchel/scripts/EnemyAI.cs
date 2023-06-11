using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    #region Variables
    [Header("Vision/State")]
    public float viewRadius;
    public float viewAngle;
    float distanceToTarget;

    Coroutine detect;

    public Vector3 playerDetection;
    private Vector3 playerPos;
    private Vector3 zero;

    public LayerMask targetPlayer;
    public LayerMask obstacles;

    public GameObject player;
    public GameObject lookTowards;

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
        detect = null;

        currentState = GuardStates.Patrol;

        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
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

                detect = StartCoroutine(Detection(1));

                break;

            case GuardStates.Seen:

                StartCoroutine(SeenMove(2));

                Debug.Log("Seen and waiting to move");

                break;

            case GuardStates.Alert:

                Debug.Log("Spotted");

                if (distanceToTarget <= 3)
                {
                    agent.isStopped = true;
                    transform.LookAt(lookTowards.transform.position);
                } else
                  {
                    agent.isStopped = false;
                    agent.destination = playerPos;
                  }

                break;

            default:
                Debug.Log("No State Linked");
                break;
        }
    }
    #endregion

    #region Vision
    void Vision()
    {
        playerPos = player.transform.position;

        Vector3 playerTarget = player.transform.position - transform.position;

        if (Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2)
        {
            if (distanceToTarget < viewRadius)
            {
                if (Physics.Raycast(transform.position, playerTarget, distanceToTarget, obstacles) == false)
                {
                    Debug.Log("Seeing");
                    playerDetection = player.transform.position;
                    detect = StartCoroutine(Detection(2));
                }
                else if (distanceToTarget > viewRadius && playerDetection != zero)
                {
                    StopCoroutine(detect);

                    currentState = GuardStates.Seen;
                }

            }
        }
        else if (detect != null)
        {
            StopCoroutine(detect);
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

    #region IEnumerator Detector Alert
    private IEnumerator Detection(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            currentState = GuardStates.Alert;
        }
    }
    #endregion

    #region Seen Move
    private IEnumerator SeenMove(float waitMove) 
    {
        while(true)
        {
            yield return new WaitForSeconds(waitMove);

            agent.destination = playerDetection;

            yield return new WaitForSeconds(waitMove);

            playerDetection = zero;
            currentState = GuardStates.Patrol;
        }
    }

    #endregion
}
