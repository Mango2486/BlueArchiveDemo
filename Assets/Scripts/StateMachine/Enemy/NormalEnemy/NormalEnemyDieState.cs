using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyDieState : NormalEnemyBaseState
{
    public NormalEnemyDieState(NormalEnemyStateMachine currentContext, NormalEnemyStateFactory currentFactory) : base(currentContext, currentFactory)
    {   
        //死亡作为一个主状态，因为不可能在死亡的状态下还保持其他状态
        IsRootState = true;
    }

    public override void EnterState()
    {   
        Debug.Log("Enemy Die");
        //播放死亡动画
        Context.EnemyAnimator.Play("Die");
        //可以加一点爆炸粒子特效之类的？

        //死亡动画播放完后消失，回到敌人对象池
    }

    public override void UpdateState()
    {
        
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
