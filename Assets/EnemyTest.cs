using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour
{
    public NavMeshAgent nav;

    public Transform target;

    private PlayerDetector playerDetector;
    private void Awake()
    {
        playerDetector = GetComponentInChildren<PlayerDetector>();
    }

    private void Update()
    {
        if (playerDetector.targetFound)
        {
            nav.SetDestination(target.position);
        }
    }
}
