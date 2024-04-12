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

    public PlayerBaseState Movement()
    {
        return new PlayerMovementState(context, this);
    }
    
    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(context,this);
    }

    public PlayerBaseState Run()
    {
        return new PlayerRunState(context,this);
    }
    
    #endregion
}
