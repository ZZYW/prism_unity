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
     [Toggle(FILL_WITH_RED)]
     _NormalRainbow("Normal Rainbow", Int) = 0
     [Toggle]
     _RMStyle("Rosa Menkmen Style", Int) = 0
     _NoisePara01("Noise Para01", Range(0,1)) = 0.2
     _NoisePara02("Noise Para02", Range(1,5)) = 2
     _NoisePara03("Noise Para03", Range(0,20)) = 0
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
        float2 uv;
    };

     half _Glossiness;
     half _Metallic;
     fixed4 _Color;
     float _Shake;
     float _ShakeFreq;
     float _RandomMult;
     int _NormalRainbow;
     int _RMStyle;
     float _NoisePara01;
      float _NoisePara02;
      float _NoisePara03;

    
     UNITY_INSTANCING_BUFFER_START(Props)
     UNITY_INSTANCING_BUFFER_END(Props)

     //One line randome function! Dope!
    float nrand(float2 uv){return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);}

    void vert (inout appdata_full v, out Input o) {       
      UNITY_INITIALIZE_OUTPUT(Input,o);
      o.uv = v.texcoord;
      v.vertex += cnoise(v.vertex.xyz + _Time.yyy * _ShakeFreq) * _Shake * v.normal.xyzx;
      v.vertex +=  (cnoise(v.vertex.xyz * _NoisePara03) - 0.5) * _RandomMult;
      o.vertex = v.vertex;
    }

   
    void mycolor (Input IN, SurfaceOutputStandard o, inout fixed4 color){
        if(_NormalRainbow > 0) {
            color = fixed4(IN.vertex.xyz+ fixed3(0.5,0.5,0.5),1);
        }
        if(_RMStyle > 0) {
            float a = IN.uv_MainTex.x;
           // float b = step(a, 0.1);
            if( (a * 99999) % 2 > 0.5){
               a = 1;
            }else{
               a = 0;
            }

            if(cnoise(IN.vertex.xyz*_NoisePara02) > _NoisePara01){
                a = 0;
            }

            color = fixed4(a,a,a,1);
        }
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
