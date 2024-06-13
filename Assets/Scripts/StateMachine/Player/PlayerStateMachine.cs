using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    private Transform playerTransform;
    public Transform PlayerTransform { get; private set; }
    [SerializeField] private Animator playerAnimator;
    public Animator PlayerAnimator { get; private set; }
    [SerializeField] private Rigidbody playerRigidbody;
    public Rigidbody PlayerRigidbody { get; private set; }

    [Header("角色数据")] 
    [SerializeField] private float baseSpeed = 5f;
    public float BaseSpeed { get; private set; }
    [SerializeField] private float speedModifier = 1f;
    public float SpeedModifier { get; private set; }

    [Header("枪口位置")] 
    [SerializeField] private Transform muzzleTransform;
    private Vector3 aimDirection;

    [Header("主摄像机")] 
    [SerializeField] private Camera mainCamera;
    
    public Transform MuzzleTransform => muzzleTransform;
    public Vector3 AimDirection => aimDirection;

    public Camera MainCamera => mainCamera;
 

    //状态机相关
    private PlayerBaseState currentState;
    private PlayerStateFactory states;
    
    public PlayerBaseState CurrentState
    {
        get => currentState;
        set => currentState = value;
    }
    

    private PlayerInput playerInput;
    public PlayerInput PlayerInput { get; private set; }
    
   
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
        
        Initialize();
        
        playerInput.SetCamera(mainCamera);
        
        //设置状态机
        states = new PlayerStateFactory(this);//使得PlayerStateFactory实例获得PlayerStateMachine引用。
        currentState = states.Stand();//设置初始状态，调用Idle()返回PlayerIdleState实例。
        currentState.EnterState();//调用PlayerIdleState下的EnterState();
    }

    private void Update()
    {
        currentState.UpdateStates();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateStates();
    }

    private void Initialize()
    {
        PlayerInput = playerInput;
        PlayerRigidbody = playerRigidbody;
        PlayerAnimator = playerAnimator;
        BaseSpeed = baseSpeed;
        SpeedModifier = speedModifier;
        PlayerTransform = playerTransform;
    }

    #region 射击相关

    public void RotateToAimPoint()
    {   
        /*
        Vector3 targetPoint = new Vector3(PlayerInput.MousePosition.x, PlayerTransform.position.y,
            PlayerInput.MousePosition.z);
        //targetPoint = new Vector3(targetPoint.x - MuzzleTransform.localPosition.x, targetPoint.y,
         //   targetPoint.z -  MuzzleTransform.localPosition.z);
        PlayerTransform.LookAt(targetPoint);
        */
        Vector3 targetPoint = new Vector3(GetMousePointPosition().x, PlayerTransform.position.y,
            GetMousePointPosition().z);
        PlayerTransform.LookAt(targetPoint);
        //尝试：因为瞄准的关系，所以鼠标坐标应该跟枪口是正对的，但是转向是整体，所以人物朝向不应该朝向鼠标点，而是鼠标点（即枪口坐标）-枪口本地坐标，得到人物朝向目标点。
        //解决：是模型问题，本身枪和人身就有一定偏差，固定枪口位置不动，移动人物模型做到匹配即可。
        Vector3 aimPoint = new Vector3(targetPoint.x, GetMuzzleTransform().position.y, targetPoint.z);
        Vector3 targetDirection = aimPoint - GetMuzzleTransform().position;
        SetAimDirection(targetDirection);
    }
    
    private  Vector3 GetMousePointPosition()
    {
        //先从摄像机发出射线
        Ray ray = MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
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

    #endregion

    public void SetAimDirection(Vector3 targetDirection)
    {
        aimDirection = targetDirection;
    }

    public Transform GetMuzzleTransform()
    {
        return muzzleTransform;
    }
}
