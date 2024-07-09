using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
public class BuffModel
{
   
   // [SerializeField]private int buffId;
    [SerializeField]private string buffName;
    [SerializeField]private Sprite buffImage;
    [SerializeField]private BuffData[] buffDatas = new BuffData[3];
    
    private Dictionary<string, float> dataDictionary = new Dictionary<string, float>();

    public float MaxHp { get; private set; }
    public float Armor { get; private set; }
    public float Speed { get; private set; }
    public float Atk { get; private set; }
    
    public Dictionary<string, float> DataDictionary { get; private set; }
    public Sprite BuffImage => buffImage;
    public string BuffName => buffName;

    public bool initialed;
    
    private void InitialProp(BuffData currentData)
    {
        MaxHp = currentData.maxHp;
        Armor = currentData.armor;
        Speed = currentData.speed;
        Atk = currentData.atk;
        dataDictionary["MaxHp"] = MaxHp;
        dataDictionary["Armor"] = Armor;
        dataDictionary["Speed"] = Speed;
        dataDictionary["Atk"] = Atk;
    }

    private void InitialDictionary()
    {
        dataDictionary.Add("MaxHp", MaxHp);
        dataDictionary.Add("Armor", Armor);
        dataDictionary.Add("Speed", Speed);
        dataDictionary.Add("Atk", Atk);
        DataDictionary = dataDictionary;
        initialed = true;
    }
    public void GetBuffData()
    {
        //在三个等级中的Buff抽取
        int random = Random.Range(0, 3);
        BuffData data = buffDatas[random];
        if (!initialed)
        {
            InitialDictionary();
        }
        InitialProp(data);
    }
    
    
}
