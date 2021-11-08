Shader "Custom/DieFadeShader"
{
    Properties
    {
		_Timer("timer", Range(0, 2)) = 0

    }
		SubShader
	{
		// 描画結果をテクスチャに書き込みたいタイミングに応じてQueueを調整する
		Tags { "Queue" = "Transparent" }

		Blend SrcAlpha OneMinusSrcAlpha
		LOD 100

		Pass {

			CGPROGRAM

		   #pragma vertex vert
		   #pragma fragment frag
		   #include "UnityCG.cginc"

			float _Timer;
	

			struct v2f {
				half4 vertex                : SV_POSITION;
				half2 uv               : TEXCOORD0;
			};

			v2f vert(float4 vertex : POSITION, float2 tex : TEXCOORD)
			{
				v2f o = (v2f)0;
				o.vertex = UnityObjectToClipPos(vertex);
				o.uv = tex;

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float2 mannaka = float2(0.5, 0.5);
				float2 dist = i.uv.xy - mannaka.xy;

				float len = length(dist);
				float alpha =pow(len,1)*5;
				return float4(0,0,0,alpha*_Timer+(_Timer*0.5));
			}

			ENDCG
		}
	}
}
