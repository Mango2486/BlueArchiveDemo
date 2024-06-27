using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimEndState : PlayerBaseState
{
    public PlayerAimEndState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    private bool isAnimationEnd = false;
    public override void EnterState()
    {
       Context.PlayerAnimator.Play("AimEnd");
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
        //无操作
        if (isAnimationEnd)
        {
            SwitchState(Factory.Idle());
        }
        //重新瞄准
        if (Context.PlayerInput.IsAiming)
        {
            SwitchState(Factory.AimStart());
        }
        //移动
        if (Context.PlayerInput.IsMovePressed)
        {
            SwitchState(Factory.Move());
        }
    }

    public override void InitialSubState()
    {

    }
    
    private void AnimationEnd()
    {
        AnimatorStateInfo animatorStateInfo;
        animatorStateInfo = Context.PlayerAnimator.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.normalizedTime >= 1 && animatorStateInfo.IsName("StandAimStart"))
        {
            isAnimationEnd = true;
        }
        else
        {
            isAnimationEnd = false;
        }
    }
}
