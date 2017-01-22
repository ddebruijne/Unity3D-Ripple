// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/BallShader"
{
	Properties
	{
		_EdgeColor("Edge Color", Color) = (1,1,1,1)
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

		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
		float3 normal : NORMAL;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
		float3 normal : NORMAL;
		float3 viewDir : TEXCOORD1;
	};

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.uv;
		o.normal = UnityObjectToWorldNormal(v.normal);
		o.viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz);
		return o;
	}

	float4 _EdgeColor;

	fixed4 frag(v2f i) : SV_Target
	{
		float NdotV = dot(i.normal, i.viewDir);
		return _EdgeColor * NdotV;
	}
			ENDCG
		}
	}
}
