// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/DistanceAlpha"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }

		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 worldPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			v2f vert(appdata_base v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target{
				fixed4 col = tex2D(_MainTex, i.uv);
				float dist = distance(i.worldPos, _WorldSpaceCameraPos);
				if (dist > 255)
					col.a = 0;
				else
					col.a = (255 - dist) * col.a / 255;
				return col;
			}

			ENDCG
		}
	}
}