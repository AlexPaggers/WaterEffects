Shader "Custom/GPUWater" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_DispTex("DispTexture", 2D) = "gray" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_NormalMap("Normalmap", 2D) = "bump" {}
		_Displacement("Displacement", Range(0, 1.0)) = 0.3
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Tess("Tessellation", Range(1,32)) = 4

		_ScrollSpeedX("Scroll Speed X", float) = 0
		_ScrollSpeedY("Scroll Speed Y", float) = 0

		_noiseWavelength	("Noise Wavelength" , float)	= 0.5
		_noiseStrength		("Noise Strength", Range(0,1))		= 0.5
		_noiseSpeed			("Noise Speed", float)			= 1

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert tessellate:tessFixed

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex; 

		struct Input {
			float2 uv_Tess;
			float2 uv_MainTex;
			float2 uv_DispTex;

		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _noiseWavelength, _noiseStrength, _noiseSpeed;

		UNITY_INSTANCING_CBUFFER_START(Props)

		UNITY_INSTANCING_CBUFFER_END

		sampler2D _DispTex;
		float _Displacement;

		void vert(inout appdata_full v)
		{
			
			//Adding Tessellation to Displacement Texture 
			float d = tex2Dlod(_MainTex, float4(v.texcoord.xy, 0, 0)).r * _Displacement;
			v.vertex.xyz += v.normal * d;

			//Adding Sin Wave to noise
			float pX = (v.vertex.x * _noiseWavelength) + (_Time.w * _noiseSpeed);
			v.vertex.y += sin(pX) * _noiseStrength;


		}

		float _Tess;

		float4 tessFixed()
		{
			return _Tess;
		}


		void surf (Input IN, inout SurfaceOutputStandard o) {

			// Albedo comes from a texture tinted by color
			IN.uv_MainTex.x += _Time.x;
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;


			//IN._DispTex_ST.zw.x += _Time.x;

		}
		ENDCG
	}
	FallBack "Diffuse"
}