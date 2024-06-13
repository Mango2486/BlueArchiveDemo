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
    //尝试从对象池中取出可用对象
    private GameObject AvailableObject()
    {
        //只要队列不为空并且队首物体不是激活状态就可以拿
        GameObject availableObject = null;
        if (objectQueue.Count > 0 && !objectQueue.Peek().activeSelf)
        {
            availableObject = objectQueue.Dequeue();
        }
        //如果队列中所有对象已经用完，则新建对象返回
        else
        {
            availableObject = CopyObject();
        }
        return availableObject;
    }
    
    //取出准备好的可用对象
    public GameObject GetPreparedObject()
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        return preparedObject;
    }
    
    //将对象返回对象池
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
