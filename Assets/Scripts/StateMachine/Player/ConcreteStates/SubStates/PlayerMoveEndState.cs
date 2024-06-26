using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveEndState : PlayerBaseState
{
    public PlayerMoveEndState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    private bool isAnimationEnd = false;
    
    public override void EnterState()
    {
        ReSetVelocity();
        Context.PlayerAnimator.Play("StandMoveEnd");
    }

    public override void UpdateState()
    {   
        AnimationEnd();
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
        //无操作播放动画
        if (isAnimationEnd)
        {
            SwitchState(Factory.Idle());
        }
        //移动
        else if (Context.PlayerInput.MoveDirection != Vector2.zero)
        {
            SwitchState(Factory.Move());
        }
        //瞄准
        if (Context.PlayerInput.IsAiming)
        {
            SwitchState(Factory.AimStart());
        }
    }

    public override void InitialSubState()
    {
       
    }

    private void AnimationEnd()
    {
        AnimatorStateInfo animatorStateInfo;
        animatorStateInfo = Context.PlayerAnimator.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.normalizedTime >= 1 && animatorStateInfo.IsName("StandMoveEnd"))
        {
            isAnimationEnd = true;
        }
        else
        {
            isAnimationEnd = false;
        }
    }

    private void ReSetVelocity()
    {
        Context.PlayerRigidbody.velocity = Vector3.zero;
    }
}
