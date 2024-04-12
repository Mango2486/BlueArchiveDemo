using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{   
      private Vector3 currentTargetRotation;
      private Vector3 timeToReachTargetRotation;
      private Vector3 dampedTargetRotationCurrentVelocity;
      private Vector3 dampedTargetRotationPassedTime;
   
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }
    public override void EnterState()
    { 
       Debug.Log("Enter Run State");
       Initialize();
    }

    public override void UpdateState()
    {
       CheckSwitchStates();
    }

    public override void ExitState()
    {
       
    }

    public override void FixedUpdateState()
    {
        Move();
    }

    public override void CheckSwitchStates()
    {
       if (!context.PlayerInput.IsMovePressed)
       {
          SwitchState(factory.Idle());
       }
    }

    public override void InitialSubState()
    {
        
    }
    
     private void Move()
      {
         if (context.PlayerInput.MoveDirection == Vector2.zero)
         {
            return;
         }
         Vector3 inputDirection = new Vector3(context.PlayerInput.MoveDirection.x, 0, context.PlayerInput.MoveDirection.y);

         float targetRotationYAngle = Rotate(inputDirection);

         Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

         float movementSpeed = GetMovementSpeed();
         //使用AddForce而不是直接设置刚体的velocity
         //使用VelocityChange的模式可以使得AddForce不受质量和时间的影响（mass，time）
         Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
         context.PlayerRigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);

      }

      private Vector3 GetTargetRotationDirection(float targetAngle)
      {
         return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
      }

      private void Initialize()
      {
         timeToReachTargetRotation.y = 0.14f;
      }

      private float Rotate(Vector3 direction)
      {
         float directionAngle = UpdateTargetRotation(direction);
         RotateTowardsTargetRotation();
         return directionAngle;
      }
      
      private float GetDirectionAngle(Vector3 direction)
      {
         float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

         if (directionAngle < 0f)
         {
            directionAngle += 360f;
         }
         
         return directionAngle;
      }
      
      private float UpdateTargetRotation(Vector3 direction)
      {
         float directionAngle = GetDirectionAngle(direction);
         if (directionAngle != currentTargetRotation.y)
         {
            UpdateTargetRotationData(directionAngle);
         }
      
         return directionAngle;
      }
      private void UpdateTargetRotationData(float directionAngle)
      {  
         
         currentTargetRotation.y = directionAngle;
         dampedTargetRotationPassedTime.y = 0f;
      }

      private void RotateTowardsTargetRotation()
      {
         float currentYAngle = context.PlayerRigidbody.rotation.eulerAngles.y;
         if (Mathf.Abs(currentYAngle - currentTargetRotation.y) < Mathf.Epsilon)
         {
            return;
         }
         
         //这里的设置意思是每经过timeToReachTargetRotation.y - dampedTargetRotationPassedTime.y秒调用一次平滑方法。
         float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, currentTargetRotation.y, ref dampedTargetRotationCurrentVelocity.y,timeToReachTargetRotation.y - dampedTargetRotationPassedTime.y );
         dampedTargetRotationPassedTime.y += Time.fixedDeltaTime;
         Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);
         context.PlayerRigidbody.MoveRotation(targetRotation);
      }


      //获得指定的移动速度
      private float GetMovementSpeed()
      {
         return context.BaseSpeed * context.SpeedModifier;
      }
      
      //获得水平方向的移动速度
      private Vector3 GetPlayerHorizontalVelocity()
      {
         Vector3 playerHorizontalVelocity = context.PlayerRigidbody.velocity;
         playerHorizontalVelocity.y = 0f;

         return playerHorizontalVelocity;
      }
   
}
