using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
   private static T instance;
   public static T Instance => instance;

   private void Awake()
   {
      if (instance == null)
      {
         instance = this as T;
      }
      InitAwake();
   }
   
   protected virtual void InitAwake()
   {
      
   }
   
}
