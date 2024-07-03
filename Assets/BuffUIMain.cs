using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUIMain : MonoBehaviour
{
    private void OnEnable()
    {   
        Subscribe();
        BuffUIController.Instance.BuffView.UpdateBuffUI(BuffUIController.Instance.GenerateBuff());

    }
    private void OnDisable()
    {
        Unsubscribe();
    }
    
    private void OnClickGetBuff(object sender, BuffModel e)
    {
        //关闭窗口
        gameObject.SetActive(false);
    }
    private void Subscribe()
    {
        for (int i = 0; i < BuffUIController.Instance.BuffView.GetBuffTemplateList().Count; i++)
        {
            BuffUIController.Instance.BuffView.GetBuffTemplateList()[i].GetBuff += OnClickGetBuff;
        }
    }
    
    private void Unsubscribe()
    {
        for (int i = 0; i < BuffUIController.Instance.BuffView.GetBuffTemplateList().Count; i++)
        {
            BuffUIController.Instance.BuffView.GetBuffTemplateList()[i].GetBuff -= OnClickGetBuff;
        }
    }
}
