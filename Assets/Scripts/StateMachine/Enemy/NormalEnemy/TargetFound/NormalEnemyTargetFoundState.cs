using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyTargetFoundState : NormalEnemyBaseState
{
    public NormalEnemyTargetFoundState(NormalEnemyStateMachine currentContext, NormalEnemyStateFactory currentFactory) : base(currentContext, currentFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("TargetFound");
        Context.EnemyAnimator.Play("Run");
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
        
    }

    public override void CheckSwitchStates()
    {
        if (!Context.TargetFound())
        {
            SwitchState(Factory.TargetNotFound());
        }
        
    }

    public override void InitialSubState()
    {
       SetSubState(Factory.Trace());
    }
}
