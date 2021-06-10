Shader "Custom/DistortionFlow" {
	Properties {

		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("MainTex", 2D) = "white" {} //main tex
		 _FlowMap ("Flow Map", 2D) = "black" {}
		 _DerivHeightMap ("_HeightMap", 2D) = "black" {}//mapa de alturas		
		_Tiling("Tiling", Float) = 1 // propriedade para a textura
		_Speed("Speed", Float) = 1// propriedade da velocidade
		_WaveFlow("Wave Flow", Float) = 1 // propriedade que faz o efeito de ondas		

		_HeightScaleModulated("Height Scale", Float) = 0.75 //  propriedade que torna possível dimensionar corretamente a altura das ondas, escala a altura com base na velocidade de fluxo
		_Shininess("Smoothness", Range(0,1)) = 1
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows
			#pragma target 3.0

			#include "Flow.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _FlowMap;
			uniform sampler2D _DerivHeightMap;
			uniform float  _Tiling;
			uniform float _Speed;
			uniform float _WaveFlow;
			uniform float  _HeightScaleModulated;
			uniform float _Shininess;

			struct Input 
			{
			float2 uv_MainTex;
			};

		
		fixed4 _Color;

		//coloca os dados corretos em  vetores flutuantes e obtem as derivadas
		float3 Height (float4 textureData) {
			float3 derivada = textureData.agb;
			derivada.xy = derivada.xy * 2 - 1;
			return derivada;
		}

		void surf (Input inside, inout SurfaceOutputStandard o)
		{

			float3 flow = tex2D(_FlowMap, inside.uv_MainTex);//aplica a textura  do mapa de fluxo
			flow.xy = flow.xy * 2 - 1;
			flow *= _WaveFlow; // altera a velocidade do fluxo da waveFlow
			float noise = tex2D(_FlowMap, inside.uv_MainTex).a;//noise
			float time = _Time.y * _Speed + noise;//animaçáo
			
			//mistura textura ooriginal sem distorções em vez de colocar somente preto nos sitios sem fluxo
			//isto exige que amostremos a textura duas vezes, cada uma com dados UVW diferentes.
			//isto permite não perder o efeito de ilusão
			float3 uvA = FlowUVW(inside.uv_MainTex, flow.xy,0,0, _Tiling, time, false);
			float3 uvB = FlowUVW(inside.uv_MainTex, flow.xy,0,0, _Tiling, time, true);

			float finalHeightScale = flow.z * _HeightScaleModulated; //obtem a escala final para as derivadas mais a altura.

			//  converter em derivadas e normaliza
			//as derivadas são calculadas  altura em 0 e 1.
			float3 dhA = Height(tex2D(_DerivHeightMap, uvA.xy)) *(uvA.z * finalHeightScale);
			float3 dhB = Height(tex2D(_DerivHeightMap, uvB.xy)) *(uvB.z * finalHeightScale);
			o.Normal = normalize(float3(-(dhA.xy + dhB.xy), 1));

			fixed4 texA = tex2D(_MainTex, uvA.xy) * uvA.z;
			fixed4 texB = tex2D(_MainTex, uvB.xy) * uvB.z;


			fixed4 color = (texA + texB) * _Color;
			o.Albedo = color;			
			o.Smoothness = _Shininess;
			o.Alpha = color.a;
		}
		ENDCG
	}
}