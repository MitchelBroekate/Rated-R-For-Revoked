using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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
    float slerpTime = 8;

    IEnumerator alert;

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
        Alert
    }
    #endregion

    #region Update, Start en Collision
    void Start()
    {
        alert = AlertState(2);

        currentState = GuardStates.Patrol;

        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
        SetCheckGuardState(currentState);
        Vision();

    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.transform.tag == "Player")
        {
            currentState = GuardStates.Alert;
        }
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

                alert = AlertState(0.1f);

                break;

            case GuardStates.Alert:

                Debug.Log("Spotted");

                if (distanceToTarget <= 1)
                {
                    agent.isStopped = true;
                    Quaternion LookOnLook = Quaternion.LookRotation(lookTowards.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, LookOnLook, Time.deltaTime * slerpTime);
                    player.transform.GetComponent<PlayerHealth>().playerHealth--; 
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

                    StartCoroutine(alert);

                }

            }

        }
       else
       {
           StopAllCoroutines();
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

    #region IEnumerator

    private IEnumerator AlertState(float waitAlert) 
    {
        while (true) 
        {
            Debug.Log("detecting");
            agent.isStopped = true;

            yield return new WaitForSeconds(waitAlert);

            agent.isStopped = false;
            currentState = GuardStates.Alert;
        }
    }
    #endregion
}
