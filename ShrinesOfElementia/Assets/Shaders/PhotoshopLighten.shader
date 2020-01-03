// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/PhotoshopLighten"
{
	Properties
	{
		_MainTex("Texture", 2D) = "" {}
		_Color("Blend Color", Color) = (0.5, 0.5, 0.5, 1.0)
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"RenderType" = "Transparent"
			}

			Lighting Off
			Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest

				#include "UnityCG.cginc"

				struct appdata_custom
				{
					float4 vertex : POSITION;
					fixed2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex : POSITION;
					fixed2 uv : TEXCOORD0;
				};

				sampler2D _MainTex;
				fixed4 _MainTex_ST;
				fixed4 _Color;

				v2f vert(appdata_custom v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv,_MainTex);
					return o;
				}

				fixed4 frag(v2f i) : COLOR
				{
					fixed4 diffuse = tex2D(_MainTex, i.uv);
					fixed luminance = dot(diffuse, fixed4(0.2126, 0.7152, 0.0722, 0));
					fixed oldAlpha = diffuse.a;

					if (luminance < 0.5)
						diffuse *= 2 * _Color;
					else
						diffuse = 1 - 2 * (1 - diffuse) * (1 - _Color);

					diffuse.a = oldAlpha * _Color.a;
					return diffuse;
				}
				ENDCG
			}
		}
			Fallback off
}
