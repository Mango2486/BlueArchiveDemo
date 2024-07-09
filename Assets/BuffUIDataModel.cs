using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class BuffUIDataModel
{
    [SerializeField] private Sprite[] propertyImages = new Sprite[5];

    private Dictionary<string, float> dataDictionary = new Dictionary<string, float>();

    public Dictionary<string, float> DataDictionary => dataDictionary;
    public Sprite[] PropertyImage => propertyImages;

    public void SetDataDictionary(Dictionary<string, float> dictionary)
    {
        dataDictionary = dictionary;
    }
}
