using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStandAimStartState : PlayerBaseState
{
    public PlayerStandAimStartState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    private bool isAnimationEnd = false;
    public override void EnterState()
    {
        ReSetVelocity();
        Context.PlayerAnimator.Play("StandAimStart");
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
        //瞄准完成，进入瞄准状态
        if (isAnimationEnd)
        {
            if (Context.PlayerInput.IsAiming)
            {
                SwitchState(Factory.StandAim());
            }
            else
            {
                SwitchState(Factory.StandAimEnd());
            }
        }
        //移动放弃瞄准
        if (Context.PlayerInput.IsMovePressed)
        {
            SwitchState(Factory.StandMove());
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

    private void ReSetVelocity()
    {
        Context.PlayerRigidbody.velocity = Vector3.zero;
    }
}
