﻿Shader "Unlit/MapShader" {

	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_FluidGradient ("Fluid Gradient Color", 2D) = "white" {}
		_FluidTex ("Fluid Detail", 2D) = "white" {}
		_Oscillation ("Fluid Oscillation Level", Range(0, 1)) = 0
		_Detail ("Fluid Detail Weight", Range(0, 1)) = 0
		_Bias ("Fluid Level Bias", Range(-1, 1)) = 0
		_BaseIntensity ("Base Light Intensity", Range(0, 1)) = 0
		_Flow ("Fluid Flowing Speed", Range(0, 10)) = 1
	}

    SubShader {
	
		Tags {"RenderType"="Opaque" "LightMode"="ForwardBase"}
		
        Pass {
		
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#pragma multi_compile_fog
			
            #include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"
			
			sampler2D _MainTex;
			sampler2D _FluidGradient;
			sampler2D _FluidTex;
			half _Oscillation;
			half _Detail;
			half _Bias;
			half _BaseIntensity;
			half _Flow;
			
			struct i2v {
				float4 pos : POSITION;
				half2 uv1 : TEXCOORD0;
				half2 uv2 : TEXCOORD1;
				half3 normal : NORMAL;
			};
         
            struct v2f {
                float4 pos : POSITION;
                half2 uv1 : TEXCOORD0;
				half2 uv2 : TEXCOORD1;
				half2 uv3 : TEXCOORD2;
				half4 light : COLOR;
				UNITY_FOG_COORDS(3)
            };
			
			float4 _MainTex_ST;
			float4 _FluidTex_ST;
			float4 _FluidGradient_ST;
            
            v2f vert (i2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.pos);
				
				o.uv1 = TRANSFORM_TEX(v.uv1, _FluidGradient);
				o.uv2 = TRANSFORM_TEX(v.uv2, _FluidTex);
				o.uv3 = TRANSFORM_TEX(v.uv2, _MainTex);
				
				half3 world_normal = UnityObjectToWorldNormal(v.normal);
				half intensity = _BaseIntensity + (max(0.4, dot(world_normal, _WorldSpaceLightPos0.xyz))) * (1 - _BaseIntensity);
                o.light = _LightColor0 * intensity;
				
				UNITY_TRANSFER_FOG(o, o.pos);
                return o;
            }

            half4 frag (v2f i) : SV_Target 
			{
				half detail = (tex2D(_FluidTex, (_Flow * (_Time[0] * half2(1, 1))) + i.uv2).r - 0.5) * 2 * _Detail;
				half time_osc = _SinTime[3] * _Oscillation;
				
				i.uv1.x = clamp(i.uv1.x + time_osc + detail + _Bias, 0.01f, 0.99f);
				
				half4 grd_col = tex2D(_FluidGradient, i.uv1);
				half4 tex_col = tex2D(_MainTex, i.uv3);
				
				half4 col = lerp(grd_col, tex_col, 1 - (grd_col.a)) * i.light;
				
				UNITY_APPLY_FOG(i.fogCoord, col);
				
				return col;
			}
            ENDCG
        }
    } 
	FallBack "Diffuse"
}