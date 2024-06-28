using System;
using System.Collections.Generic;
using UnityEngine;

//对象池数据类
[Serializable]
public class ObjectPool 
{
    //通过队列来存储
    [SerializeField]private Queue<GameObject> objectQueue = new Queue<GameObject>();
    //需要生成的GameObject
    [Header("存储的对象")]
    [SerializeField]private GameObject targetObject;
    public GameObject TargetObject => targetObject;
    //对象池的最大数量
    [Header("对象池最大容量")]
    [SerializeField]private int maxCapacity;
    //对象池初始化时生成数量
    private int initCapacity;
    private Transform parentTransform;

    public ObjectPoolName poolName;

    //对象池的功能
    //首先是初始化对象池
    public void Initialize(Transform poolParent)
    {   
        ClearObjectPool();
        parentTransform = poolParent;
        //设置初始容量
        initCapacity = maxCapacity / 2;
        for (int i = 1; i <= initCapacity; i++)
        {
            objectQueue.Enqueue(CopyObject());
        }
    }

    #region 第一套逻辑:严格控制最大数量，并且所有对象都实时存储在Queue里面
    private GameObject AvailableObject()
    {
        GameObject availableObject = null;
        if (objectQueue.Count > 0 && !objectQueue.Peek().activeSelf)
        { 
            availableObject = objectQueue.Dequeue();
        }
        else if(objectQueue.Count < maxCapacity)
        {
            availableObject = CopyObject();
        }
        //出队后直接入队，否则对象池队列中无法知道当前共生成多少个对象，实现不了实时总量控制
        //如果成功取用了，那么就直接返回队列，空物体不入列
        if (availableObject != null)
        {
            objectQueue.Enqueue(availableObject);
        }
        return availableObject;

    }
    
    public void ReturnObject(GameObject gameObject)
    {
        //因为对象启用后直接入列，所以此时队列中一直拥有所有的对象，那么只需要将对象失活即可
        //不会有多于maxCapacity的对象产生，所以不需要手动去调用Destroy
        gameObject.SetActive(false);
    }
    

    #endregion

    #region 第二套逻辑:只控制最终对象池的最大容量，过程中不管生成了多少对象，同时队列中存储的也不是生成的所有对象

    //尝试从对象池中取出可用对象
    /*
    private GameObject AvailableObject()
    {
        //只要队列不为空并且队首物体不是激活状态就可以拿
        GameObject availableObject = null; 
        if (objectQueue.Count > 0 && !objectQueue.Peek().activeSelf)
        { 
            availableObject = objectQueue.Dequeue();
        }
        //如果队列中所有对象已经用完，且没有超过对象池最大容量，则新建对象返回
        else
        {   
            availableObject = CopyObject();
        }
        //如果已经用完并且超过对象池最大容量，则返回空物体
        return availableObject;
    }
    */
    //将对象返回对象池
    /*
    public void ReturnObject(GameObject gameObject)
    {
        //如果队列容量仍旧可以容纳，并且当前对象不在队列中，那么直接返回对象池队列
        if (objectQueue.Count < maxCapacity)
        {
            if (!objectQueue.Contains(gameObject))
            {   
                gameObject.SetActive(false);
                objectQueue.Enqueue(gameObject);
            }
        }
        //队列对象池无法容纳，那么将对象销毁
        else
        {
            GameObject.Destroy(gameObject);
        }
    }
    */

    #endregion

    //取出准备好的可用对象
    public GameObject GetPreparedObject()
    {
        GameObject preparedObject = AvailableObject();
        if (preparedObject != null)
        {
            preparedObject.SetActive(true);
        }
        return preparedObject;
    }
    

    private GameObject CopyObject()
    {
        GameObject newObject = GameObject.Instantiate(targetObject,parentTransform);
        newObject.SetActive(false);
        return newObject;
    }

    public void ClearObjectPool()
    {
        if (objectQueue.Count != 0)
        {
            objectQueue.Clear();
        }
    }


    public int GetCurrentCapacity()
    {
        return objectQueue.Count;
    }

    
}
