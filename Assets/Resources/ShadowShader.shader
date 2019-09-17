Shader "Custom/ShadowShader"
{
	Properties{

	   _DesiredColor("Desired Color ", Color) = (0,0,0,0.4)
	   _MainTex("Base (RGB)", 2D) = "white" {}
	   _ShadowIntensity("ShadowIntesity", Range(0, 3)) = 0.5
	}

		SubShader{
		   Tags { "Queue" = "Transparent" }
		   Cull Off

		   Pass {
			   Stencil {
				   Ref 2
				   Comp NotEqual
				   Pass Replace
			   }

				Blend SrcAlpha OneMinusSrcAlpha

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				float4 _DesiredColor;
				float _ShadowIntensity;

				struct v2f {
					half4 pos : POSITION;
					half2 uv : TEXCOORD0;
					fixed4 worldpos : WORLDPOS;
				};

				v2f vert(appdata_img v) {
					v2f o;
					o.worldpos = mul(unity_ObjectToWorld, fixed4(v.vertex.x, v.vertex.y, 0, 1));
					o.pos = UnityObjectToClipPos(v.vertex);
					half2 uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord);
					o.uv = uv;
					return o;
				}

				half4 frag(v2f i) : COLOR {
					half4 color = tex2D(_MainTex, i.uv);//dobimo barvo pixla texture
					if (color.a == 0.0)//če je del texture neviden to pustimo
						discard;
					else
						if (i.worldpos.y > 0)//če je pixel nad linijo, kjer se senca začne jo skrijemo 
							
							color.a = 0;
						else
							color = _DesiredColor;//nastavimo barvo pixla, ki smo jo nastavili v unity nastavitvah
							//nastavimo intenzivnost sence glede na y
							//y je 0 ali manjše
							color.a = color.a *(_ShadowIntensity + i.worldpos.y);
						
					return color;
				}
				ENDCG
		   }

	}

		Fallback off
}
