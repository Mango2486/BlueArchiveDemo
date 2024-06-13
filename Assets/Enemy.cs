using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   //整个场地目前是 50 * 50
   //刷怪最好是在玩家视野内
   
   //没必要，毕竟主体是类幸存者，普通怪就追着玩家就完了
   
   
   //需要一个Trigger用于检测玩家，一个Collider怪物自身负责碰撞
   //检测到玩家之后就用寻路一直追
}
