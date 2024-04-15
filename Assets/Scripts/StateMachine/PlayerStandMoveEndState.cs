using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandMoveEndState : PlayerBaseState
{
    public PlayerStandMoveEndState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
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
        if (isAnimationEnd)
        {
            SwitchState(Factory.StandIdle());
            Debug.Log("1");
        }
        else if (Context.PlayerInput.MoveDirection != Vector2.zero)
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
