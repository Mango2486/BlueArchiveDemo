using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyTargetNotFoundState : NormalEnemyBaseState
{
    public NormalEnemyTargetNotFoundState(NormalEnemyStateMachine currentContext, NormalEnemyStateFactory currentFactory) : base(currentContext, currentFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
       Debug.Log("Enter TargetNotFound");
       InitialSubState();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        
    }

    public override void ExitState()
    {
       Debug.Log("Exit TargetNotFound"); 
    }

    public override void CheckSwitchStates()
    {
        if (Context.TargetFound())
        {   
            Context.SetTarget(Context.detector.GetPlayerTransform());
            SwitchState(Factory.TargetFound());
        }
    }

    public override void InitialSubState()
    {
       SetSubState(Factory.Patrol());
    }
}
