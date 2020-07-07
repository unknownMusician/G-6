﻿Shader "Custom/Minimap"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Intensity("Intensity", Float) = 1.0
		_Offset("Offset", Float) = 1.0
		_ShowInColor("ShowInColor", Float) = 0.0
	}
		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float _Intensity;
			float _Offset;
			float _ShowInColor;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				float2 uv = i.uv;

				float alpha;

				alpha = smoothstep(
					0,
					0.5	,
					min(uv.x, 1. - uv.x) *
					min(uv.y, 1. - uv.y)
				);

				return col;
			}
			ENDCG
		}
	}
}
