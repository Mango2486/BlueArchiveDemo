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

namespace Tutorial
{
    public class CSCallLua : MonoBehaviour
    {
        LuaEnv luaenv = null;
        string script = @"
        a = 1
        b = 'hello world'
        c = true

        d = {
           f1 = 12, 
           f2 = 34, 
           1, 2, 3, 5, 7, 2.3355,
           add = function(self, a, b) 
              print('d.add called')
              return a + b 
           end
        }

        function e()
            print('i am e')
            return 1,'string',2
        end

        function f(a, b)
            print('a', a, 'b', b)
            return {f1 = 1024},1
        end
        
        function ret_e()
            print('ret_e called')
            return e
        end
    ";

        public class DClass
        {
            public int f1;
            public int f2;
            public int f3;
        }

        [CSharpCallLua]
        public interface ItfD
        {
            int f1 { get; set; }
            int f2 { get; set; }
            int add(int a, int b);
        }

        [CSharpCallLua]
        public delegate int FDelegate(int a, string b, out DClass c);
        [CSharpCallLua]
        public delegate DClass TestDelegate(int a, string b, out int c);

        [CSharpCallLua]
        public delegate Action GetE();

        // Use this for initialization
        void Start()
        {   
            //创建lua虚拟环境
            luaenv = new LuaEnv();
            //在虚拟环境中执行script中的lua脚本
            luaenv.DoString(script);
            //通过大G表获得全局变量的a,b,c
            //Get<T>(key)指定获取值的返回
            Debug.Log("_G.a = " + luaenv.Global.Get<int>("a"));
            Debug.Log("_G.b = " + luaenv.Global.Get<string>("b"));
            Debug.Log("_G.c = " + luaenv.Global.Get<bool>("c"));
            
            //映射到class或者struct的前提是属性或者变量为public，class中的属性相对于table可多可少，因为只会获得class中对应属性的映射。
            DClass d = luaenv.Global.Get<DClass>("d");//映射到有对应字段的class，by value
            Debug.Log("_G.d = {f1=" + d.f1 + ", f2=" + d.f2 + ", f3=" + d.f3 + "}");

            Dictionary<string, double> d1 = luaenv.Global.Get<Dictionary<string, double>>("d");//映射到Dictionary<string, double>，by value
            
            Debug.Log("_G.d = {f1=" + d1["f1"] + ", f2=" + d1["f2"] + "}, d.Count=" + d1.Count);
            
            List<string> d2 = luaenv.Global.Get<List<string>>("d"); //映射到List<double>，by value
            foreach (var VARIABLE in d2)
            {
                Debug.Log(VARIABLE);
            }
            Debug.Log("_G.d.len = " + d2.Count);

            ItfD d3 = luaenv.Global.Get<ItfD>("d"); //映射到interface实例，by ref，这个要求interface加到生成列表，否则会返回null，建议用法
            d3.f2 = 1000;
            Debug.Log("_G.d = {f1=" + d3.f1 + ", f2=" + d3.f2 + "}");
            //test
            Debug.Log("_G.d:add(1, 2)=" + d3.add(1, 2));

            LuaTable d4 = luaenv.Global.Get<LuaTable>("d");//映射到LuaTable，by ref
            Debug.Log("_G.d = {f1=" + d4.Get<int>("f1") + ", f2=" + d4.Get<int>("f2") + "}");


            Action e = luaenv.Global.Get<Action>("e");//映射到一个delgate，要求delegate加到生成列表，否则返回null，建议用法
            e();

            TestDelegate test = luaenv.Global.Get<TestDelegate>("f");
            int testD;
            DClass testRet = test(100, "Mango",out testD);
            Debug.Log("test.d = {f1=" + testRet.f1 + ", f2=" + testRet.f2 + "}, ret=" + testD );
            
            /*
            FDelegate f = luaenv.Global.Get<FDelegate>("f");
            DClass d_ret;
            int f_ret = f(100, "John", out d_ret);//lua的多返回值映射：从左往右映射到c#的输出参数，输出参数包括返回值，out参数，ref参数
            Debug.Log("ret.d = {f1=" + d_ret.f1 + ", f2=" + d_ret.f2 + "}, ret=" + f_ret);
            */
            GetE ret_e = luaenv.Global.Get<GetE>("ret_e");//delegate可以返回更复杂的类型，甚至是另外一个delegate
            e = ret_e();
            e();

            LuaFunction d_e = luaenv.Global.Get<LuaFunction>("e");
            var test2 = d_e.Call();
            foreach (var VARIABLE in test2)
            {
                Debug.Log(VARIABLE);
            }


        }

        // Update is called once per frame
        void Update()
        {
            if (luaenv != null)
            {
                luaenv.Tick();
            }
        }

        void OnDestroy()
        {
            luaenv.Dispose();
        }
    }
}
