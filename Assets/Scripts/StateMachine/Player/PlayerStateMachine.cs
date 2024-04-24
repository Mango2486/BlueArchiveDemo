using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
 

    //状态机相关
    private PlayerBaseState currentState;
    private PlayerStateFactory states;
    
    public PlayerBaseState CurrentState
    {
        get { return currentState;}
        set { currentState = value; }
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

    public void SetAimDirection(Vector3 targetDirection)
    {
        aimDirection = targetDirection;
    }
}
