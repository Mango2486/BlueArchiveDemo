using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/Property", fileName = "PropertiesData" )]
    public class PropertiesData : ScriptableObject
    {   
        [Header("血量")]
        public float hp;
        [Header("攻击力")]
        public float atk;
        [Header("护盾值")] 
        public float shield;
        [Header("经验值")]
        public float exp;
    }
}
