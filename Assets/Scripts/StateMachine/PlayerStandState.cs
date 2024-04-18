using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandState : PlayerBaseState,IRootState
{
    public PlayerStandState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitialSubState();
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
    
    //根状态只在根状态之间互相切换，而不会在与子状态进行切换。
    public override void CheckSwitchStates()
    {
        
    }
    
    //这里只是在根状态被创建的时候，尝试设置为其设置一个子状态，不满足条件就不设置。
    public override void InitialSubState()
    {
        //设置子状态
        //MovementState用作不做射击动作时的根状态
        //那么其实就是对于一个根状态，有很多个具体状态作为根状态的子状态备选
        if (Context.PlayerInput.IsMovePressed)
        {
            SetSubState(Factory.StandMove());
        }
        else
        {
            SetSubState(Factory.StandIdle());
        }
        Debug.Log(CurrentSubState);
    }
}
