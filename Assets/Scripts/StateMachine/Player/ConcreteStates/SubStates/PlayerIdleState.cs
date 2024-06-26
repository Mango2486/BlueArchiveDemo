using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{   
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
       
       
    }
    public override void EnterState()
    {
        Context.PlayerAnimator.Play("StandIdle");
    }

    public override void UpdateState()
    {
       CheckSwitchStates();
    }

    public override void ExitState()
    {
       
    }

    public override void FixedUpdateState()
    {
        
    }

    public override void CheckSwitchStates()
    {
        if (Context.PlayerInput.IsMovePressed)
        {
            SwitchState(Factory.Move());
        }

        if (Context.PlayerInput.IsAiming)
        {
            SwitchState(Factory.AimStart());
        }
        
    }
    
    public override void InitialSubState()
    {   
        
    }

   
}
