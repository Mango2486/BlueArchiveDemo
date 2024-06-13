using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    private RectTransform rectTransform;
    public Camera mainCamera;
    
    
    private Ray[] cornerRay = new Ray[4];
    private Vector3[] point = new Vector3[4];
    protected override void InitAwake()
    {
        rectTransform = GetComponent<RectTransform>();

    }

    private void Update()
    {
        GetScreenCornerPosInWorldSpace();
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
    
    //获取敌人生成区域的四角
    public Vector3[] GetSpawnCornerPos()
    {
        return point;
    }
}
