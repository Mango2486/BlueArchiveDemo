using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public Camera mainCamera;
    [SerializeField] private float spawnTimer;
    private float timer;
    private Ray[] cornerRay = new Ray[4];
    private Vector3[] point = new Vector3[4];

    private void Update()
    {   
        GetScreenCornerPosInWorldSpace();
        timer += Time.deltaTime;
        if (timer > spawnTimer)
        {
            ObjectPoolManager.Instance.Release(ObjectPoolName.Sweeper, GetSpawnPosition());
            timer = 0;
        }
    }
  

    private void InitializeCornerRay()
    {
        cornerRay[0] = mainCamera.ScreenPointToRay(new Vector3(0, 0, 0));
        cornerRay[1] = mainCamera.ScreenPointToRay(new Vector3(0, Screen.height, 0));
        cornerRay[2] = mainCamera.ScreenPointToRay(new Vector3(Screen.width, 0, 0));
        cornerRay[3] = mainCamera.ScreenPointToRay(new Vector3(Screen.width, Screen.height, 0));
    }

    private void GetScreenCornerPosInWorldSpace()
    {   
        InitializeCornerRay();
        //这里以（0，1，0）为法线向量，（0，0，0）为点，创建一个与Y轴垂直的平面。
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        //用于获得射线与平面相交后这段射线的长度
        float rayDistance;
        //使射线与平面相交
        for (int i = 0; i < 4; i++)
        {
            if (groundPlane.Raycast(cornerRay[i], out rayDistance))
            {
                point[i] = cornerRay[i].GetPoint(rayDistance);
                if (Mathf.Abs(point[i].x) > 25)
                {
                    if (point[i].x > 0)
                    {
                        point[i] = new Vector3(25, point[i].y, point[i].z);
                    }
                    else
                    {
                        point[i] = new Vector3(-25, point[i].y, point[i].z);
                    } 
                }

                if (Mathf.Abs(point[i].z) > 25)
                {
                    if (point[i].z > 0)
                    {
                        point[i] = new Vector3(point[i].x, point[i].y, 25);
                    }
                    else
                    {
                        point[i] = new Vector3(point[i].x, point[i].y, -25);
                    } 
                }
                Debug.DrawLine(cornerRay[i].origin,point[i],Color.red);
            }
        }
    }
    
    //获取敌人可生成区域中的随机一点
    private Vector3 GetSpawnPosition()
    {
        //point[0]屏幕左下点，point[3]屏幕右上点
        float x = Random.Range(point[0].x, point[3].x);
        float z = Random.Range(point[0].z, point[3].z);
        Vector3 position = new Vector3(x, 0, z);
        return  position;
    }
}
