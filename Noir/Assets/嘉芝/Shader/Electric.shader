// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:Dissolve,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2393-OUT,alpha-3816-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32334,y:32602,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0b4388d5607631f44bf894cae76da768,ntxv:0,isnm:False|UVIN-2167-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32530,y:32602,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-797-RGB,D-8823-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32334,y:32773,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32334,y:32931,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3160377,c2:0.8086352,c3:1,c4:1;n:type:ShaderForge.SFN_Append,id:3265,x:30526,y:32621,varname:node_3265,prsc:2|A-1056-OUT,B-9035-OUT;n:type:ShaderForge.SFN_Time,id:4702,x:30528,y:32792,varname:node_4702,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:1056,x:30336,y:32587,ptovrint:False,ptlb:U speed,ptin:_Uspeed,varname:node_1056,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_ValueProperty,id:9035,x:30336,y:32685,ptovrint:False,ptlb:V speed,ptin:_Vspeed,varname:_node_1056_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:8738,x:30719,y:32564,varname:node_8738,prsc:2|A-3265-OUT,B-4702-T;n:type:ShaderForge.SFN_Add,id:2344,x:30900,y:32584,varname:node_2344,prsc:2|A-8738-OUT,B-9072-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:9072,x:30719,y:32722,varname:node_9072,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2dAsset,id:1004,x:30900,y:32739,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_1004,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:541f518900e7879438c577e82fee8d2c,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9752,x:31092,y:32584,varname:node_9752,prsc:2,tex:541f518900e7879438c577e82fee8d2c,ntxv:0,isnm:False|UVIN-2344-OUT,TEX-1004-TEX;n:type:ShaderForge.SFN_Append,id:5225,x:30528,y:32942,varname:node_5225,prsc:2|A-7454-OUT,B-2703-OUT;n:type:ShaderForge.SFN_Time,id:5495,x:30528,y:33128,varname:node_5495,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:7454,x:30342,y:32942,ptovrint:False,ptlb:2U speed_copy,ptin:_2Uspeed_copy,varname:_Uspeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.2;n:type:ShaderForge.SFN_ValueProperty,id:2703,x:30342,y:33040,ptovrint:False,ptlb:2V speed_copy,ptin:_2Vspeed_copy,varname:_Vspeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.05;n:type:ShaderForge.SFN_Multiply,id:1693,x:30719,y:32921,varname:node_1693,prsc:2|A-5225-OUT,B-5495-T;n:type:ShaderForge.SFN_TexCoord,id:7363,x:30719,y:33079,varname:node_7363,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:5791,x:30913,y:32921,varname:node_5791,prsc:2|A-1693-OUT,B-7363-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:7201,x:31092,y:32823,varname:node_7201,prsc:2,tex:541f518900e7879438c577e82fee8d2c,ntxv:0,isnm:False|UVIN-5791-OUT,TEX-1004-TEX;n:type:ShaderForge.SFN_Slider,id:276,x:30317,y:32372,ptovrint:False,ptlb:Dissolve,ptin:_Dissolve,varname:node_276,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2,max:1;n:type:ShaderForge.SFN_OneMinus,id:2971,x:30683,y:32375,varname:node_2971,prsc:2|IN-276-OUT;n:type:ShaderForge.SFN_RemapRange,id:3856,x:30879,y:32385,varname:node_3856,prsc:2,frmn:0,frmx:1,tomn:-0.6,tomx:0.5|IN-2971-OUT;n:type:ShaderForge.SFN_Add,id:4535,x:31302,y:32584,varname:node_4535,prsc:2|A-3856-OUT,B-9752-R;n:type:ShaderForge.SFN_Add,id:7786,x:31302,y:32765,varname:node_7786,prsc:2|A-3856-OUT,B-7201-R;n:type:ShaderForge.SFN_Multiply,id:5626,x:31522,y:32584,varname:node_5626,prsc:2|A-4535-OUT,B-7786-OUT;n:type:ShaderForge.SFN_RemapRange,id:9315,x:31666,y:32584,varname:node_9315,prsc:2,frmn:0,frmx:1,tomn:-10,tomx:10|IN-5626-OUT;n:type:ShaderForge.SFN_Clamp01,id:7341,x:31811,y:32602,varname:node_7341,prsc:2|IN-9315-OUT;n:type:ShaderForge.SFN_OneMinus,id:3152,x:31964,y:32602,varname:node_3152,prsc:2|IN-7341-OUT;n:type:ShaderForge.SFN_Append,id:2167,x:32151,y:32602,varname:node_2167,prsc:2|A-3152-OUT,B-1613-OUT;n:type:ShaderForge.SFN_Vector1,id:1613,x:31964,y:32756,varname:node_1613,prsc:2,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:8823,x:32334,y:33144,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_8823,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:3816,x:32513,y:32423,varname:node_3816,prsc:2|A-6664-OUT,B-6074-R;n:type:ShaderForge.SFN_ValueProperty,id:6664,x:32312,y:32423,ptovrint:False,ptlb:Strench,ptin:_Strench,varname:_Opacity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.8;n:type:ShaderForge.SFN_ValueProperty,id:2495,x:32462,y:33272,ptovrint:False,ptlb:Opacity_copy,ptin:_Opacity_copy,varname:_Opacity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;proporder:6074-797-1056-9035-1004-276-7454-2703-8823-6664;pass:END;sub:END;*/

Shader "Shader Forge/Electric" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (0.3160377,0.8086352,1,1)
        _Uspeed ("U speed", Float ) = 0.2
        _Vspeed ("V speed", Float ) = 0.2
        _Noise ("Noise", 2D) = "white" {}
        _Dissolve ("Dissolve", Range(0, 1)) = 0.2
        _2Uspeed_copy ("2U speed_copy", Float ) = -0.2
        _2Vspeed_copy ("2V speed_copy", Float ) = 0.05
        _Opacity ("Opacity", Float ) = 2
        _Strench ("Strench", Float ) = 0.8
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform float _Uspeed;
            uniform float _Vspeed;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _2Uspeed_copy;
            uniform float _2Vspeed_copy;
            uniform float _Dissolve;
            uniform float _Opacity;
            uniform float _Strench;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float node_3856 = ((1.0 - _Dissolve)*1.1+-0.6);
                float4 node_4702 = _Time;
                float2 node_2344 = ((float2(_Uspeed,_Vspeed)*node_4702.g)+i.uv0);
                float4 node_9752 = tex2D(_Noise,TRANSFORM_TEX(node_2344, _Noise));
                float4 node_5495 = _Time;
                float2 node_5791 = ((float2(_2Uspeed_copy,_2Vspeed_copy)*node_5495.g)+i.uv0);
                float4 node_7201 = tex2D(_Noise,TRANSFORM_TEX(node_5791, _Noise));
                float2 node_2167 = float2((1.0 - saturate((((node_3856+node_9752.r)*(node_3856+node_7201.r))*20.0+-10.0))),0.0);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_2167, _MainTex));
                float3 emissive = (_MainTex_var.rgb*i.vertexColor.rgb*_TintColor.rgb*_Opacity);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_Strench*_MainTex_var.r));
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
