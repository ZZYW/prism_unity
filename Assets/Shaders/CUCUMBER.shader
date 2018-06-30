// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/CUCUMBER" {
 Properties {
     _Color ("Color", Color) = (1,1,1,1)
     _MainTex ("Albedo (RGB)", 2D) = "white" {}
     _Glossiness ("Smoothness", Range(0,1)) = 0.5
     _Metallic ("Metallic", Range(0,1)) = 0.0
     _Shake("Shake", Range(0,50))=0.0
     _ShakeFreq("Shake Freq", Range(0,10))=0.0
     _RandomMult("Random Mult", Float) = 1.0
 }
 SubShader {
     Tags { "Queue" = "Transparent"   "RenderType"="Transparent" }
     LOD 200
     
     CGPROGRAM
     // Physically based Standard lighting model, and enable shadows on all light types
     #pragma surface surf Standard fullforwardshadows finalcolor:mycolor vertex:vert alpha
     #include "ClassicNoise3D.hlsl"
     // Use shader model 3.0 target, to get nicer looking lighting
     #pragma target 3.0

     sampler2D _MainTex;

    struct Input {
        float3 normal;
        float4 vertex;
        float2 uv_MainTex;
        float3 worldPos;
    };

     half _Glossiness;
     half _Metallic;
     fixed4 _Color;
     float _Shake;
     float _ShakeFreq;
     float _RandomMult;

    
     UNITY_INSTANCING_BUFFER_START(Props)
     UNITY_INSTANCING_BUFFER_END(Props)


         float nrand(float2 uv)
    {      
        return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
    }

    void vert (inout appdata_full v, out Input o) {       
      UNITY_INITIALIZE_OUTPUT(Input,o);
      v.vertex += cnoise(v.vertex.xyz + _Time.yyy * _ShakeFreq) * _Shake * v.normal.xyzx;
      v.vertex += nrand(v.texcoord) * _RandomMult;
      o.vertex = v.vertex;
    }



    void mycolor (Input IN, SurfaceOutputStandard o, inout fixed4 color){
        //do shit
    }

     void surf (Input IN, inout SurfaceOutputStandard o) {
         // Albedo comes from a texture tinted by color
         fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
         o.Albedo = c.rgb;
         // Metallic and smoothness come from slider variables
         o.Metallic = _Metallic;
         o.Smoothness = _Glossiness;
         o.Alpha = c.a;
     }



     ENDCG
 }
 FallBack "Diffuse"
}
