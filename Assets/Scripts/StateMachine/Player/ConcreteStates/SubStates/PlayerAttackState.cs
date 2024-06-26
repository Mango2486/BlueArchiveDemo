using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }
    private float currentInterval;
    public override void EnterState()
    {
        currentInterval = Context.PlayerModel.ShootInterval;
    }

    public override void UpdateState()
    {   
        Context.RotateToAimPoint();
        WaitShootInterval();
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
       
    }

    public override void ExitState()
    {
       
    }

    public override void CheckSwitchStates()
    {
       //停止射击但是还是瞄准状态
       if (Context.PlayerInput.IsAiming && !Context.PlayerInput.IsAttacking)
       {
           SwitchState(Factory.Aim());
       }
       //停止射击并且停止瞄准
       if (!Context.PlayerInput.IsAttacking && !Context.PlayerInput.IsAiming)
       {
           SwitchState(Factory.AimEnd());
       }
       //移动
       if (Context.PlayerInput.IsMovePressed)
       {
           SwitchState(Factory.Move());
       }
       
    }

    public override void InitialSubState()
    {
        
    }

    private void Shoot()
    {
        //先只播放动画
        Context.PlayerAnimator.Play("StandAttack",0,0);
        //射击子弹
        GameObject bullet = ObjectPoolManager.Instance.Release(ObjectPoolName.Bullet, Context.MuzzleTransform, Context.PlayerModel.Atk);
        bullet.GetComponent<Bullet>().SetMoveDirection(Context.AimDirection);

    }

    private void WaitShootInterval()
    {
        currentInterval += Time.deltaTime;
        if (currentInterval > Context.PlayerModel.ShootInterval)
        {
            Shoot();
            currentInterval = 0;
        }
    }
}
