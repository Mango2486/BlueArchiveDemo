Shader "Custom/AlpahShader"
{
    Properties {
		_Color ("Color Tint", Color) = (1, 1, 1, 1)
		_MainTex ("Main Tex", 2D) = "white" {}
		//我们使用一个新的属性_AlphaScale 来替代原先的_Cutoff 属性。_AlphaScale用于在透明纹理的基础上控制整体的透明度。
		_AlphaScale ("Alpha_Scale", Range(0, 1)) = 1
	}
	SubShader {
		//把Queue标签设置为Alphaparent	
		//把IgnoreProjector设置为true，意味着这个Shader不会受到投影器(Properties)的影响
		//RenderType标签可以让Unity把这个Shader归入到提前定义的组(这里就是Transparent组)，以指明该Shader是一个使用了透明度混合的Shader
		//使用了透明度测试的Shader都应该在SubShader设置这三个标签
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		
		Pass {
			ZWrite On
			ColorMask 0
		}

		Pass {
			Tags { "LightMode"="ForwardBase" }
			//为透明度混合进行合适的混合状态设置
			//关闭深度写入
			ZWrite Off
			//使用了Blend SrcFactor DstFactor语义
			//把源颜色(该片元着色器产生的颜色)的混合因子SrcFactor设为SrcAlpha, 而目标颜色(已经存在于颜色缓冲中的颜色)的混合因子 DstFactor设为 OneMinusSrcAlpha 。
			Blend SrcAlpha OneMinusSrcAlpha//这部分在下面 六 会讲到
			
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			#include "Lighting.cginc"
			
			fixed4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _AlphaScale;
			
			struct a2v {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
			};
			
			struct v2f {
				float4 pos : SV_POSITION;
				float3 worldNormal : TEXCOORD0;
				float3 worldPos : TEXCOORD1;
				float2 uv : TEXCOORD2;
			};
			
			v2f vert(a2v v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				
				return o;
			}
			
			fixed4 frag(v2f i) : SV_Target {
				fixed3 worldNormal = normalize(i.worldNormal);
				fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
				
				fixed4 texColor = tex2D(_MainTex, i.uv);
				
				fixed3 albedo = texColor.rgb * _Color.rgb;
				
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;
				
				fixed3 diffuse = _LightColor0.rgb * albedo * max(0, dot(worldNormal, worldLightDir));
				
				//设置了该片元着色器返回值中的透明通道，它是纹理像素的透明通道和材质参数_AlphaScale的乘积。
				return fixed4(ambient + diffuse, texColor.a * _AlphaScale);
			}
			
			ENDCG
		}
	} 
	FallBack "Transparent/VertexLit"
}


