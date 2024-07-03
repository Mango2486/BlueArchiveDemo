using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Data
{   
    public enum PropertyName
    {
        MaxHp,
        Atk,
        Armor,
        Speed,
    }
    [CreateAssetMenu(menuName = "Data/Buff", fileName = "BuffData" )]
    public class BuffData : ScriptableObject
    {
        [Header("最大血量")]
        public float maxHp;
        [Header("攻击力")]
        public float atk;
        [Header("护甲值")] 
        public float armor;
        [Header("移动速度")] 
        public float speed;
    }
}
