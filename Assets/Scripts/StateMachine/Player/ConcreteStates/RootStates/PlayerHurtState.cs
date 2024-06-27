using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerBaseState
{
    public PlayerHurtState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {   
        //设置根状态
        InitialSubState();
        //受伤反应
        Context.GetHurt(Context.HurtDamage);
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
        if (Context.PlayerModel.CurrentHp != 0 && !Context.Hurt)
        {
            SwitchState(Factory.Normal());
        }

        if (Context.PlayerModel.CurrentHp == 0 && Context.Hurt)
        {
            SwitchState(Factory.Dead());
        }
    }

    public override void InitialSubState()
    {   
        //设置好初始子状态即可，会根据当前按键进入对应的子状态
        SetSubState(Context.PlayerInput.IsMovePressed ? Factory.Move() : Factory.Idle());
    }
    
  
}
