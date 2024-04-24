using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float activeTime;

    [SerializeField] private float moveSpeed;
    private Vector3 moveDirection;
    private TrailRenderer trailRenderer;
    private float currentTime;

    private void Awake()
    {
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void Update()
    {
        LifeTime();
        Move();
    }

    private void OnDisable()
    {
        trailRenderer.Clear();
    }

    private void Move()
    {
        transform.Translate(moveDirection * Time.deltaTime * moveSpeed);
    }
    
    private void LifeTime()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= activeTime)
        {
            //回收到对象池中
            ObjectPoolManager.Instance.BackToPool(ObjectPoolName.Bullet, gameObject);
            //别忘记重置计时器
            currentTime = 0f;
        }
    }

    public void SetMoveDirection(Vector3 targetDirection)
    {
        moveDirection = targetDirection.normalized;
    }
}
