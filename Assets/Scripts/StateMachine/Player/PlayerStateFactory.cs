using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;


public enum PlayerState
{
    Idle,
    Move,
    MoveEnd,
    AimStart,
    Aim,
    AimEnd,
    Attack,
    Reload,
    Normal,
    Hurt,
    Dead,
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
        states[PlayerState.Normal] = new PlayerNormalState(context, this);
        states[PlayerState.Idle] = new PlayerIdleState(context,this);
        states[PlayerState.Move] =  new PlayerMoveState(context,this);
        states[PlayerState.MoveEnd] = new PlayerMoveEndState(context, this);
        states[PlayerState.AimStart] = new PlayerAimStartState(context, this);
        states[PlayerState.Aim] = new PlayerAimState(context, this);
        states[PlayerState.AimEnd] = new PlayerAimEndState(context, this);
        states[PlayerState.Attack] = new PlayerAttackState(context, this);
        states[PlayerState.Reload] = new PlayerReloadState(context, this);
        states[PlayerState.Hurt] = new PlayerHurtState(context, this);
        states[PlayerState.Dead] = new PlayerDeadState(context, this);
    }

    #region Create Concrete States

    public PlayerBaseState Normal()
    {
        return states[PlayerState.Normal];
    }
    
    public PlayerBaseState Idle()
    {
        return states[PlayerState.Idle];
    }

    public PlayerBaseState Move()
    {
        return states[PlayerState.Move];
    }
    
    public PlayerBaseState MoveEnd()
    {
        return states[PlayerState.MoveEnd];
    }

    public PlayerBaseState AimStart()
    {
        return states[PlayerState.AimStart];
    }

    public PlayerBaseState Aim()
    {
        return states[PlayerState.Aim];
    }

    public PlayerBaseState AimEnd()
    {
        return states[PlayerState.AimEnd];
    }

    public PlayerBaseState Attack()
    {
        return states[PlayerState.Attack];
    }

    public PlayerBaseState Reload()
    {
        return states[PlayerState.Reload];
    }

    public PlayerBaseState Hurt()
    {
        return states[PlayerState.Hurt];
    }

    public PlayerBaseState Dead()
    {
        return states[PlayerState.Dead];
    }
    
    #endregion
}
