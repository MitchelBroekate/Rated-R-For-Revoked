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

    int test = 10;
    public enum TestEnum
    {
        a,
        b
    }

    public TestEnum testEnum;

    NavMeshAgent agent;
    public Transform[] checkPoints;
    void Start()
    {
        currentState = patrolState;

        currentState.EnterState(this);


    }

    public void SetAndCheckState(TestEnum newEnum)
    {
        testEnum = newEnum;
        switch (testEnum)
        {
            case TestEnum.a:
                // do this;
                break;
            case TestEnum.b:
                // do this....
                break;
            default:
                // else
                break;

        }
        }
    }
    public void TestThing()
    {
        //Checks distance
        SetAndCheckState(TestEnum.b);
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
