﻿Shader "Hidden/Blur Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

			uniform float2 resolution = float2(1920, 1080);
			uniform float2 px = float2(1 / 1920, 1 / 1080);
			sampler2D _MainTex;

			float4 blur(sampler2D tex, float2 uv, int intensivity) {
				float4 col = float4(0, 0, 0, 0);
				for (int i = -intensivity; i <= intensivity; i++) {
					for (int j = -intensivity; j <= intensivity; j++) {
						col += tex2D(_MainTex, uv + float2(px.x * i, px.y * j));
					}
				}
				col /= (intensivity * 2 + 1) * (intensivity * 2 + 1);
				return col;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				col = blur(_MainTex, i.uv, 3);

				return col;
			}
            ENDCG
        }
    }
}
