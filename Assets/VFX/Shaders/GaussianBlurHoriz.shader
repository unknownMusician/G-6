Shader "Custom/GaussianBlurHoriz"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Intensity("Intensity", Float) = 1.0
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
			float _Intensity;
			float4 _PlayerPos;
			float4 _LookPos;
			float _LookAngle;
			float4 _Resolution;
			float _ViewWidth;
			float4 _Offset;
			float4 _OffsetRot;
			float _ShowInColor;

			float4 horizBlur(sampler2D tex, float2 uv, int intensity) {
				float4 col = float4(0, 0, 0, 0);
				/*for (int i = -intensity; i <= intensity; i++) {
					col += tex2D(_MainTex, uv + float2(i / 1920.0, 0));
				}*/

				col += tex2D(_MainTex, uv);

				if (intensity > 0) {
					col += tex2D(_MainTex, uv + float2(1 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-1 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(2 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-2 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(3 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-3 / 1920.0, 0));
				}
				if (intensity > 1) {
					col += tex2D(_MainTex, uv + float2(4 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-4 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(5 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-5 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(6 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-6 / 1920.0, 0));
				}
				if (intensity > 2) {
					col += tex2D(_MainTex, uv + float2(7 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-7 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(8 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-8 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(9 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-9 / 1920.0, 0));
				}
				if (intensity > 3) {
					col += tex2D(_MainTex, uv + float2(10 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-10 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(11 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-11 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(12 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-12 / 1920.0, 0));
				}
				if (intensity > 4) {
					col += tex2D(_MainTex, uv + float2(13 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-13 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(14 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-14 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(15 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-15 / 1920.0, 0));
				}
				if (intensity > 5) {
					col += tex2D(_MainTex, uv + float2(16 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-16 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(17 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-17 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(18 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-18 / 1920.0, 0));
				}
				if (intensity > 6) {
					col += tex2D(_MainTex, uv + float2(19 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-19 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(20 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-20 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(21 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-21 / 1920.0, 0));
				}
				if (intensity > 7) {
					col += tex2D(_MainTex, uv + float2(22 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-22 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(23 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-23 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(24 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-24 / 1920.0, 0));
				}
				if (intensity > 8) {
					col += tex2D(_MainTex, uv + float2(25 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-25 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(26 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-26 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(27 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-27 / 1920.0, 0));
				}
				if (intensity > 9) {
					col += tex2D(_MainTex, uv + float2(28 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-28 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(29 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-29 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(30 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-30 / 1920.0, 0));
				}
				if (intensity > 10) {
					col += tex2D(_MainTex, uv + float2(31 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-31 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(32 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-32 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(33 / 1920.0, 0));
					col += tex2D(_MainTex, uv + float2(-33 / 1920.0, 0));
				}

				col /= intensity * 6 + 1;
				return col;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				float intensity;

				float2 uv = i.uv;
				float2 uvr = i.uv * _Resolution;
				uvr -= _PlayerPos * _Resolution;
				uv -= _Offset;
				uvr = float2(
					uvr.x * sin(_LookAngle) + uvr.y * -cos(_LookAngle),
					uvr.x * cos(_LookAngle) + uvr.y * sin(_LookAngle)
				);
				uv -= _OffsetRot;

				if (uvr.y > (uvr.x) / _ViewWidth * (uvr.x) / _ViewWidth) {
					intensity = 1.;
				}
				float hyperbola = smoothstep((uvr.x) / _ViewWidth * (uvr.x) / _ViewWidth, -100., uvr.y);
				float dist = distance(float2(0., 0.), uvr.xy);
				float sphere = smoothstep(10. * _ViewWidth, 50. * _ViewWidth, dist);
				intensity = hyperbola * sphere;
				intensity *= _Intensity;

				col = horizBlur(_MainTex, i.uv, 10. * intensity);
				col += float4(0., 0., intensity * 0.5 * _ShowInColor, 1.0);
				return col;
			}
			ENDCG
		}
	}
}
