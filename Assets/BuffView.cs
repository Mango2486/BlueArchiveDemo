using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffView : MonoBehaviour
{
    [Serializable]
    private class DataTemplate
    {
        public Image dataImage;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI valueText;
    }

    [SerializeField] 
    private List<BuffTemplate> buffTemplateList;

    [SerializeField] 
    private List<DataTemplate> dataTemplateList;

    
    //显示当前抽取的Buff的相关信息
    public void UpdateBuffUI(BuffModel[] buffModels)
    {
        for (int i = 0; i < buffTemplateList.Count; i++)
        {
            //更新显示
            //先进行Buff等级抽取
            buffModels[i].GetBuffData();
            if (buffModels[i].BuffImage != null)
            {
                buffTemplateList[i].buffImage = buffModels[i].BuffImage;
            }
            buffTemplateList[i].nameText.text = buffModels[i].BuffName;
            //显示Buff拥有属性极其数据
            buffTemplateList[i].valueText.text = "";
            foreach (var kv in buffModels[i].DataDictionary)
            {
                if (kv.Value != 0)
                {
                    buffTemplateList[i].valueText.text += $"{kv.Key}: {kv.Value}\n";
                }
            }
            //将对应的BuffModel传给对应的BuffTemplate
            buffTemplateList[i].SetBuffModel(buffModels[i]);
        }
    }
    //显示本次抽取Buff前角色的数据信息
    public void UpdateDataUI(BuffModel[] buffModels)
    {
        
    }

    public List<BuffTemplate> GetBuffTemplateList()
    {
        return buffTemplateList;
    }
}
