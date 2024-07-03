using System;
using System.Collections.Generic;
using Data;
using Unity.VisualScripting;
using UnityEngine;

namespace MVCTest.Player
{   
    [Serializable]
    public class PlayerModel
    {
        public PlayerModel(PlayerData playerData)
        {
            Instance = this;
            SetData(playerData);
            Initialize();
        }

        private PlayerData playerData;

        public static PlayerModel Instance { get;private set; }
        
        //字典中存放可更改的属性
        private  Dictionary<string, float> propDictionary = new Dictionary<string, float>();
        
        public float CurrentHp { get; private set; }
        
        public float InvincibleTime { get; private set; }
        
        public float ShootInterval { get; private set; }
        
        public float[] MaxExp { get; private set; }
        
        public float Exp { get; private set; }

        public int Level { get; private set; }
        
        public Dictionary<string, float> PropDictionary { get; private set; }

        private void Initialize()
        {
          
            CurrentHp = playerData.maxHp;
            InvincibleTime = playerData.invincibleTime;
            ShootInterval = playerData.shootInterval;
            MaxExp = playerData.maxExp;
            Level = playerData.level;
            Exp = 0f;
            InitDictionary();
        }

        public delegate void UnityAction<PlayerModel>(PlayerModel playerModel);
        public event UnityAction<PlayerModel> Actions;

        private void UpdateInformation()
        {
            Actions?.Invoke(this);
        }

        public void GetHurt(float damage)
        {
            if (CurrentHp > 0)
            {
                CurrentHp -= damage;
                CurrentHp = Mathf.Clamp(CurrentHp, 0, propDictionary["MaxHp"]);
            }
            UpdateInformation();
        }

        public void GetExp(float exp)
        {   
            //获得经验
            Exp += exp;
            //检查是否符合升级要求
            if (Exp >= MaxExp[Level])
            {
                Exp -= MaxExp[Level];
                Level++;
            }
            //更新UI显示
            UpdateInformation();
        }

        private void SetData(PlayerData playerData)
        {
            this.playerData = playerData;
        }


        private void InitDictionary()
        {   
            //会更改的数据使用字典对应存储。
            propDictionary.Add("MaxHp", playerData.maxHp);
            propDictionary.Add("Atk", playerData.atk);
            propDictionary.Add("Armor", playerData.armor);
            propDictionary.Add("Speed", playerData.speed);
            propDictionary.Add("Shield", playerData.shield);
            PropDictionary = propDictionary;
        }
    }
}
