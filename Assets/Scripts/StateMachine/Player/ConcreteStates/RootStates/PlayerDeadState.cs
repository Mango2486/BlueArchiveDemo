using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        //TODO: 播放死亡动画，给出死亡信号，游戏结束呼出UI
        Context.PlayerAnimator.Play("Dead");
    }

    public override void UpdateState()
    {
    }

    public override void FixedUpdateState()
    {
    }

    public override void ExitState()
    {
    }

    public override void CheckSwitchStates()
    {
    }

    public override void InitialSubState()
    {
    }
}
