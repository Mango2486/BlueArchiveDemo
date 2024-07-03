using System;
using System.Collections;
using System.Collections.Generic;
using MVCTest.Player;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class BuffTemplate : MonoBehaviour,IPointerClickHandler
{
    public Image buffImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI valueText;

    private BuffModel buffModel;

    public event EventHandler<BuffModel> GetBuff;

    public void SetBuffModel(BuffModel newBuffModel)
    {
        buffModel = newBuffModel;
    }

    public void OnPointerClick(PointerEventData eventData)
    {   
       /*
        foreach (var kv in buffModel.DataDictionary)
        {
            if (kv.Value != 0)
            {
                PlayerModel.Instance.PropDictionary[kv.Key] += kv.Value;
            }
        }
        */
       GetBuff?.Invoke(this, buffModel);
    }
}
