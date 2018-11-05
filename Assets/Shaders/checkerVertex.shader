Shader "Custom/checkerVertex"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Density ("Density", Range(2,50)) = 30
		_Cutoff ("Glitch Density", Range(0, 1)) = 0.1
		_Displacement("Glitch Displacement", Float) = 0.5
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			//include random number library
			#include "ClassicNoise2D.hlsl"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			float _Density;
			float _Cutoff;
			float _Displacement;

			v2f vert (appdata v)
			{
				v2f o;
				//Transforms a point from object space to the camera
				//’s clip space in homogeneous coordinates. 
				//This is the equivalent of mul(UNITY_MATRIX_MVP, float4(pos, 1.0)), and should be used in its place.
				o.vertex = UnityObjectToClipPos(v.vertex);
				//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv = v.uv * _Density;
				float thresh = pnoise(v.vertex.xy+_Time.xx*10, v.vertex.xy);
				if(thresh < _Cutoff){
                        //models'displayed along y axis
              
                        o.vertex.y += _Displacement;
                    
                    }
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				//fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
				//return col;
				float2 c = i.uv;
                c = floor(c) / 2;
                float checker = frac(c.x + c.y) * 2;
                UNITY_APPLY_FOG(i.fogCoord, checker);
                return checker;
				
			}
			ENDCG
		}
	}
}
