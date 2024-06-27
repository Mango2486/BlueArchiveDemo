using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using MVCTest;
using MVCTest.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{   
    [Header("主要组件")]
    private Transform playerTransform;
    public Transform PlayerTransform { get; private set; }
    [SerializeField] private Animator playerAnimator;
    public Animator PlayerAnimator { get; private set; }
    [SerializeField] private Rigidbody playerRigidbody;
    public Rigidbody PlayerRigidbody { get; private set; }

    [SerializeField] private CapsuleCollider playerCollider;
    
    [Header("角色UI及数据")]
    [SerializeField] private PlayerData playerData;
    
    [SerializeField]private PlayerView playerView;
    
    private PlayerModel playerModel;
    public PlayerModel PlayerModel => playerModel;
    public PlayerView PlayerView => playerView;

    [Header("角色材质")] 
    [SerializeField] private Material[] playerMaterials;
    
    private float alpha;
    
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

    private bool hurt;
    public bool Hurt => hurt;
    
    
    //用于接收攻击的敌人的数据
    public float HurtDamage { get; private set; }
    
    
    
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
        playerCollider = GetComponent<CapsuleCollider>();
        
        Initialize();
        
        playerInput.SetCamera(mainCamera);
        
        //设置状态机
        states = new PlayerStateFactory(this);//使得PlayerStateFactory实例获得PlayerStateMachine引用。
        currentState = states.Normal();//设置初始状态，调用Idle()返回PlayerIdleState实例。
        currentState.EnterState();//调用PlayerIdleState下的EnterState();
    }
    
    private void Start()
    {
        playerModel.Actions += OnPlayerHit;
    }

    private void Update()
    {   
        currentState.UpdateStates();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateStates();
    }
    
    private void OnDestroy()
    {
        playerModel.Actions -= OnPlayerHit;
    }

    private void Initialize()
    {   
        InitializePlayerModel();
        PlayerInput = playerInput;
        PlayerRigidbody = playerRigidbody;
        PlayerAnimator = playerAnimator;
        PlayerTransform = playerTransform;
        alpha = 1f;
        SetAlpha(alpha);
    }

    #region 射击相关

    public void RotateToAimPoint()
    {   
        
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
    
    public void SetAimDirection(Vector3 targetDirection)
    {
        aimDirection = targetDirection;
    }

    public Transform GetMuzzleTransform()
    {
        return muzzleTransform;
    }
    
    
    #endregion

    
    private void OnPlayerHit(PlayerModel playerModel)
    {
        playerView.UpdateUI(this.playerModel);
    }
    private void InitializePlayerModel()
    {
        playerModel = new PlayerModel(playerData);
        playerView.UpdateUI(playerModel);
    }
    public void GetHurt(float damage)
    {   
        //更新UI
        playerModel.GetHurt(damage);
        //看当前血量是否进入进入无敌时间
        if (playerModel.CurrentHp != 0)
        {
            StartCoroutine(InvincibleTimer());
        }
    }
    
    //受击暂时不单独作为一个状态使用
    //受击无敌协程
    private IEnumerator InvincibleTimer()
    {
        alpha = 0.5f;
        SetAlpha(alpha);
        playerCollider.isTrigger= true;
        yield return new WaitForSeconds(playerModel.InvincibleTime);
        alpha = 1f;
        SetAlpha(alpha);
        playerCollider.isTrigger = false;
        hurt = false;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        //给出碰撞信号
        if (other.gameObject.TryGetComponent(out NormalEnemyStateMachine stateMachine))
        {
            Debug.Log("发生碰撞");
        }
        
        if (other.gameObject.TryGetComponent<EnemyUIController>(out EnemyUIController enemyUIController))
        {   
            Debug.Log("发生碰撞");
            hurt = true;
            HurtDamage = enemyUIController.EnemyModel.Atk;
        }
    }
    
    //设置材质颜色值，使得角色变透明
    //受击效果1：角色半透明 目前采用
    //受击效果2：角色闪烁
    private void SetAlpha(float targetAlpha)
    {
        foreach (var render in playerMaterials)
        {
            render.SetColor("_Color", new Color(1,1,1,targetAlpha));
        }
    }
}
