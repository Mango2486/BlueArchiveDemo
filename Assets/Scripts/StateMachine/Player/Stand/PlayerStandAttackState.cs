using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandAttackState : PlayerBaseState
{
    public PlayerStandAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }
    
    private float shootInterval = 0.5f;
    private float currentInterval = 0f;
    public override void EnterState()
    {
        Shoot();
    }

    public override void UpdateState()
    {
        currentInterval += Time.deltaTime;
        if (currentInterval > shootInterval)
        {
            Shoot();
            currentInterval = 0;
        }
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
       //停止射击但是还是瞄准状态
       if (Context.PlayerInput.IsAiming && !Context.PlayerInput.IsAttacking)
       {
           SwitchState(Factory.StandAim());
       }
       //停止射击并且停止瞄准
       if (!Context.PlayerInput.IsAttacking && !Context.PlayerInput.IsAiming)
       {
           SwitchState(Factory.StandAimEnd());
       }
       //移动
       if (Context.PlayerInput.IsMovePressed)
       {
           SwitchState(Factory.StandMove());
       }
       
    }

    public override void InitialSubState()
    {
        
    }

    private void Shoot()
    {
        //先只播放动画
        Context.PlayerAnimator.Play("StandAttack",0,0);
    }
}
