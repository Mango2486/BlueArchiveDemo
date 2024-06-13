using System;
using System.Collections;
using System.Collections.Generic;
using MVCTest;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    [Header("子弹生命周期")]
    [SerializeField] private float activeTime;
    [Header("子弹移动速度")]
    [SerializeField] private float moveSpeed;

    private RaycastHit raycastHit;
    [SerializeField] private float rayDistance = 5f; 
    
    private Vector3 startPosition;
    
    private TrailRenderer trailRenderer;
    private Vector3 moveDirection;
    private float currentTime;
    
    //测试使用刚体   
    
    private void OnEnable()
    {   
        //重置计时器
        ResetLifeTime();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        startPosition = transform.position;
    }
    
    //逻辑相关
    private void Update()
    {
        LifeTime();
        Move();
    }
    
    //物理相关
    private void FixedUpdate()
    {
        HitCheck();

    }
    
    private void OnDisable()
    {
        trailRenderer.Clear();
    }

    #region  子弹移动

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
            BackToPool();
        }
    }
    
    public void SetMoveDirection(Vector3 targetDirection)
    {
        moveDirection = targetDirection.normalized;
    }
    #endregion

    #region 子弹碰撞

    private void HitCheck()
    {   
        //高速移动的物体进行射线检测时可以使用这种分段线的检测方法。
        Vector3 direction = transform.position - startPosition;
        float distance = direction.magnitude;
        if (Physics.Raycast(startPosition, direction , out raycastHit, distance))
        {
           if (raycastHit.collider.TryGetComponent<EnemyUIController>(out EnemyUIController enemy))
           {
               Debug.Log("Hit Enemy!");
               enemy.Hit();
               HitSomething();
           }
        } 
        startPosition = transform.position;
       RayTest();
    }
    private void HitSomething()
    {
        //返回对象池，同时重置生命周期
        BackToPool();
        
    }

    #endregion
   
    
    # region Debug
    private void RayTest()
    {
        Debug.DrawRay(transform.position, moveDirection*rayDistance, Color.red, 0.01f );
    }
    #endregion

  
    private void BackToPool()
    {
        ObjectPoolManager.Instance.BackToPool(ObjectPoolName.Bullet, gameObject);
    }
    
    private void ResetLifeTime()
    {
        currentTime = 0f;
    }



}
