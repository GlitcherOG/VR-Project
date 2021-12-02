// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Water"
{
	Properties
	{
		_TopLayer("Top Layer", 2D) = "white" {}
		_TopLayerUnder("Top Layer Under", 2D) = "white" {}
		_MidLayer("Mid Layer", 2D) = "white" {}
		_MidlayerTint("Midlayer Tint", Color) = (0.05882353,0.4941176,0.4823529,0)
		_BGColour("BG Colour", Color) = (0.05882353,0.4941176,0.4823529,0)
		_FoamTexture("Foam Texture", 2D) = "white" {}
		_FoamColour("Foam Colour", Color) = (0.03137255,0.2588235,0.3176471,1)
		_FoamIntensity("Foam Intensity", Range( 0 , 1)) = 0.2
		_Depth("Depth", Range( 0 , 10)) = 0.2
		_DeepWaterColour("Deep Water Colour", Color) = (0.05882353,0.4941176,0.4823529,0)
		_TopLayerSpeed("Top Layer Speed", Range( -1 , 1)) = 0
		_TopUnderLayerSpeed("Top Under Layer Speed", Range( -1 , 1)) = 0
		_MidLayerSpeed("Mid Layer Speed", Range( -1 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _TopLayer;
		uniform float _TopLayerSpeed;
		uniform float4 _TopLayer_ST;
		uniform sampler2D _TopLayerUnder;
		uniform float _TopUnderLayerSpeed;
		uniform float4 _TopLayerUnder_ST;
		uniform sampler2D _MidLayer;
		uniform float _MidLayerSpeed;
		uniform float4 _MidLayer_ST;
		uniform float4 _MidlayerTint;
		uniform float4 _DeepWaterColour;
		uniform float4 _BGColour;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _Depth;
		uniform float4 _FoamColour;
		uniform float _FoamIntensity;
		uniform sampler2D _FoamTexture;
		uniform float4 _FoamTexture_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 appendResult48 = (float2(_SinTime.y , _SinTime.x));
			float2 uv_TopLayer = i.uv_texcoord * _TopLayer_ST.xy + _TopLayer_ST.zw;
			float2 panner37 = ( 1.0 * _Time.y * ( (-0.1 + (_TopLayerSpeed - -1.0) * (0.1 - -0.1) / (1.0 - -1.0)) * appendResult48 ) + uv_TopLayer);
			float2 appendResult54 = (float2(_SinTime.x , _SinTime.y));
			float2 uv_TopLayerUnder = i.uv_texcoord * _TopLayerUnder_ST.xy + _TopLayerUnder_ST.zw;
			float2 panner57 = ( 0.5 * _Time.y * ( (-0.1 + (_TopUnderLayerSpeed - -1.0) * (0.1 - -0.1) / (1.0 - -1.0)) * appendResult54 ) + uv_TopLayerUnder);
			float clampResult67 = clamp( ( tex2D( _TopLayer, panner37 ).a + ( tex2D( _TopLayerUnder, panner57 ).a * 0.2 ) ) , 0.0 , 1.0 );
			float2 appendResult73 = (float2(_SinTime.x , _SinTime.y));
			float2 uv_MidLayer = i.uv_texcoord * _MidLayer_ST.xy + _MidLayer_ST.zw;
			float2 panner76 = ( 0.3 * _Time.y * ( (-0.1 + (_MidLayerSpeed - -1.0) * (0.1 - -0.1) / (1.0 - -1.0)) * appendResult73 ) + uv_MidLayer);
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth82 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth82 = abs( ( screenDepth82 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _Depth ) );
			float clampResult83 = clamp( distanceDepth82 , 0.0 , 1.0 );
			float4 lerpResult84 = lerp( _DeepWaterColour , _BGColour , ( 1.0 - clampResult83 ));
			o.Albedo = ( clampResult67 + ( tex2D( _MidLayer, panner76 ).a * float4( 0.4666667,0.4666667,0.4666667,0 ) * _MidlayerTint ) + lerpResult84 ).rgb;
			float screenDepth10 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth10 = abs( ( screenDepth10 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _FoamIntensity ) );
			float clampResult11 = clamp( distanceDepth10 , 0.0 , 1.0 );
			float4 lerpResult13 = lerp( _FoamColour , float4( 0,0,0,0 ) , clampResult11);
			float2 uv_FoamTexture = i.uv_texcoord * _FoamTexture_ST.xy + _FoamTexture_ST.zw;
			float2 panner28 = ( 1.0 * _Time.y * float2( 0.1,0.1 ) + uv_FoamTexture);
			float4 Intersect14 = ( lerpResult13 * float4( 1,1,1,0 ) * tex2D( _FoamTexture, panner28 ).a );
			o.Emission = Intersect14.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18921
2005;158;1148;736;3800.261;158.2667;1.3;True;False
Node;AmplifyShaderEditor.RangedFloatNode;88;-3344.267,-62.71513;Inherit;False;Property;_TopUnderLayerSpeed;Top Under Layer Speed;11;0;Create;True;0;0;0;False;0;False;0;-0.006;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinTimeNode;47;-3655.894,66.12098;Inherit;True;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;91;-3008.435,-54.3249;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;-0.1;False;4;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;93;-3145.462,-178.1665;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;92;-3125.961,-165.1665;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;71;-2818.275,-190.7282;Inherit;True;Property;_TopLayerUnder;Top Layer Under;1;0;Create;True;0;0;0;False;0;False;None;ae73e54b8edfdb7499d386ebc5a316cc;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.RangedFloatNode;87;-2990.287,-462.5774;Inherit;False;Property;_TopLayerSpeed;Top Layer Speed;10;0;Create;True;0;0;0;False;0;False;0;0.01;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;54;-2737.228,85.28146;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;94;-2802.263,32.43353;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;29;-2073.436,1840.998;Inherit;True;Property;_FoamTexture;Foam Texture;5;0;Create;True;0;0;0;False;0;False;None;f80c573281d2d7b429c7b6724fda690a;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.RangedFloatNode;89;-3191.205,505.9968;Inherit;False;Property;_MidLayerSpeed;Mid Layer Speed;12;0;Create;True;0;0;0;False;0;False;0;0.01;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;-2516.499,15.66526;Inherit;False;2;2;0;FLOAT;0.001;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;48;-2662.006,-291.7754;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCRemapNode;90;-2704.691,-467.9917;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;-0.1;False;4;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;35;-2758.213,-665.7501;Inherit;True;Property;_TopLayer;Top Layer;0;0;Create;True;0;0;0;False;0;False;None;ae73e54b8edfdb7499d386ebc5a316cc;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.TextureCoordinatesNode;70;-2574.364,-102.3693;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;9;-1926.274,1677.781;Float;False;Property;_FoamIntensity;Foam Intensity;7;0;Create;True;0;0;0;False;0;False;0.2;0.638;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;81;-2303.036,986.5114;Float;False;Property;_Depth;Depth;8;0;Create;True;0;0;0;False;0;False;0.2;2.99;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;73;-2842.527,401.769;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DepthFade;10;-1620.274,1678.781;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;72;-2832.889,180.7707;Inherit;True;Property;_MidLayer;Mid Layer;2;0;Create;True;0;0;0;False;0;False;None;f80c573281d2d7b429c7b6724fda690a;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.PannerNode;57;-2298.676,-105.8901;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.1,0.1;False;1;FLOAT;0.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;36;-2480.569,-559.5057;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;27;-1837.092,1918.242;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;-2473.435,-425.6524;Inherit;True;2;2;0;FLOAT;1E-05;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCRemapNode;95;-2882.461,507.3338;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;-0.1;False;4;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-2517.026,397.0692;Inherit;False;2;2;0;FLOAT;1E-05;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ClampOpNode;11;-1363.274,1630.781;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;12;-1438.11,1451.329;Float;False;Property;_FoamColour;Foam Colour;6;0;Create;True;0;0;0;False;0;False;0.03137255,0.2588235,0.3176471,1;0.1086686,0.6226415,0.6073877,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DepthFade;82;-1995.188,962.6693;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;37;-2241.941,-551.3414;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.1,0.1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;28;-1596.134,1929.771;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.1,0.1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;58;-2073.054,-184.4167;Inherit;True;Property;_TextureSample0;Texture Sample 0;6;0;Create;True;0;0;0;False;0;False;-1;None;ae73e54b8edfdb7499d386ebc5a316cc;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;74;-2587.624,258.7849;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;30;-1973.316,-657.8947;Inherit;True;Property;_TextureSample2;Texture Sample 2;5;0;Create;True;0;0;0;False;0;False;-1;None;ae73e54b8edfdb7499d386ebc5a316cc;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;83;-1723.188,915.6693;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;26;-1350.567,1857.138;Inherit;True;Property;_TextureSample1;Texture Sample 1;4;0;Create;True;0;0;0;False;0;False;-1;None;c2d300948c3fc9848bfdb6dc24f3f315;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;-1722.923,-119.016;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;76;-2308.719,267.0275;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.1,0.1;False;1;FLOAT;0.3;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;13;-1176.264,1458.933;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;80;-2018.773,369.1261;Inherit;False;Property;_MidlayerTint;Midlayer Tint;3;0;Create;True;0;0;0;False;0;False;0.05882353,0.4941176,0.4823529,0;0,0.2735849,0.2672225,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;77;-2101.881,168.959;Inherit;True;Property;_TextureSample3;Texture Sample 3;6;0;Create;True;0;0;0;False;0;False;-1;None;ae73e54b8edfdb7499d386ebc5a316cc;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;85;-1608.435,516.4573;Inherit;False;Property;_DeepWaterColour;Deep Water Colour;9;0;Create;True;0;0;0;False;0;False;0.05882353,0.4941176,0.4823529,0;0,0.05956436,0.064,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;66;-1460.514,-214.2151;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-988.3446,1542.983;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;1,1,1,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;86;-1537.692,907.7339;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;53;-1616.143,703.5679;Inherit;False;Property;_BGColour;BG Colour;4;0;Create;True;0;0;0;False;0;False;0.05882353,0.4941176,0.4823529,0;0.05882353,0.4941176,0.4823529,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;84;-1313.178,755.8213;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;67;-1123.985,-57.75272;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;14;-771.6985,1524.2;Float;False;Intersect;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;-1666.639,236.2192;Inherit;False;3;3;0;FLOAT;0.5;False;1;COLOR;0.4666667,0.4666667,0.4666667,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;33;-875.8926,39.01447;Inherit;True;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;16;-158.7818,426.2495;Inherit;False;14;Intersect;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;236.7919,294.7878;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;91;0;88;0
WireConnection;93;0;47;1
WireConnection;92;0;47;2
WireConnection;54;0;47;1
WireConnection;54;1;47;2
WireConnection;94;0;91;0
WireConnection;55;0;94;0
WireConnection;55;1;54;0
WireConnection;48;0;92;0
WireConnection;48;1;93;0
WireConnection;90;0;87;0
WireConnection;70;2;71;0
WireConnection;73;0;47;1
WireConnection;73;1;47;2
WireConnection;10;0;9;0
WireConnection;57;0;70;0
WireConnection;57;2;55;0
WireConnection;36;2;35;0
WireConnection;27;2;29;0
WireConnection;49;0;90;0
WireConnection;49;1;48;0
WireConnection;95;0;89;0
WireConnection;75;0;95;0
WireConnection;75;1;73;0
WireConnection;11;0;10;0
WireConnection;82;0;81;0
WireConnection;37;0;36;0
WireConnection;37;2;49;0
WireConnection;28;0;27;0
WireConnection;58;0;71;0
WireConnection;58;1;57;0
WireConnection;74;2;72;0
WireConnection;30;0;35;0
WireConnection;30;1;37;0
WireConnection;83;0;82;0
WireConnection;26;0;29;0
WireConnection;26;1;28;0
WireConnection;64;0;58;4
WireConnection;76;0;74;0
WireConnection;76;2;75;0
WireConnection;13;0;12;0
WireConnection;13;2;11;0
WireConnection;77;0;72;0
WireConnection;77;1;76;0
WireConnection;66;0;30;4
WireConnection;66;1;64;0
WireConnection;18;0;13;0
WireConnection;18;2;26;4
WireConnection;86;0;83;0
WireConnection;84;0;85;0
WireConnection;84;1;53;0
WireConnection;84;2;86;0
WireConnection;67;0;66;0
WireConnection;14;0;18;0
WireConnection;78;0;77;4
WireConnection;78;2;80;0
WireConnection;33;0;67;0
WireConnection;33;1;78;0
WireConnection;33;2;84;0
WireConnection;0;0;33;0
WireConnection;0;2;16;0
ASEEND*/
//CHKSM=9C869C090D228B402C0B83385E075FED6AADF3AD