using System;
using System.Collections;
using System.Collections.Generic;
using MVCTest;
using UnityEngine;
using UnityEngine.AI;

public class NormalEnemyStateMachine : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;
    private NavMeshAgent navMeshAgent;
    public Animator EnemyAnimator => animator;

    private Transform enemyTransform;
    public Transform EnemyTransform => enemyTransform;

    public PlayerDetector detector;
    
    private NormalEnemyBaseState currentState;
    private NormalEnemyStateFactory states;

    public NormalEnemyBaseState CurrentState
    {
        get => currentState;
        set => currentState = value;
    }

    public Transform Target => target;

    public NavMeshAgent NavMeshAgent => navMeshAgent;

    public EnemyUIController enemyUIController;

    [Header("掉落物")] 
    [SerializeField] private GameObject expBall;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyTransform = GetComponent<Transform>();
        detector = GetComponentInChildren<PlayerDetector>();
        enemyUIController = GetComponent<EnemyUIController>();

    }
    
    //因为是对象池取用，所以需要将初始化方法放在OnEnable中
    private void OnEnable()
    {
        InitialStateMachine();
        navMeshAgent.speed = moveSpeed;
    }

    private void Update()
    {
        currentState.UpdateStates();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateStates();
    }

    private void InitialStateMachine()
    {
        states = new NormalEnemyStateFactory(this);
        currentState = states.TargetNotFound();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public bool TargetFound()
    {
        return detector.targetFound;
    }
}
