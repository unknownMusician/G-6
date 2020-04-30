Shader "Custom/GaussianBlurHoriz"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_PxIntensity("PxIntensity", Int) = 1.0
		_PlayerPos("PlayerPos", Vector) = (0.5, 0.5, 0.5, 0.5)
		_LookPos("LookPos", Vector) = (0.5, 0.5, 0.5, 0.5)
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

			uniform float2 resolution = float2(1920, 1080);
			uniform float2 px = float2(1 / 1920, 1 / 1080);
			sampler2D _MainTex;
			int _PxIntensity;
			float4 _PlayerPos;
			float4 _LookPos;

			float4 blur(sampler2D tex, float2 uv, int intensity) {
				float4 col = float4(0, 0, 0, 0);
				for (int i = -intensity; i <= intensity; i++) {
					for (int j = -intensity; j <= intensity; j++) {
						col += tex2D(_MainTex, uv + float2(i/1920.0, j/ 1080.0));
					}
				}
				col /= (intensity * 2 + 1) * (intensity * 2 + 1);
				return col;
			}

			float4 pixelate(sampler2D tex, float2 uv, float intensity) {
				return tex2D(_MainTex, floor(uv * intensity) / intensity);
			}

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

				if (distance(i.uv.xy, float2(0.5, 0.4)) > 0.2) {
					//col = pixelate(_MainTex, i.uv, 1/distance(i.uv.xy, float2(0.5, 0.4)) * 20.);
					//col = blur(_MainTex, i.uv, 1.);
				}
				//col = pixelate(_MainTex, i.uv, _PxIntensity);
				col = horizBlur(_MainTex, i.uv, -2. + clamp(_PxIntensity * min(abs(distance(i.uv, _PlayerPos)), abs(distance(i.uv, _LookPos))), 2., 10.));
				return col;
			}
			ENDCG
		}
	}
}
