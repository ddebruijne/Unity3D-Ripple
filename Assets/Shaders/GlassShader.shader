Shader "Unlit/GlassShader"
{
	Properties
	{
		_MainTex("Color (RGB) Alpha (A)", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
	}
		SubShader{
			Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			LOD 200

			CGPROGRAM
			#pragma surface surf Lambert alpha

			fixed4 _Color;
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};


		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 col = tex2D(_MainTex, IN.uv_MainTex) * _Color;

			o.Albedo = col.rgb;
			o.Emission = col.rgb; // * _Color.a;
			o.Alpha = col.a;
		}
		ENDCG
		}
			FallBack "Diffuse"
}
