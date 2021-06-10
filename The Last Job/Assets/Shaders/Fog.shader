Shader "Unlit/Fog"
{
    Properties
    {
        
        _MainTex("Fog texture", 2D) = "white" {} // main texture nevoeiro
        _FogEffect("Fog Effect", 2D) = "white" {}
        _Color("Color", color) = (1, 1, 1, 1)
        _Speed("Speed", float) = 1.
        _Distance("Fading distance", Range(1., 10.)) = 1.
    }

        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            //determina como O GPU fica com o output do fragment para o  target render
               Blend SrcAlpha OneMinusSrcAlpha
           

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct v2f {
                    float4 pos : SV_POSITION;
                    float4 vertCol : COLOR0;// COLOR0;tem menos precisão no input do vertex shader
                    float2 uv : TEXCOORD0;
                    float2 uv2 : TEXCOORD1;
                };

                uniform sampler2D _MainTex;
                uniform float4 _MainTex_ST;
                uniform float _Distance;
                uniform sampler2D _FogEffect;
                uniform float _Speed;


                v2f vert(appdata_full v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                    o.uv2 = v.texcoord;
                    o.vertCol = v.color;
                    return o;
                }

             
                fixed4 _Color;

                fixed4 frag(v2f i) : SV_Target
                {
                    float2 uv = i.uv + _Speed * _Time.x;
                    fixed4 col = tex2D(_MainTex, uv) * _Color * i.vertCol;
                    col.a *= tex2D(_FogEffect, i.uv2);
                    col.a *= 1 - ((i.pos.z / i.pos.w) * _Distance);
                    return col;
                }
                ENDCG
            }
        }
}