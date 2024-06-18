using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NormalEnemyBaseState : IBaseState
{

    private bool isRootState = false;

    private NormalEnemyStateMachine context;
    private NormalEnemyStateFactory factory;
    private NormalEnemyBaseState currentSuperState;
    private NormalEnemyBaseState currentSubState;

    protected bool IsRootState
    {
        set => isRootState = value;
    }

    protected NormalEnemyStateMachine Context => context;

    protected NormalEnemyStateFactory Factory => factory;

    protected NormalEnemyBaseState CurrentSuperState => currentSuperState;

    protected NormalEnemyBaseState CurrentSubState => currentSubState;

    protected NormalEnemyBaseState(NormalEnemyStateMachine currentContext, NormalEnemyStateFactory currentFactory)
    {
        context = currentContext;
        factory = currentFactory;
    }
    
    
    public abstract void EnterState();
    
    public abstract void UpdateState();
    
    public abstract void FixedUpdateState();

    public abstract void ExitState();
    
    public abstract void CheckSwitchStates();
    
    public abstract void InitialSubState();

    protected void SwitchState(NormalEnemyBaseState newState)
    {
        ExitState();
        newState.EnterState();
        if (isRootState)
        {
            context.CurrentState = newState;
        }
        else if (currentSuperState != null)
        {
            currentSuperState.SetSubState(newState);
        }
    }
    
    
    public void UpdateStates()
    {
        UpdateState();
        currentSubState?.UpdateState();
    }

    public void FixedUpdateStates()
    {
        FixedUpdateState();
        currentSubState?.FixedUpdateState();
    }

    protected void SetSuperState(NormalEnemyBaseState newSuperState)
    {
        currentSuperState = newSuperState;
    }

    protected void SetSubState(NormalEnemyBaseState newSubState)
    {
        currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

    protected void ClearSubState()
    {
        currentSubState = null;
    }
}
