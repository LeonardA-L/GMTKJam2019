// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Cel"
{
    Properties
    {
		_Color("Color", Color) = (1,1,1,1)
		_Color2 ("Outline Color", Color) = (1,1,1,1)
		_Width("Outline Width", float) = 0.005
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_ToonLut("Toon LUT", 2D) = "white" {}
    }
    SubShader
    {
		Pass{
			Tags{
				"LightMode" = "ForwardAdd"
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase

			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			#include "Lighting.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float3 normal: NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : TEXCOORD1;
			};

			v2f vert(appdata v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.normal = UnityObjectToWorldNormal(v.normal);
				return o;
			}

			sampler2D _MainTex;
			sampler2D _ToonLut;
			fixed4 _Color;

			fixed4 frag(v2f i) : SV_Target{
				float3 normal = normalize(i.normal);
				float3 lightDir = -normalize(_WorldSpaceLightPos0.xyz - i.pos.xyz);
				float ndotl = max(0.0, dot(normal, lightDir));
				float3 lut = tex2D(_ToonLut, float2(ndotl, 0));
				float3 directDiffuse = lut * _LightColor0;
				fixed4 color = tex2D(_MainTex, i.uv) * _Color;
				color.rgb *= directDiffuse;
				color.a = 1.0;
				return color;
			}
				ENDCG
		}
			Pass{
				Cull front
				Tags{
				"LightMode" = "ForwardBase"
			}

				CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fwdbase

#include "UnityCG.cginc"
#include "AutoLight.cginc"
#include "Lighting.cginc"

				struct appdata {
				float4 vertex : POSITION;
				float3 normal: NORMAL;
			};

			struct v2f {
				float4 position : SV_POSITION;
			};

			fixed4 _Color2;
			float _Width;

			v2f vert(appdata v) {
				v2f o;
				float4 position = UnityObjectToClipPos(v.vertex);
				o.position = position + _Width * UnityObjectToClipPos(normalize(v.normal));
				return o;
			}


			fixed4 frag(v2f i) : SV_Target{
				fixed4 col = _Color2;
			col.a = 1.0;
			return col;
			}
				ENDCG
			}
    }
    FallBack "Diffuse"
}
