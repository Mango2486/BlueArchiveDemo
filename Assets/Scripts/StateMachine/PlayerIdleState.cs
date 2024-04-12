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
        Debug.Log("Idle State");
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
        if (context.PlayerInput.IsMovePressed)
        {
            SwitchState(factory.Run());
        }
    }

    public override void InitialSubState()
    {
        
    }

   
}
