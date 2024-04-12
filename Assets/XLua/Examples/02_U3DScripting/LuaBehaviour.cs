/*
 * Tencent is pleased to support the open source community by making xLua available.
 * Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
 * Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XLua;
using System;

namespace XLuaTest
{
    [System.Serializable]
    public class Injection
    {
        public string name;
        public GameObject value;
    }
    
    //标签代表使用Lua来调用C#
    [LuaCallCSharp]
    public class LuaBehaviour : MonoBehaviour
    {   
        //xlua中lua是txt文件,获取lua文件
        public TextAsset luaScript;
        //一个注入类，用于充当存储到lua中键值对，键是GameObject的名字,值就是GameObject本身
        public Injection[] injections;
        
        //Lua的虚拟机，出于开销的考虑，全局唯一，所以设置为static
        internal static LuaEnv luaEnv = new LuaEnv(); //all lua behaviour shared one luaenv only!
        //luaGC的计时器，间隔为1s
        internal static float lastGCTime = 0;
        internal const float GCInterval = 1;//1 second 
        
        //对应lua脚本中的三个MonoBehaviour方法
        private Action luaStart;
        private Action luaUpdate;
        private Action luaOnDestroy;
        
        //用于获取lua文件中的变量,一个独立的脚本环境
        private LuaTable scriptEnv;

        void Awake()
        {   
            //首先新建一个脚本环境
            scriptEnv = luaEnv.NewTable();

            // 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
            LuaTable meta = luaEnv.NewTable();
            //meta表作为元表时索引指向设置为全局，也就是_G表
            meta.Set("__index", luaEnv.Global);
            //此时当前脚本环境的lua表就获取到了lua文件的全局变量、函数
            scriptEnv.SetMetaTable(meta);
            //meta表已经完成了给scriptEnv表连接上_G表的任务，就可以被释放了。
            //因为先设置好了__index,并且设置好了元表关系，就算meta表被释放，事先设置好的元表关系依然有效，__index的设置也有效，scriptEnv仍然可以索引到_G表
            meta.Dispose();

            scriptEnv.Set("self", this);
            foreach (var injection in injections)
            {
                scriptEnv.Set(injection.name, injection.value);
            }
            
            //在scriptEnv环境中执行luaScript.text脚本
            luaEnv.DoString(luaScript.text, "LuaTestScript", scriptEnv);

            Action luaAwake = scriptEnv.Get<Action>("awake");
            scriptEnv.Get("start", out luaStart);
            scriptEnv.Get("update", out luaUpdate);
            scriptEnv.Get("ondestroy", out luaOnDestroy);

            if (luaAwake != null)
            {
                luaAwake();
            }
        }

        // Use this for initialization
        void Start()
        {
            if (luaStart != null)
            {
                luaStart();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (luaUpdate != null)
            {
                luaUpdate();
            }
            if (Time.time - LuaBehaviour.lastGCTime > GCInterval)
            {
                luaEnv.Tick();//手动调用GC
                LuaBehaviour.lastGCTime = Time.time;
            }
        }

        void OnDestroy()
        {
            if (luaOnDestroy != null)
            {
                luaOnDestroy();
            }
            luaOnDestroy = null;
            luaUpdate = null;
            luaStart = null;
            scriptEnv.Dispose();
            injections = null;
        }
    }
}
