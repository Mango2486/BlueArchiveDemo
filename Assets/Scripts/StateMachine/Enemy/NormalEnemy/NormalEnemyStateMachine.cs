using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalEnemyStateMachine : MonoBehaviour
{

    [SerializeField] private Transform target;
    private NavMeshAgent navMeshAgent;

    [SerializeField] private Animator animator;
    public Animator EnemyAnimator => animator;

    private Transform enemyTransform;
    public Transform EnemyTransform => enemyTransform;

    private PlayerDetector detector;
    
    private NormalEnemyBaseState currentState;
    private NormalEnemyStateFactory states;

    public NormalEnemyBaseState CurrentState
    {
        get => currentState;
        set => currentState = value;
    }

    public Transform Target => target;

    public NavMeshAgent NavMeshAgent => navMeshAgent;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyTransform = GetComponent<Transform>();
        detector = GetComponentInChildren<PlayerDetector>();
        
        InitialStateMachine();
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
