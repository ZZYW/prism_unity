// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/CUCUMBER" {

 Properties {
     _Color ("Color", Color) = (1,1,1,1)
     _MainTex ("Albedo (RGB)", 2D) = "white" {}
     _FogColor("Fog Color", Color) = (0.0,0.0,0.0,1.0)
     _Glossiness ("Smoothness", Range(0,1)) = 0.5
     _Metallic ("Metallic", Range(0,1)) = 0.0
     _Shake("Shake", Range(0,50))=0.0
     _ShakeFreq("Shake Freq", Range(0,10))=0.0
     _RandomMult("Random Mult", Float) = 1.0
     [Toggle(FILL_WITH_RED)]
     _NormalRainbow("Normal Rainbow", Int) = 0
     [Toggle]
     _RMStyle("Rosa Menkmen Style", Int) = 0
       [Toggle]
     _UseAlbedo("Use Albedo in RM Style", Int) = 0
     _NoisePara01("Noise Para01", Range(0,1)) = 0.2
     _NoisePara02("Noise Para02", Range(0.1,5)) = 2
     _NoisePara03("Noise Para03", Range(0,20)) = 0
     [Toggle]
      _UseBugFixValue("Use Bug Fix Value for Rainbow Shader", Int)=0
 }

 SubShader {
     Tags { "Queue" = "Transparent"   "RenderType"="Opaque" }
     LOD 200
     
     CGPROGRAM
     // Physically based Standard lighting model, and enable shadows on all light types
     #pragma surface surf Standard fullforwardshadows finalcolor:mycolor vertex:vert alpha
     #include "ClassicNoise3D.hlsl"
     // Use shader model 3.0 target, to get nicer looking lighting
     #pragma target 3.0

     sampler2D _MainTex;


    struct Input {
        float3 viewDir;
        float3 worldPos;
        float3 localPos;
        float2 uv_MainTex;
        float4 screenPos;
        half fog;
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
      int _UseAlbedo;
      int _UseBugFixValue;
       fixed4 _FogColor;


    uniform half4 unity_FogStart;
    uniform half4 unity_FogEnd;
    
     UNITY_INSTANCING_BUFFER_START(Props)
     UNITY_INSTANCING_BUFFER_END(Props)

     //One line randome function! Dope!
    float nrand(float2 uv){return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);}

    void vert (inout appdata_full v, out Input o) {       
        UNITY_INITIALIZE_OUTPUT(Input,o);
     
        v.vertex += (cnoise(v.vertex.xyz + _Time.yyy * _ShakeFreq) - 0.5) * _Shake * v.normal.xyzx;
        v.vertex +=  (cnoise(v.vertex.xyz * _NoisePara03) - 0.5) * _RandomMult;

        o.localPos = v.vertex.xyz;


        float pos = length(UnityObjectToViewPos(v.vertex).xyz);
      float diff = unity_FogEnd.x - unity_FogStart.x;
      float invDiff = 1.0f / diff;
      o.fog = clamp ((unity_FogEnd.x - pos) * invDiff, 0.0, 1.0);


    }

   
    void mycolor (Input IN, SurfaceOutputStandard o, inout fixed4 color){


        float bugFixValue = 0.9;

        if(_UseBugFixValue){
            bugFixValue = 200;
        }

        if(_NormalRainbow > 0){     
            color = fixed4((IN.localPos.xyz + 0.5)/bugFixValue,1);
        }

        if(_RMStyle > 0) {
            float a = IN.uv_MainTex.x;
           // float b = step(a, 0.1);
            if( (a * 99999) % 2 > 0.5){
               a = 1;
            }else{
               a = 0;
            }

            if(cnoise(IN.worldPos.xyz*_NoisePara02) > _NoisePara01){
                a = 0;
            }

            float3 c = float3(a,a,a);
            float mult = 0.3;
            if(_UseAlbedo > 0){
               color = fixed4(c + color.rgb*mult - float3(0.1,0.1,0.1), 1);
            }else{
               color = fixed4(a,a,a,1);
            }

           UNITY_APPLY_FOG_COLOR(IN.fog, color, unity_FogColor);
       

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
