using UnityEngine;

namespace Data
{   
    [CreateAssetMenu(menuName = "Data/Enemy", fileName = "EnemyData" )]
    public class EnemyData : ScriptableObject
    {
        [Header("血量")]
        public float maxHp;
        [Header("攻击力")]
        public float atk;
        [Header("移动速度")] 
        public float speed;
        [Header("护甲值")] 
        public float armor;
        [Header("护盾值")] 
        public float shield;
    }
}
