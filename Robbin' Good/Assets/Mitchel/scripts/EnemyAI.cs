using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    #region Variables
    [Header("Vision")]
    public float viewRadius;
    public float viewAngle;

    public LayerMask targetPlayer;
    public LayerMask obstacles;

    public GameObject player;

    private IEnumerator coroutine;

    public GuardStates currentState;

    [Header("Patrol")]
    public Transform[] checkPoints;
    public int destination = 0;
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
                    StopAllCoroutines();
                    coroutine = Detection(3);
                    StartCoroutine(coroutine);
                    Debug.Log("Holy shieee");
                    Vector3 playerDetection = player.transform.position;
                }
            }
            if (distanceToTarget > viewRadius)
            {

            }
        }

    }
    #endregion

    #region Switch
    public void SetCheckGuardState(GuardStates newEnum)
    {
        Enum updatedGuardState = newEnum;

        switch (updatedGuardState)
        {
            case GuardStates.Patrol:
                
                break;

            case GuardStates.Caution:

                break;

            case GuardStates.Seen:

                break;

            case GuardStates.Alert: 

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

            ChangeStateAlert();
        }
    }
    #endregion

    #region ChangeStates
    void ChangeStatePatrol()
    {

    }
    public void ChangeStateCaution()
    {

    }
    public void ChangeStateSeen()
    {

    }
    public void ChangeStateAlert()
    {

    }
    #endregion
}
