using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("角色形象")] 
    [SerializeField] private Transform playerVisualTransform;
    public Transform PlayerVisualTransform { get; private set; }
    [SerializeField] private Animator playerAnimator;
    public Animator PlayerAnimator { get; private set; }
    [SerializeField] private Rigidbody playerRigidbody;
    public Rigidbody PlayerRigidbody { get; private set; }

    [Header("角色数据")] 
    [SerializeField] private float baseSpeed = 5f;
    public float BaseSpeed { get; private set; }
    [SerializeField] private float speedModifier = 1f;
    public float SpeedModifier { get; private set; }
      
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
        
        Initialize();
        
        //设置状态机
        states = new PlayerStateFactory(this);//使得PlayerStateFactory实例获得PlayerStateMachine引用。
        currentState = states.Idle();//设置初始状态，调用Idle()返回PlayerIdleState实例。
        currentState.EnterState();//调用PlayerIdleState下的EnterState();
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState();
    }

    private void Initialize()
    {
        PlayerInput = playerInput;
        PlayerRigidbody = playerRigidbody;
        PlayerAnimator = playerAnimator;
        BaseSpeed = baseSpeed;
        SpeedModifier = speedModifier;
    }
}
