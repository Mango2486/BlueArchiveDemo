using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandIdleState : PlayerBaseState
{   
    public PlayerStandIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
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
            SwitchState(Factory.StandMove());
        }

        if (Context.PlayerInput.IsAiming)
        {
            SwitchState(Factory.StandAimStart());
        }
        
    }
    
    public override void InitialSubState()
    {   
        
    }

   
}
