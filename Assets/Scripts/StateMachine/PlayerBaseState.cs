
public abstract class PlayerBaseState
{

   protected PlayerStateMachine context;
   protected PlayerStateFactory factory;
   protected PlayerBaseState currentSuperState;
   protected PlayerBaseState currentSubState;
   

   public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
   {
      context = currentContext;
      factory = playerStateFactory;
   }
   
   void Start(){}
   
   void Update(){}

   public abstract void EnterState();

   public abstract void UpdateState();

   public abstract void ExitState();

   public abstract void FixedUpdateState();

   protected void SwitchState(PlayerBaseState newState)
   {
      //切换状态的逻辑
      //先退出当前状态
      ExitState();
      //进入新状态
      newState.EnterState();
      //切换当前状态到新状态
      context.CurrentState = newState;
   }
   public abstract void CheckSwitchStates();
   
   //HSF的关键所在
   public abstract void InitialSubState();
   
   protected void UpdateStates(){}

   protected void SetSuperState(PlayerBaseState newSuperState)
   {
      currentSuperState = newSuperState;
   }


   protected void SetSubState(PlayerBaseState newSubState)
   {  
      //获得当前状态的子状态。
      currentSubState = newSubState;
      //那么理所应当当前状态就是子状态的父状态。
      newSubState.SetSuperState(this);
   }
}
