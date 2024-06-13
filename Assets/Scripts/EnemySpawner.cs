using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnTimer;
    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTimer)
        {
            //ObjectPoolManager.Instance.Release(ObjectPoolName.Sweeper)
        }
    }
}
