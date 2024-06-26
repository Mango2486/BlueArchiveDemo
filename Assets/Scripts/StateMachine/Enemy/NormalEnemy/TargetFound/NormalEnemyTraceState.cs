using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyTraceState : NormalEnemyBaseState
{
    public NormalEnemyTraceState(NormalEnemyStateMachine currentContext, NormalEnemyStateFactory currentFactory) : base(currentContext, currentFactory)
    {
    }

    public override void EnterState()
    {
        if (Context.Target != null)
        {
            Context.NavMeshAgent.speed = Context.enemyUIController.EnemyModel.Speed;
        }
        Context.EnemyAnimator.Play("Run");
    }

    public override void UpdateState()
    {
        if (Context.Target != null)
        {
            Context.NavMeshAgent.SetDestination(Context.Target.position);
        }
    }

    public override void FixedUpdateState()
    {
       
    }

    public override void ExitState()
    {
        
    }

    public override void CheckSwitchStates()
    {
       
    }

    public override void InitialSubState()
    {
        
    }
}
