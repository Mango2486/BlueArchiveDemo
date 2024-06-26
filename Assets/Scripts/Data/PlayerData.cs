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
        [Header("护甲值")] 
        public float armor;
        [Header("护盾值")] 
        public float shield;
    }
}
