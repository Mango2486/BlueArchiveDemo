using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    [Header("子弹生命周期")]
    [SerializeField] private float activeTime;
    [Header("子弹移动速度")]
    [SerializeField] private float moveSpeed;

    private RaycastHit raycastHit;
    [SerializeField] private float rayDistance = 10f; 
    
    private Ray checkRay;
    private Vector3 startPosition;
    
    private TrailRenderer trailRenderer;
    private Vector3 moveDirection;
    private float currentTime;

    private void OnEnable()
    {
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
            //别忘记重置计时器
            ResetLifeTime();
        }
    }
    
    public void SetMoveDirection(Vector3 targetDirection)
    {
        moveDirection = targetDirection.normalized;
        checkRay = new Ray(startPosition, moveDirection);
    }
    #endregion

    #region 子弹碰撞

    private void HitCheck()
    {     
        
        rayDistance = (transform.position - startPosition).magnitude;
        //暂时不检测Layer
        if (Physics.Raycast(checkRay , out raycastHit, rayDistance))
        {
            if (raycastHit.collider.enabled)
            {
                Debug.Log("Hit Enemy!");
                HitSomething();
            }
            
        }
    }
    private void HitSomething()
    {
        //返回对象池，同时重置生命周期
        BackToPool();
        ResetLifeTime();
    }

    #endregion
   
    
    # region Debug
    private void RayTest()
    {
        Debug.DrawRay(transform.position, moveDirection, Color.red, rayDistance );
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
