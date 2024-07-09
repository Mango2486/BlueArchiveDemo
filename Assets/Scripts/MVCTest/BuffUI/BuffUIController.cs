using System;using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using Random = UnityEngine.Random;


public class BuffUIController : MonoSingleton<BuffUIController>
{
    [SerializeField] private List<BuffModel> buffLists;
    [SerializeField] private BuffView buffView;
    [SerializeField] private BuffUIMain buffUIMain;


    public BuffView BuffView => buffView;

    private BuffModel[] currentBuffModels;
    
    
    
    //获取本次抽取的Buff列表
    public BuffModel[] GenerateBuff()
    {
        BuffModel[] buffModels = new BuffModel[3];
        //随机抽取一个种类的Buff，并抽取三个等级中的一种Buff
        bool[] flags = new bool[buffLists.Count];
        int randomIndex = Random.Range(0, buffLists.Count);
        for (int i = 0; i < 3; i++)
        {   
            //剔除本次已经抽取的Buff
            while (flags[randomIndex])
            { 
                randomIndex = Random.Range(0, buffLists.Count);
            }
            flags[randomIndex] = true;
            buffModels[i] = buffLists[randomIndex];
        }
        currentBuffModels = buffModels;
        return currentBuffModels;
    }

    public void ShowBuffUI()
    {
        buffUIMain.gameObject.SetActive(true);
    }

    public void HideBuffUI()
    {
        buffUIMain.gameObject.SetActive(false);
    }
        
    
    

}
