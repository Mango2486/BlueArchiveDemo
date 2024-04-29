using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//角色统一拥有的数据
public class CharacterSO : ScriptableObject
{
   [Header("移动速度")] 
   public float moveSpeed;
   public float speedModifier;

   [Header("生命值")] 
   public int maxHP;
   public int currentHP;

   [Header("护盾值")] 
   public int maxShield;
   public int currentShield;

   [Header("攻击力")] 
   public int attack;

   [Header("防御力")] 
   public int defend;
}
