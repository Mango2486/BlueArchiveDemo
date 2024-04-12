using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHeight : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;

    private void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        Debug.Log(skinnedMeshRenderer.bounds.size);
    }
}
