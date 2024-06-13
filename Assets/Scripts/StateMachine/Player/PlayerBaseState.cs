
using UnityEngine;

public abstract class PlayerBaseState : IBaseState
{
   private bool isRootState = false;
   private PlayerStateMachine context; 
   private PlayerStateFactory factory;
   private PlayerBaseState currentSuperState;
   private PlayerBaseState currentSubState;
   
   protected bool IsRootState
   {
      set { isRootState = value; }
   }

   protected PlayerStateMachine Context => context;

   protected PlayerStateFactory Factory => factory;

   protected PlayerBaseState CurrentSuperState => currentSuperState;

   protected PlayerBaseState CurrentSubState => currentSubState;
  
   
   
   //因为需要获得context和factory，所以直接在Abstract State的构造函数中获取，那么Concrete States中都会继承这个构造器。
   protected PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
   {
      context = currentContext;
      factory = playerStateFactory;
   }
   //进入状态后执行什么
   //public abstract  void EnterState();
   //该状态的Update，里面一般是逻辑相关
   public abstract void EnterState();

   public abstract  void UpdateState();
   //该状态的FixedUpdate，里面一般放物理相关
   public abstract  void FixedUpdateState();
   //退出状态后执行什么
   public abstract  void ExitState();
   //用于判断切换状态的情况
   public abstract  void CheckSwitchStates();
   
   //HSF的关键所在,只在根状态设置
   public abstract  void InitialSubState();
   
   protected void SwitchState(PlayerBaseState newState)
   {
      //切换状态的逻辑
      //先退出当前状态
      ExitState();
      //进入新状态
      newState.EnterState();
      //只有当前状态是该层状态的根状态时，才会执行状态转换。也就是对于子状态来说，不具备切换层状态的权限。
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
      //首先肯定是执行当前状态的Update
      UpdateState();
      //然后看当前状态下有无其他子状态，有则同时启动子状态的Update
      currentSubState?.UpdateState();
   }

   public void FixedUpdateStates()
   {
      FixedUpdateState();
      currentSubState?.FixedUpdateState();
   }
   
   //设置父状态
   protected void SetSuperState(PlayerBaseState newSuperState)
   { 
      currentSuperState = newSuperState;
   }

   //设置子状态
   protected void SetSubState(PlayerBaseState newSubState)
   {  
      //获得当前状态的子状态。
      currentSubState = newSubState;
      //那么理所应当当前状态就是子状态的父状态。
      newSubState.SetSuperState(this);
   }
}
