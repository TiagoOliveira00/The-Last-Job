Shader "Unlit/Gota"
{
	Properties
	{

	_MainTex("Texture", 2D) = "white" {} // textura
	_TotalSquares("Total Squares", float) = 1//tamanho da gota. maior o numero menor o tamanho
	_T("Time", float) = 1 //tempo
	_Distortion("_Distortion", range(-5,5)) = 1 // distorção da gota, os valores variam entre -5 e 5 para conseguir ver a distorcion em direçóes diferentes 
	_Blur("_Blur", range(0,1)) = 1 // efeito condensação

	}
		SubShader
	{
		Tags { "Queue" = "Transparent" }
		// "Queue" = "Transparent"

		ZWrite Off// não escreve na profundidade(não fica a frente)
		Blend SrcAlpha
		OneMinusSrcAlpha
		Cull Off // permite que se consiga ver as gotas dos dois lados

		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

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
			float _TotalSquares;
			float _T;
			float _Distortion;
			float _Blur;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			float randNum(float2 p)
			{
				p = frac(p * float2 (123.45, 345.45));
				p += dot(p, p + 34.345);
				return frac(p.x * p.y);
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float time = fmod(_Time.y + _T,7200); //TEMPO

			//black screen
			 float4 col = 1;
			 
			 float2 boxDimention = float2(2, 1);

			 ///grid para as gotas
			 float2 uv = i.uv * _TotalSquares * boxDimention;

			 uv.y += time * 0.25;//coloca as gotas a descer com o mesma velocidade

			 float2 reproduceSquare = frac(uv) - 0.5f; //reproduz os quadrados e coloca a origem no meio de cada um (-0.5)


			 //id de cada box
			 float2 boxId = floor(uv);
			 float n = randNum(boxId);//numero entre 0 e 1, noise
			 time += n * 6.2831;//vel das gotas de forma random

			 float wiggles = i.uv.y * 10;
			 float x = (n - 0.5) * 0.8;
			 x += (0.4 - abs(x)) * sin(3 * wiggles) * pow(sin(wiggles),6) * 0.45;

			 float y = -sin(time + sin(time + sin(time) * 0.5)) * 0.45;
			 //modifica o circulo, deslizando-o, saggy drop e faz com que não deslize totlamente na horizontal
			 y -= (reproduceSquare.x - x) * (reproduceSquare.x - x);

			 float2 dropPos = (reproduceSquare - float2 (x, y)) / boxDimention;//move a gota
			 float drop = smoothstep(0.05, 0.03, length(dropPos));//desenha a gota


			  float2 trailPos = (reproduceSquare - float2 (x, time * 0.25)) / boxDimention;
			  trailPos.y = (frac(trailPos.y * 8) - 0.5) / 8;
			  float trail = smoothstep(0.03, 0.01, length(trailPos));
			  float fogTrail = smoothstep(-0.5,0.5,dropPos.y);
			  fogTrail *= smoothstep(0.5,y, reproduceSquare.y);

			  trail *= fogTrail;
			  fogTrail *= smoothstep(-0.5, 0.4, abs(dropPos.x));

			  //col += fogTrail * 0.5;//cria o efeito de fog
			  col += trail;
			  col += drop;

			 float2 offset = drop * dropPos + trail * trailPos;//cria um pequeno offset para a gota

			  if (reproduceSquare.x > 0.48 || reproduceSquare.y > 0.49)
			{
				  col = float4 (1, 0, 0, 1); 
			}


			  float blur = _Blur * 7 * fogTrail;
			  col = tex2Dlod(_MainTex, float4(i.uv + offset * _Distortion,0,blur));
			  col.a = 0.3f;// altera  o valor de alpha da textura para a tornar transparente
			  return col;
		  }
		  ENDCG
	  }
	}
}