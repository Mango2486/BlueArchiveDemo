using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveDirection => inputActions.Player.Move.ReadValue<Vector2>();
    public bool IsMovePressed => MoveDirection != Vector2.zero;

    private Vector3 mousePosition;
    public Vector3 MousePosition => mousePosition;

    public bool IsAttacking { get; set; }

    public bool IsAiming { get; set; }


    private InputActions inputActions;

    private Camera camera;

   
    private void OnEnable()
    {
        EnableInput();
    }

    private void Awake()
    {
        inputActions = new InputActions();
        AddListener();
    }
#if  UNITY_EDITOR
    private void Update()
    {
        //先从摄像机发出射线
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
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
            Debug.DrawLine(ray.origin,point,Color.red);
        }
    }
#endif

    private void OnDisable()
    {
        DisableInput();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }

    #region 输入事件管理

    private void AddListener()
    {
        inputActions.Player.Aim.performed += OnAimPerformed;
        inputActions.Player.Aim.canceled += OnAimCanceled;
        inputActions.Player.Attack.performed += OnAttackPerformed;
        inputActions.Player.Attack.canceled += OnAttackCanceled;
    }

    private void RemoveListener()
    {
        inputActions.Player.Aim.performed -= OnAimPerformed;
        inputActions.Player.Aim.canceled -= OnAimCanceled;
        inputActions.Player.Attack.performed -= OnAttackPerformed;
        inputActions.Player.Attack.canceled -= OnAttackCanceled;
    }
    private void OnAttackCanceled(InputAction.CallbackContext obj)
    {
        IsAttacking = false;
    }

    private void OnAttackPerformed(InputAction.CallbackContext obj)
    {
        IsAttacking = true;
    }

    private void OnAimCanceled(InputAction.CallbackContext obj)
    {
        IsAiming = false;
    }

    private void OnAimPerformed(InputAction.CallbackContext obj)
    {   
        //获得当前鼠标落点位置
        SetMousePosition();
        IsAiming = true;
    }

    #endregion

    #region 动作表管理

    public void EnableInput()
    {
        inputActions.Enable();
    }
    
    public void DisableInput()
    {
        inputActions.Disable();
    }
    
    public void EnablePlayerInput()
    {   
        DisableInput();
        inputActions.Player.Enable();
    }

    public void DisablePlayerInput()
    {
        inputActions.Player.Disable();
    }


    #endregion
    
    //获得鼠标所在位置的点的坐标
    private Vector3 GetMousePointPosition()
    {
        //先从摄像机发出射线
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
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
    private void SetMousePosition()
    {
        mousePosition = GetMousePointPosition();
    }
    public void SetCamera(Camera camera)
    {
        this.camera = camera;
    }

}
