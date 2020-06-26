Shader "Custom/GaussianBlurVert"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_PxIntensity("PxIntensity", Int) = 1.0
		_PlayerPos("PlayerPos", Vector) = (0.5, 0.5, 0.5, 0.5)
		_LookPos("LookPos", Vector) = (0.5, 0.5, 0.5, 0.5)
		_LookAngle("LookAngle", Float) = 0.0
		_Resolution("Resolution", Vector) = (1920, 1080, 0., 0.)
		_ViewWidth("ViewWidth", Float) = 1.0
		_Offset("Offset", Vector) = (0., 0., 0., 0.)
		_OffsetRot("OffsetRot", Vector) = (0., 0., 0., 0.)
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
			int _PxIntensity;
			float4 _PlayerPos;
			float4 _LookPos;
			float _LookAngle;
			float4 _Resolution;
			float _ViewWidth;
			float4 _Offset;
			float4 _OffsetRot;
			float _ShowInColor;

			float4 vertBlur(sampler2D tex, float2 uv, int intensity) {
				float4 col = float4(0, 0, 0, 0);
				/*for (int i = -intensity; i <= intensity; i++) {
					col += tex2D(_MainTex, uv + float2(0, i / 1080.0));
				}*/

				col += tex2D(_MainTex, uv);

				if (intensity > 0) {
					col += tex2D(_MainTex, uv + float2(0, 1 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -1 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 2 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -2 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 3 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -3 / 1080.0));
				}
				if (intensity > 1) {
					col += tex2D(_MainTex, uv + float2(0, 4 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -4 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 5 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -5 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 6 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -6 / 1080.0));
				}
				if (intensity > 2) {
					col += tex2D(_MainTex, uv + float2(0, 7 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -7 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 8 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -8 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 9 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -9 / 1080.0));
				}
				if (intensity > 3) {
					col += tex2D(_MainTex, uv + float2(0, 10 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -10 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 11 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -11 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 12 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -12 / 1080.0));
				}
				if (intensity > 4) {
					col += tex2D(_MainTex, uv + float2(0, 13 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -13 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 14 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -14 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 15 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -15 / 1080.0));
				}
				if (intensity > 5) {
					col += tex2D(_MainTex, uv + float2(0, 16 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -16 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 17 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -17 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 18 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -18 / 1080.0));
				}
				if (intensity > 6) {
					col += tex2D(_MainTex, uv + float2(0, 19 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -19 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 20 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -20 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 21 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -21 / 1080.0));
				}
				if (intensity > 7) {
					col += tex2D(_MainTex, uv + float2(0, 22 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -22 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 23 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -23 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 24 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -24 / 1080.0));
				}
				if (intensity > 8) {
					col += tex2D(_MainTex, uv + float2(0, 25 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -25 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 26 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -26 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 27 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -27 / 1080.0));
				}
				if (intensity > 9) {
					col += tex2D(_MainTex, uv + float2(0, 28 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -28 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 29 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -29 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 30 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -30 / 1080.0));
				}
				if (intensity > 10) {
					col += tex2D(_MainTex, uv + float2(0, 31 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -31 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 32 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -32 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, 33 / 1080.0));
					col += tex2D(_MainTex, uv + float2(0, -33 / 1080.0));
				}

				col /= intensity * 6 + 1;
				return col;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				float intensity;

				float2 uv = i.uv; 
				uv -= _PlayerPos;
				uv -= _Offset;
				uv = float2(
					uv.x * sin(_LookAngle) + uv.y * -cos(_LookAngle),
					uv.x * cos(_LookAngle) + uv.y * sin(_LookAngle)
				);
				//uv += _PlayerPos;
				uv -= _OffsetRot;

				if (uv.y > (uv.x) / _ViewWidth * (uv.x) / _ViewWidth) { 
					intensity = 1.;
				}
				intensity = smoothstep((uv.x) / _ViewWidth * (uv.x) / _ViewWidth, 0., uv.y);
				//float intensity = -3. + clamp(_PxIntensity * min(abs(distance(i.uv, _PlayerPos)), abs(distance(i.uv, _LookPos))), 3., 10.);
				//
				col = vertBlur(_MainTex, i.uv, 10. * intensity);
				col += float4(intensity * 0.05 * _ShowInColor, 0., 0., 1.0);
				return col;
			}
			ENDCG
		}
	}
}
