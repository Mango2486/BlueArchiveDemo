using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{
    private PlayerStateMachine context;

    public PlayerStateFactory (PlayerStateMachine currentContext)
    {
        context = currentContext;
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(context,this);
    }

    public PlayerBaseState Run()
    {
        return new PlayerRunState(context,this);
    }
}
