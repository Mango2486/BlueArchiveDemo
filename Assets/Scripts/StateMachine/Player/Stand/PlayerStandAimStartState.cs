using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStandAimStartState : PlayerBaseState
{
    public PlayerStandAimStartState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    private bool isAnimationEnd = false;
    public override void EnterState()
    {       
        //TODO:按下瞄准让人物朝向鼠标所在方向，同时子弹也朝向该方向发射。
        ReSetVelocity();
        Context.RotateToAimPoint();
        Context.PlayerAnimator.Play("StandAimStart");
    }

    public override void UpdateState()
    {
        AnimationEnd();
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
        //瞄准完成，进入瞄准状态
        if (isAnimationEnd)
        {
            if (Context.PlayerInput.IsAiming)
            {
                SwitchState(Factory.StandAim());
            }
            else
            {
                SwitchState(Factory.StandAimEnd());
            }
        }
        //移动放弃瞄准
        if (Context.PlayerInput.IsMovePressed)
        {
            SwitchState(Factory.StandMove());
        }
    }

    public override void InitialSubState()
    {
       
    }
    
    private void AnimationEnd()
    {
        AnimatorStateInfo animatorStateInfo;
        animatorStateInfo = Context.PlayerAnimator.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.normalizedTime >= 1 && animatorStateInfo.IsName("StandAimStart"))
        {
            isAnimationEnd = true;
        }
        else
        {
            isAnimationEnd = false;
        }
    }

    private void ReSetVelocity()
    {
        Context.PlayerRigidbody.velocity = Vector3.zero;
    }

    #region 三个状态使用的瞄准方法，移动到PlayerStateMachine中避免重复代码
    /*
    private void RotateToAimPoint()
    {   
        
        Vector3 targetPoint = new Vector3(GetMousePointPosition().x, Context.PlayerTransform.position.y,
            GetMousePointPosition().z);
        Context.PlayerTransform.LookAt(targetPoint);
        //尝试：因为瞄准的关系，所以鼠标坐标应该跟枪口是正对的，但是转向是整体，所以人物朝向不应该朝向鼠标点，而是鼠标点（即枪口坐标）-枪口本地坐标，得到人物朝向目标点。
        //解决：是模型问题，本身枪和人身就有一定偏差，固定枪口位置不动，移动人物模型做到匹配即可。
        Vector3 aimPoint = new Vector3(targetPoint.x, Context.GetMuzzleTransform().position.y, targetPoint.z);
        Vector3 targetDirection = aimPoint - Context.GetMuzzleTransform().position;
        Context.SetAimDirection(targetDirection);
    }
    
    private Vector3 GetMousePointPosition()
    {
        //先从摄像机发出射线
        Ray ray = Context.MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        //创建一个用于接收射线的平面
        //new Plane(法线向量，一个点)
        //这里以（0，1，0）为法线向量，（0，0，0）为点，创建一个与Y轴垂直的平面。
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        //用于获得射线与平面相交后这段射线的长度
        float rayDistance;
        //使射线与平面相交
        Vector3 point = Vector3.zero;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            point = ray.GetPoint(rayDistance);
        }
        return point;
    }
    */
    #endregion
   
}
