using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
   
    
    [SerializeField] private ObjectPool[] objectPools;
    private Dictionary<ObjectPoolName, ObjectPool> objectPoolsDictionary = new Dictionary<ObjectPoolName, ObjectPool>();

    protected override void InitAwake()
    {
       Initialize();
    }


    //完成对象池的初始化
    private void Initialize()
    {
        for (int i = 0; i < objectPools.Length; i++)
        {   
            objectPoolsDictionary.Add(objectPools[i].poolName,objectPools[i]);
            //这一段是为了对象池生成的对象在Unity面板中便于管理。
            Transform poolParent = new GameObject("Pool: " + objectPools[i].TargetObject.name).transform;
            poolParent.SetParent(transform);
            objectPools[i].Initialize(poolParent);
        }
    }
    
    //从对象池中释放对象
    public void  Release(ObjectPoolName objectPoolNameEnum)
    {
        objectPoolsDictionary[objectPoolNameEnum].GetPreparedObject();
    }
    //指定Transform释放
    public GameObject Release(ObjectPoolName objectPoolName, Transform targetTransform)
    {   
        GameObject obejct =  objectPoolsDictionary[objectPoolName].GetPreparedObject();
        obejct.transform.position = targetTransform.position;
        return obejct;
    }
    public GameObject Release(ObjectPoolName objectPoolName, Transform targetTransform, float atkData)
    {   
        GameObject obejct =  objectPoolsDictionary[objectPoolName].GetPreparedObject();
        if (obejct != null)
        {
            obejct.transform.position = targetTransform.position;
            obejct.GetComponent<Bullet>().SetAtk(atkData);
        }
        return obejct;
    }
    //指定Position重载
    public GameObject Release(ObjectPoolName objectPoolName, Vector3 position)
    {
        GameObject obejct =  objectPoolsDictionary[objectPoolName].GetPreparedObject();
        obejct.transform.position = position;
        return obejct;
    }
    //将对象返回池中
    public void BackToPool(ObjectPoolName objectPoolNameEnum, GameObject poolObject)
    {
        objectPoolsDictionary[objectPoolNameEnum].ReturnObject(poolObject);
    }
}

public enum ObjectPoolName
{
   Bullet = 0,
   Sweeper = 1,
}