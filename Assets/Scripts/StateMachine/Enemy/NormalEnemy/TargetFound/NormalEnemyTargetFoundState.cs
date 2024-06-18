using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyTargetFoundState : NormalEnemyBaseState
{
    public NormalEnemyTargetFoundState(NormalEnemyStateMachine currentContext, NormalEnemyStateFactory currentFactory) : base(currentContext, currentFactory)
    {
        IsRootState = true;
    }

    private bool isAnimationEnd;
    public override void EnterState()
    {
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
        //把子状态清空
        ClearSubState();
        //同时去掉寻路的Target
        Context.SetTarget(null);
        //速度归零
        Context.NavMeshAgent.speed = 0f;
    }
    

    public override void CheckSwitchStates()
    {
        if (!Context.TargetFound())
        {
            SwitchState(Factory.TargetNotFound());
        }
        //判断是否死亡
        if (Context.enemyUIController.EnemyDie())
        {
            SwitchState(Factory.Die());
        }
        
    }

    public override void InitialSubState()
    {
       SetSubState(Factory.Trace());
    }
    
    
}
