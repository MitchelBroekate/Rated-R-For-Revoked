using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardStateManager : MonoBehaviour
{
    GuardBaseState currentState;
    GuardPatrolState patrolState = new GuardPatrolState();
    GuardCautionState GuardCautionState = new GuardCautionState();
    GuardSearchState GuardSearchState = new GuardSearchState();
    GuardAlertState guardAlertState = new GuardAlertState();

    NavMeshAgent agent;
    public Transform[] checkPoints;
    void Start()
    {
        currentState = patrolState;

        currentState.EnterState(this);
    }


    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GuardBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
