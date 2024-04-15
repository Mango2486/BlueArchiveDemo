using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{   
    //用于获取当前状态机环境
    private PlayerStateMachine context;
    
    //构造器中获取当前状态机环境的引用
    public PlayerStateFactory (PlayerStateMachine currentContext)
    {
        context = currentContext;
    }

    #region Create Concrete States

    public PlayerBaseState Stand()
    {
        return new PlayerStandState(context, this);
    }
    
    public PlayerBaseState StandIdle()
    {
        return new PlayerIdleState(context,this);
    }

    public PlayerBaseState StandMove()
    {
        return new PlayerStandMoveState(context,this);
    }
    
    public PlayerBaseState StandMoveEnd()
    {
        return new PlayerStandMoveEndState(context, this);
    }
    
    #endregion
}
