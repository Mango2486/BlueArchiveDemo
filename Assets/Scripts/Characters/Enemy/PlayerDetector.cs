using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public bool targetFound;
    private Transform playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<PlayerInput>(out PlayerInput playerInput))
        {
            targetFound = true;
            playerTransform = playerInput.transform;
        }
    }

    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }
}
