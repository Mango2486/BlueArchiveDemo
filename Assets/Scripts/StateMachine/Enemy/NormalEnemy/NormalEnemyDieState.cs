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

    private bool isAnimationEnd;
    public override void EnterState()
    {   
        InitialSubState();
        //播放死亡动画
        Context.EnemyAnimator.Play("Die");
        //可以加一点爆炸粒子特效之类的？
        
    }

    public override void UpdateState()
    {
        AnimationEnd();
        if (isAnimationEnd)
        {
            ObjectPoolManager.Instance.Release(ObjectPoolName.ExpBall, new Vector3(Context.transform.position.x, 0.1f, Context.transform.position.z));
            //死亡动画播放完后消失，回到敌人对象池
            ObjectPoolManager.Instance.BackToPool(ObjectPoolName.Sweeper,Context.gameObject);
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
       ClearSubState();
    }
    
    private void AnimationEnd()
    {
        AnimatorStateInfo animatorStateInfo;
        animatorStateInfo = Context.EnemyAnimator.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.normalizedTime >= 1 && animatorStateInfo.IsName("Die"))
        {
            isAnimationEnd = true;
        }
        else
        {
            isAnimationEnd = false;
        }
    }
}
