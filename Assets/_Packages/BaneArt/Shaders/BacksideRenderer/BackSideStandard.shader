// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BackSideStandard"
{
	Properties
	{
		_DiffuseTint("Diffuse Tint", Color) = (1,1,1,0)
		_BackfaceDiffuseTint("Backface Diffuse Tint", Color) = (1,1,1,0)
		_Diffuse("Diffuse", 2D) = "white" {}
		_BackfaceDiffuse("Backface Diffuse", 2D) = "white" {}
		[Toggle]_EnableUniqueBackside("Enable Unique Backside", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			half ASEVFace : VFACE;
		};

		uniform float4 _DiffuseTint;
		uniform sampler2D _Diffuse;
		uniform float4 _Diffuse_ST;
		uniform float _EnableUniqueBackside;
		uniform float4 _BackfaceDiffuseTint;
		uniform sampler2D _BackfaceDiffuse;
		uniform float4 _BackfaceDiffuse_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Diffuse = i.uv_texcoord * _Diffuse_ST.xy + _Diffuse_ST.zw;
			float4 temp_output_2_0 = ( _DiffuseTint * tex2D( _Diffuse, uv_Diffuse ) );
			float2 uv_BackfaceDiffuse = i.uv_texcoord * _BackfaceDiffuse_ST.xy + _BackfaceDiffuse_ST.zw;
			float4 switchResult6 = (((i.ASEVFace>0)?(temp_output_2_0):((( _EnableUniqueBackside )?( ( _BackfaceDiffuseTint * tex2D( _BackfaceDiffuse, uv_BackfaceDiffuse ) ) ):( temp_output_2_0 )))));
			o.Albedo = switchResult6.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18921
1766;77;1353;776;1170.319;414.8956;1.341497;True;False
Node;AmplifyShaderEditor.SamplerNode;4;-820.5,-77;Inherit;True;Property;_Diffuse;Diffuse;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;1;-732.5,-253;Inherit;False;Property;_DiffuseTint;Diffuse Tint;0;0;Create;True;0;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;9;-749.2666,110.7;Inherit;False;Property;_BackfaceDiffuseTint;Backface Diffuse Tint;1;0;Create;True;0;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;8;-821.6667,284.1;Inherit;True;Property;_BackfaceDiffuse;Backface Diffuse;3;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-481.7001,-95.90003;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-486.7668,110.4999;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;10;-325.6407,6.982148;Inherit;False;Property;_EnableUniqueBackside;Enable Unique Backside;4;0;Create;True;0;0;0;False;0;False;0;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SwitchByFaceNode;6;-19.70871,-92.66601;Inherit;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;193.3497,-95.60006;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;BackSideStandard;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;0
WireConnection;2;1;4;0
WireConnection;7;0;9;0
WireConnection;7;1;8;0
WireConnection;10;0;2;0
WireConnection;10;1;7;0
WireConnection;6;0;2;0
WireConnection;6;1;10;0
WireConnection;0;0;6;0
ASEEND*/
//CHKSM=49E24FDCFC6E6D840F57B79F42418C0DE3CAA028