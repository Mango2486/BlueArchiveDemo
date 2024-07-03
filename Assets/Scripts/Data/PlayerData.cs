using System.Collections.Generic;
using UnityEngine;

namespace Data
{   
    [CreateAssetMenu(menuName = "Data/Player", fileName = "PlayerData" )]
    public class PlayerData : ScriptableObject
    {   
        [Header("血量")]
        public float maxHp;
        [Header("攻击力")]
        public float atk;
        [Header("移动速度")] 
        public float speed;
        [Header("受击无敌时间")] 
        public float invincibleTime;
        [Header("射击间隔")]
        public float shootInterval;
        [Header("等级")] 
        public int level;
        [Header("升级所需经验值")] 
        public float[] maxExp;
        [Header("护甲值")] 
        public float armor;
        [Header("护盾值")] 
        public float shield;
        

    }
}
