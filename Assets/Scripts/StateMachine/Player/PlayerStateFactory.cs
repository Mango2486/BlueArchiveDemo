using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;


public enum PlayerState
{
    Stand,
    StandIdle,
    StandMove,
    StandMoveEnd,
    StandAimStart,
    StandAim,
    StandAimEnd,
    StandAttack,
    StandReload,
}


public class PlayerStateFactory
{   
    //用于获取当前状态机环境
    private PlayerStateMachine context;
    //建立状态字典，以避免重复新建状态实例造成的GC
    private Dictionary<PlayerState, PlayerBaseState> states = new Dictionary<PlayerState, PlayerBaseState>();

    //构造器中获取当前状态机环境的引用
    public PlayerStateFactory (PlayerStateMachine currentContext)
    {
        context = currentContext;
        states[PlayerState.Stand] = new PlayerStandState(context, this);
        states[PlayerState.StandIdle] = new PlayerStandIdleState(context,this);
        states[PlayerState.StandMove] =  new PlayerStandMoveState(context,this);
        states[PlayerState.StandMoveEnd] = new PlayerStandMoveEndState(context, this);
        states[PlayerState.StandAimStart] = new PlayerStandAimStartState(context, this);
        states[PlayerState.StandAim] = new PlayerStandAimState(context, this);
        states[PlayerState.StandAttack] = new PlayerStandAttackState(context, this);
        states[PlayerState.StandReload] = new PlayerStandReloadState(context, this);
        states[PlayerState.StandAimEnd] = new PlayerStandAimEndState(context, this);
    }

    #region Create Concrete States

    public PlayerBaseState Stand()
    {
        return states[PlayerState.Stand];
    }
    
    public PlayerBaseState StandIdle()
    {
        return states[PlayerState.StandIdle];
    }

    public PlayerBaseState StandMove()
    {
        return states[PlayerState.StandMove];
    }
    
    public PlayerBaseState StandMoveEnd()
    {
        return states[PlayerState.StandMoveEnd];
    }

    public PlayerBaseState StandAimStart()
    {
        return states[PlayerState.StandAimStart];
    }

    public PlayerBaseState StandAim()
    {
        return states[PlayerState.StandAim];
    }

    public PlayerBaseState StandAimEnd()
    {
        return states[PlayerState.StandAimEnd];
    }

    public PlayerBaseState StandAttack()
    {
        return states[PlayerState.StandAttack];
    }

    public PlayerBaseState StandReload()
    {
        return states[PlayerState.StandReload];
    }
    
    #endregion
}
