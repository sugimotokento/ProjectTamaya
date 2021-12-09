Shader "Unlit/UnlitTextureShader"
{
	Properties
	{
		_MainTex("MainTex"  , 2D) = "White" {}
		_Alpha("Alpha", Range(0.0, 1.0)) = 0
	
	}


		SubShader
		{
			Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha

			Pass {

				CGPROGRAM

			   #pragma vertex vert
			   #pragma fragment frag
			   #include "UnityCG.cginc"

				sampler2D _MainTex;
				float _Alpha;
	

				struct v2f {
					half4 vertex                : SV_POSITION;
					half2 uv               : TEXCOORD0;
				};

				v2f vert(float4 vertex : POSITION, float2 tex : TEXCOORD)
				{
					v2f o = (v2f)0;
					// ‚Ü‚¸UnityObjectToClipPos
					o.vertex = UnityObjectToClipPos(vertex);
					o.uv = tex;

					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{

					float4 col = tex2D(_MainTex, i.uv);
					col.a *= _Alpha;
					return col;
				}

				ENDCG
			}
		}
}
