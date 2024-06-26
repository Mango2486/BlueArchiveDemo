using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAimState : PlayerBaseState
{
    public PlayerAimState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }
    
    public override void EnterState()
    {
        Context.PlayerAnimator.Play("StandAim");
    }

    public override void UpdateState()
    {   
        Context.RotateToAimPoint();
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
        //松开瞄准
        if (!Context.PlayerInput.IsAiming)
        {
            SwitchState(Factory.AimEnd());
        }
        //移动
        if (Context.PlayerInput.IsMovePressed)
        {
            SwitchState(Factory.Move());
        }
        //射击
        if (Context.PlayerInput.IsAttacking)
        {
            SwitchState(Factory.Attack());
        }
    }

    public override void InitialSubState()
    {
       
    }
    
  
   
}
