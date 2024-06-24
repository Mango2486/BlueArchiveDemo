using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public enum NormalEnemyState
{
   TargetFound,
   Trace,
   TargetNotFound,
   Patrol,
   Die,
   Attack,
   
}

public class NormalEnemyStateFactory
{
   private NormalEnemyStateMachine context;

   private Dictionary<NormalEnemyState, NormalEnemyBaseState> states =
      new Dictionary<NormalEnemyState, NormalEnemyBaseState>();

   public NormalEnemyStateFactory(NormalEnemyStateMachine currentContext)
   {
      context = currentContext;
      states[NormalEnemyState.Patrol] = new NormalEnemyPatrolState(context, this);
      states[NormalEnemyState.Trace] = new NormalEnemyTraceState(context, this);
      states[NormalEnemyState.TargetFound] = new NormalEnemyTargetFoundState(context, this);
      states[NormalEnemyState.TargetNotFound] = new NormalEnemyTargetNotFoundState(context, this);
      states[NormalEnemyState.Die] = new NormalEnemyDieState(context, this);
      states[NormalEnemyState.Attack] = new NromalEnemyAttackState(context, this);
   }

   #region Create Concrete States

   public NormalEnemyBaseState TargetFound()
   {
      return states[NormalEnemyState.TargetFound];
   }

   public NormalEnemyBaseState TargetNotFound()
   {
      return states[NormalEnemyState.TargetNotFound];
   }

   public NormalEnemyBaseState Trace()
   {
      return states[NormalEnemyState.Trace];
   }

   public NormalEnemyBaseState Patrol()
   {
      return states[NormalEnemyState.Patrol];
   }

   public NormalEnemyBaseState Die()
   {
      return states[NormalEnemyState.Die];
   }

   public NormalEnemyBaseState Attack()
   {
      return states[NormalEnemyState.Attack];
   }
#endregion
}
