using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStandAimState : PlayerBaseState
{
    public playerStandAimState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }
    
    public override void EnterState()
    {
        Context.PlayerAnimator.Play("StandAim");
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
        
    }

    public override void CheckSwitchStates()
    {
        //松开瞄准
        if (!Context.PlayerInput.IsAiming)
        {
            SwitchState(Factory.StandAimEnd());
        }
        //移动
        if (Context.PlayerInput.IsMovePressed)
        {
            SwitchState(Factory.StandMove());
        }
        //射击
        if (Context.PlayerInput.IsAttacking)
        {
            SwitchState(Factory.StandAttack());
        }
    }

    public override void InitialSubState()
    {
       
    }
    
   
}
