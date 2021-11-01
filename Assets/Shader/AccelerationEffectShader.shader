Shader "Unlit/AccelerationEffectShader"
{
	Properties
	{
		_NoiseMap("NoiseMap"  , 2D)="White" {}
		_Timer("timer", Range(0.0, 1000000.0)) = 0
		_NoiseLate("noiseLate",Range(0.0, 3.0)) = 0.03
		_TexSize("TexSize", Range(0.0, 1.0))=1
	}


		SubShader
	{
		// 描画結果をテクスチャに書き込みたいタイミングに応じてQueueを調整する
		Tags { "Queue" = "Transparent" }

		GrabPass { "_GrabPassTexture" }

		Pass {

			CGPROGRAM

		   #pragma vertex vert
		   #pragma fragment frag
		   #include "UnityCG.cginc"

			sampler2D _NoiseMap;
			sampler2D _GrabPassTexture;
			float _Timer;
			float _NoiseLate;
			float _TexSize;

			struct v2f {
				half4 vertex                : SV_POSITION;
				half4 grabPos               : TEXCOORD0;
				half2 uv               : TEXCOORD1;
			};

			v2f vert(float4 vertex : POSITION, float2 tex : TEXCOORD)
			{
				v2f o = (v2f)0;
				// まずUnityObjectToClipPos
				o.vertex = UnityObjectToClipPos(vertex);
				// GrabPassのテクスチャをサンプリングするUV座標はComputeGrabScreenPosで求める
				o.grabPos = ComputeGrabScreenPos(o.vertex);
				o.uv = tex;

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{

				float4 noiseMap = tex2D(_NoiseMap, float4(i.uv.x*_TexSize + _Timer, i.uv.y*_TexSize, 0, 0));

				float y = (i.uv.y * 100) % 5*0.2f;
				float4 texAdd = float4(noiseMap.r-0.5,noiseMap.g-0.5,0,0);

			
				//return noiseMap;
				return tex2Dproj(_GrabPassTexture, i.grabPos + texAdd * _NoiseLate);
			}

			ENDCG
		}
	}
}
