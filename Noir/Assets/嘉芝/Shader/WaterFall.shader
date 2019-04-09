// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5351104,fgcg:0.5780638,fgcb:0.6132076,fgca:0.1607843,fgde:0.1,fgrn:-30,fgrf:150,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:7882,x:32719,y:32712,varname:node_7882,prsc:2|diff-7257-RGB,emission-6528-OUT,alpha-6139-OUT,refract-7469-OUT,voffset-7018-OUT;n:type:ShaderForge.SFN_Color,id:7257,x:32490,y:32649,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7257,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:6139,x:32411,y:32869,varname:node_6139,prsc:2|A-6591-OUT,B-895-OUT;n:type:ShaderForge.SFN_Slider,id:6591,x:32064,y:32803,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_6591,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1709402,max:1;n:type:ShaderForge.SFN_Clamp01,id:895,x:32276,y:33006,varname:node_895,prsc:2|IN-8600-OUT;n:type:ShaderForge.SFN_Add,id:8600,x:32085,y:33006,varname:node_8600,prsc:2|A-5159-OUT,B-664-OUT;n:type:ShaderForge.SFN_Multiply,id:5159,x:31783,y:32942,varname:node_5159,prsc:2|A-764-OUT,B-1674-OUT;n:type:ShaderForge.SFN_Multiply,id:664,x:31783,y:33122,varname:node_664,prsc:2|A-6829-G,B-6130-OUT;n:type:ShaderForge.SFN_Add,id:764,x:31571,y:32942,varname:node_764,prsc:2|A-5133-OUT,B-9596-OUT;n:type:ShaderForge.SFN_Add,id:6130,x:31571,y:33122,varname:node_6130,prsc:2|A-6829-G,B-9074-A;n:type:ShaderForge.SFN_Add,id:6528,x:31817,y:32567,varname:node_6528,prsc:2|A-5133-OUT,B-326-OUT;n:type:ShaderForge.SFN_Vector1,id:1674,x:31571,y:33067,varname:node_1674,prsc:2,v1:2;n:type:ShaderForge.SFN_Clamp01,id:326,x:31571,y:33328,varname:node_326,prsc:2|IN-7589-OUT;n:type:ShaderForge.SFN_Add,id:7589,x:31378,y:33328,varname:node_7589,prsc:2|A-9596-OUT,B-1376-OUT;n:type:ShaderForge.SFN_Multiply,id:5133,x:31278,y:32569,varname:node_5133,prsc:2|A-37-V,B-8100-OUT,C-7907-OUT;n:type:ShaderForge.SFN_Multiply,id:7469,x:31582,y:32691,varname:node_7469,prsc:2|A-1174-OUT,B-4498-OUT,C-3799-OUT;n:type:ShaderForge.SFN_Vector1,id:7907,x:31041,y:32646,varname:node_7907,prsc:2,v1:3;n:type:ShaderForge.SFN_Slider,id:4498,x:31001,y:32916,ptovrint:False,ptlb:Reflaction,ptin:_Reflaction,varname:node_4498,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1931867,max:1;n:type:ShaderForge.SFN_Multiply,id:9596,x:31099,y:33326,varname:node_9596,prsc:2|A-6987-OUT,B-7195-R,C-6898-OUT;n:type:ShaderForge.SFN_Multiply,id:6987,x:30883,y:33326,varname:node_6987,prsc:2|A-3392-OUT,B-5546-OUT;n:type:ShaderForge.SFN_Clamp01,id:3392,x:30671,y:33326,varname:node_3392,prsc:2|IN-1187-OUT;n:type:ShaderForge.SFN_Clamp01,id:1376,x:31279,y:33664,varname:node_1376,prsc:2|IN-3903-OUT;n:type:ShaderForge.SFN_Multiply,id:1187,x:30460,y:33326,varname:node_1187,prsc:2|A-1017-B,B-7410-B;n:type:ShaderForge.SFN_Vector1,id:6898,x:30883,y:33469,varname:node_6898,prsc:2,v1:2;n:type:ShaderForge.SFN_Add,id:3903,x:31106,y:33664,varname:node_3903,prsc:2|A-2891-OUT,B-6058-OUT,C-8820-OUT,D-3473-OUT;n:type:ShaderForge.SFN_Clamp01,id:2891,x:30671,y:33541,varname:node_2891,prsc:2|IN-1866-OUT;n:type:ShaderForge.SFN_RemapRange,id:1866,x:30460,y:33541,varname:node_1866,prsc:2,frmn:0,frmx:1,tomn:1,tomx:-1.8|IN-7494-U;n:type:ShaderForge.SFN_Clamp01,id:6058,x:30671,y:33709,varname:node_6058,prsc:2|IN-7059-OUT;n:type:ShaderForge.SFN_RemapRange,id:7059,x:30460,y:33709,varname:node_7059,prsc:2,frmn:0,frmx:1,tomn:-1.3,tomx:1|IN-7494-U;n:type:ShaderForge.SFN_Clamp01,id:8820,x:30858,y:33891,varname:node_8820,prsc:2|IN-6500-OUT;n:type:ShaderForge.SFN_RemapRange,id:6500,x:30671,y:33891,varname:node_6500,prsc:2,frmn:0,frmx:2,tomn:-0.75,tomx:2|IN-5546-OUT;n:type:ShaderForge.SFN_OneMinus,id:5546,x:30460,y:33908,varname:node_5546,prsc:2|IN-7494-V;n:type:ShaderForge.SFN_Clamp01,id:3473,x:30858,y:34096,varname:node_3473,prsc:2|IN-1573-OUT;n:type:ShaderForge.SFN_RemapRange,id:1573,x:30671,y:34078,varname:node_1573,prsc:2,frmn:0,frmx:1,tomn:-1.5,tomx:0.75|IN-7494-V;n:type:ShaderForge.SFN_Clamp01,id:1174,x:30881,y:32743,varname:node_1174,prsc:2|IN-5127-OUT;n:type:ShaderForge.SFN_Multiply,id:5127,x:30657,y:32743,varname:node_5127,prsc:2|A-3439-OUT,B-6829-G,C-37-V;n:type:ShaderForge.SFN_Append,id:3439,x:30432,y:32743,varname:node_3439,prsc:2|A-7036-R,B-5795-R;n:type:ShaderForge.SFN_Multiply,id:7018,x:30892,y:33023,varname:node_7018,prsc:2|A-8100-OUT,B-6174-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6174,x:30670,y:33067,ptovrint:False,ptlb:VertexOffset,ptin:_VertexOffset,varname:node_6174,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.25;n:type:ShaderForge.SFN_Multiply,id:8100,x:30427,y:33016,varname:node_8100,prsc:2|A-1017-R,B-7410-R,C-6829-G;n:type:ShaderForge.SFN_TexCoord,id:37,x:30171,y:32332,varname:node_37,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:7036,x:30149,y:32556,varname:node_7036,prsc:2,tex:7099ab5875dc39949832926a57f94897,ntxv:0,isnm:False|UVIN-7408-UVOUT,TEX-902-TEX;n:type:ShaderForge.SFN_Tex2d,id:5795,x:30160,y:32781,varname:node_5795,prsc:2,tex:7099ab5875dc39949832926a57f94897,ntxv:0,isnm:False|UVIN-2283-UVOUT,TEX-902-TEX;n:type:ShaderForge.SFN_Panner,id:7408,x:29922,y:32556,varname:node_7408,prsc:2,spu:-0.2,spv:2|UVIN-2453-UVOUT,DIST-499-OUT;n:type:ShaderForge.SFN_Panner,id:2283,x:29922,y:32781,varname:node_2283,prsc:2,spu:0.2,spv:2|UVIN-2453-UVOUT,DIST-499-OUT;n:type:ShaderForge.SFN_Tex2d,id:6829,x:30160,y:32980,varname:node_6829,prsc:2,tex:7099ab5875dc39949832926a57f94897,ntxv:0,isnm:False|UVIN-5132-OUT,TEX-902-TEX;n:type:ShaderForge.SFN_Panner,id:2571,x:29916,y:33167,varname:node_2571,prsc:2,spu:0,spv:1|UVIN-5132-OUT,DIST-499-OUT;n:type:ShaderForge.SFN_Tex2d,id:1017,x:30171,y:33354,varname:node_1017,prsc:2,tex:7099ab5875dc39949832926a57f94897,ntxv:0,isnm:False|UVIN-6970-UVOUT,TEX-902-TEX;n:type:ShaderForge.SFN_Panner,id:6970,x:29916,y:33354,varname:node_6970,prsc:2,spu:-0.25,spv:1|UVIN-2453-UVOUT,DIST-499-OUT;n:type:ShaderForge.SFN_Tex2d,id:7410,x:30171,y:33563,varname:node_7410,prsc:2,tex:7099ab5875dc39949832926a57f94897,ntxv:0,isnm:False|UVIN-1640-UVOUT,TEX-902-TEX;n:type:ShaderForge.SFN_Panner,id:1640,x:29916,y:33563,varname:node_1640,prsc:2,spu:-0.25,spv:2|UVIN-2453-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:7494,x:30171,y:33741,varname:node_7494,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:9074,x:30143,y:33167,varname:node_9074,prsc:2,tex:7099ab5875dc39949832926a57f94897,ntxv:0,isnm:False|UVIN-2571-UVOUT,TEX-902-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:902,x:29541,y:32343,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_902,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7099ab5875dc39949832926a57f94897,ntxv:2,isnm:False;n:type:ShaderForge.SFN_ValueProperty,id:3918,x:29541,y:32542,ptovrint:False,ptlb:WaterSplash,ptin:_WaterSplash,varname:node_3918,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Multiply,id:499,x:29711,y:32556,varname:node_499,prsc:2|A-3918-OUT,B-6200-TSL;n:type:ShaderForge.SFN_Time,id:6200,x:29541,y:32603,varname:node_6200,prsc:2;n:type:ShaderForge.SFN_Add,id:5132,x:29503,y:32968,varname:node_5132,prsc:2|A-485-UVOUT,B-2777-OUT;n:type:ShaderForge.SFN_TexCoord,id:2453,x:29611,y:33156,varname:node_2453,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:2777,x:29302,y:33004,varname:node_2777,prsc:2|A-7195-R,B-387-OUT;n:type:ShaderForge.SFN_Tex2d,id:7195,x:29054,y:32742,ptovrint:False,ptlb:FlowMask,ptin:_FlowMask,varname:node_7195,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0d53d82c7b9b95b4abec37ca8a6d1bb7,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Append,id:8352,x:28889,y:33008,varname:node_8352,prsc:2|A-9985-R,B-5088-G;n:type:ShaderForge.SFN_Multiply,id:387,x:29103,y:33028,varname:node_387,prsc:2|A-8352-OUT,B-6194-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6194,x:28880,y:33177,ptovrint:False,ptlb:FlowStrength,ptin:_FlowStrength,varname:node_6194,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_Tex2d,id:9985,x:28639,y:32929,varname:node_9985,prsc:2,tex:1ee6b8061afb9a54d81f33d4b46a4dd6,ntxv:0,isnm:False|UVIN-5503-UVOUT,TEX-5491-TEX;n:type:ShaderForge.SFN_Tex2d,id:5088,x:28643,y:33111,varname:node_5088,prsc:2,tex:1ee6b8061afb9a54d81f33d4b46a4dd6,ntxv:0,isnm:False|UVIN-3365-UVOUT,TEX-5491-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:5491,x:28089,y:32758,ptovrint:False,ptlb:FlowMap,ptin:_FlowMap,varname:node_5491,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1ee6b8061afb9a54d81f33d4b46a4dd6,ntxv:2,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:485,x:28089,y:32918,varname:node_485,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:6590,x:28089,y:33136,varname:node_6590,prsc:2|A-5288-OUT,B-4728-TSL;n:type:ShaderForge.SFN_ValueProperty,id:5288,x:27843,y:33005,ptovrint:False,ptlb:FlowMapSpeed,ptin:_FlowMapSpeed,varname:node_5288,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Time,id:4728,x:27843,y:33136,varname:node_4728,prsc:2;n:type:ShaderForge.SFN_Panner,id:5503,x:28367,y:32940,varname:node_5503,prsc:2,spu:0.2,spv:1.5|UVIN-485-UVOUT,DIST-6590-OUT;n:type:ShaderForge.SFN_Panner,id:3365,x:28367,y:33132,varname:node_3365,prsc:2,spu:-0.2,spv:1|UVIN-485-UVOUT,DIST-6590-OUT;n:type:ShaderForge.SFN_ComponentMask,id:3799,x:31250,y:32754,varname:node_3799,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-7036-RGB;proporder:7257-6591-6174-902-3918-7195-6194-5491-5288-4498;pass:END;sub:END;*/

Shader "Custom/WaterFall" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _Opacity ("Opacity", Range(0, 1)) = 0.1709402
        _VertexOffset ("VertexOffset", Float ) = 0.25
        _MainTex ("MainTex", 2D) = "black" {}
        _WaterSplash ("WaterSplash", Float ) = 3
        _FlowMask ("FlowMask", 2D) = "white" {}
        _FlowStrength ("FlowStrength", Float ) = 0.1
        _FlowMap ("FlowMap", 2D) = "black" {}
        _FlowMapSpeed ("FlowMapSpeed", Float ) = 2
        _Reflaction ("Reflaction", Range(0, 1)) = 0.1931867
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        GrabPass{ }
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
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _GrabTexture;
            uniform float4 _Color;
            uniform float _Opacity;
            uniform float _Reflaction;
            uniform float _VertexOffset;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _WaterSplash;
            uniform sampler2D _FlowMask; uniform float4 _FlowMask_ST;
            uniform float _FlowStrength;
            uniform sampler2D _FlowMap; uniform float4 _FlowMap_ST;
            uniform float _FlowMapSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_6200 = _Time;
                float node_499 = (_WaterSplash*node_6200.r);
                float2 node_6970 = (o.uv0+node_499*float2(-0.25,1));
                float4 node_1017 = tex2Dlod(_MainTex,float4(TRANSFORM_TEX(node_6970, _MainTex),0.0,0));
                float4 node_5553 = _Time;
                float2 node_1640 = (o.uv0+node_5553.g*float2(-0.25,2));
                float4 node_7410 = tex2Dlod(_MainTex,float4(TRANSFORM_TEX(node_1640, _MainTex),0.0,0));
                float4 _FlowMask_var = tex2Dlod(_FlowMask,float4(TRANSFORM_TEX(o.uv0, _FlowMask),0.0,0));
                float4 node_4728 = _Time;
                float node_6590 = (_FlowMapSpeed*node_4728.r);
                float2 node_5503 = (o.uv0+node_6590*float2(0.2,1.5));
                float4 node_9985 = tex2Dlod(_FlowMap,float4(TRANSFORM_TEX(node_5503, _FlowMap),0.0,0));
                float2 node_3365 = (o.uv0+node_6590*float2(-0.2,1));
                float4 node_5088 = tex2Dlod(_FlowMap,float4(TRANSFORM_TEX(node_3365, _FlowMap),0.0,0));
                float2 node_5132 = (o.uv0+(_FlowMask_var.r*(float2(node_9985.r,node_5088.g)*_FlowStrength)));
                float4 node_6829 = tex2Dlod(_MainTex,float4(TRANSFORM_TEX(node_5132, _MainTex),0.0,0));
                float node_8100 = (node_1017.r*node_7410.r*node_6829.g);
                float node_7018 = (node_8100*_VertexOffset);
                v.vertex.xyz += float3(node_7018,node_7018,node_7018);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float4 node_6200 = _Time;
                float node_499 = (_WaterSplash*node_6200.r);
                float2 node_7408 = (i.uv0+node_499*float2(-0.2,2));
                float4 node_7036 = tex2D(_MainTex,TRANSFORM_TEX(node_7408, _MainTex));
                float2 node_2283 = (i.uv0+node_499*float2(0.2,2));
                float4 node_5795 = tex2D(_MainTex,TRANSFORM_TEX(node_2283, _MainTex));
                float4 _FlowMask_var = tex2D(_FlowMask,TRANSFORM_TEX(i.uv0, _FlowMask));
                float4 node_4728 = _Time;
                float node_6590 = (_FlowMapSpeed*node_4728.r);
                float2 node_5503 = (i.uv0+node_6590*float2(0.2,1.5));
                float4 node_9985 = tex2D(_FlowMap,TRANSFORM_TEX(node_5503, _FlowMap));
                float2 node_3365 = (i.uv0+node_6590*float2(-0.2,1));
                float4 node_5088 = tex2D(_FlowMap,TRANSFORM_TEX(node_3365, _FlowMap));
                float2 node_5132 = (i.uv0+(_FlowMask_var.r*(float2(node_9985.r,node_5088.g)*_FlowStrength)));
                float4 node_6829 = tex2D(_MainTex,TRANSFORM_TEX(node_5132, _MainTex));
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + (saturate((float2(node_7036.r,node_5795.r)*node_6829.g*i.uv0.g))*_Reflaction*node_7036.rgb.rg);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuseColor = _Color.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float2 node_6970 = (i.uv0+node_499*float2(-0.25,1));
                float4 node_1017 = tex2D(_MainTex,TRANSFORM_TEX(node_6970, _MainTex));
                float4 node_5553 = _Time;
                float2 node_1640 = (i.uv0+node_5553.g*float2(-0.25,2));
                float4 node_7410 = tex2D(_MainTex,TRANSFORM_TEX(node_1640, _MainTex));
                float node_8100 = (node_1017.r*node_7410.r*node_6829.g);
                float node_5133 = (i.uv0.g*node_8100*3.0);
                float node_5546 = (1.0 - i.uv0.g);
                float node_9596 = ((saturate((node_1017.b*node_7410.b))*node_5546)*_FlowMask_var.r*2.0);
                float node_6528 = (node_5133+saturate((node_9596+saturate((saturate((i.uv0.r*-2.8+1.0))+saturate((i.uv0.r*2.3+-1.3))+saturate((node_5546*1.375+-0.75))+saturate((i.uv0.g*2.25+-1.5)))))));
                float3 emissive = float3(node_6528,node_6528,node_6528);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                float2 node_2571 = (node_5132+node_499*float2(0,1));
                float4 node_9074 = tex2D(_MainTex,TRANSFORM_TEX(node_2571, _MainTex));
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,(_Opacity*saturate((((node_5133+node_9596)*2.0)+(node_6829.g*(node_6829.g+node_9074.a)))))),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _GrabTexture;
            uniform float4 _Color;
            uniform float _Opacity;
            uniform float _Reflaction;
            uniform float _VertexOffset;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _WaterSplash;
            uniform sampler2D _FlowMask; uniform float4 _FlowMask_ST;
            uniform float _FlowStrength;
            uniform sampler2D _FlowMap; uniform float4 _FlowMap_ST;
            uniform float _FlowMapSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
                LIGHTING_COORDS(4,5)
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_6200 = _Time;
                float node_499 = (_WaterSplash*node_6200.r);
                float2 node_6970 = (o.uv0+node_499*float2(-0.25,1));
                float4 node_1017 = tex2Dlod(_MainTex,float4(TRANSFORM_TEX(node_6970, _MainTex),0.0,0));
                float4 node_7244 = _Time;
                float2 node_1640 = (o.uv0+node_7244.g*float2(-0.25,2));
                float4 node_7410 = tex2Dlod(_MainTex,float4(TRANSFORM_TEX(node_1640, _MainTex),0.0,0));
                float4 _FlowMask_var = tex2Dlod(_FlowMask,float4(TRANSFORM_TEX(o.uv0, _FlowMask),0.0,0));
                float4 node_4728 = _Time;
                float node_6590 = (_FlowMapSpeed*node_4728.r);
                float2 node_5503 = (o.uv0+node_6590*float2(0.2,1.5));
                float4 node_9985 = tex2Dlod(_FlowMap,float4(TRANSFORM_TEX(node_5503, _FlowMap),0.0,0));
                float2 node_3365 = (o.uv0+node_6590*float2(-0.2,1));
                float4 node_5088 = tex2Dlod(_FlowMap,float4(TRANSFORM_TEX(node_3365, _FlowMap),0.0,0));
                float2 node_5132 = (o.uv0+(_FlowMask_var.r*(float2(node_9985.r,node_5088.g)*_FlowStrength)));
                float4 node_6829 = tex2Dlod(_MainTex,float4(TRANSFORM_TEX(node_5132, _MainTex),0.0,0));
                float node_8100 = (node_1017.r*node_7410.r*node_6829.g);
                float node_7018 = (node_8100*_VertexOffset);
                v.vertex.xyz += float3(node_7018,node_7018,node_7018);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float4 node_6200 = _Time;
                float node_499 = (_WaterSplash*node_6200.r);
                float2 node_7408 = (i.uv0+node_499*float2(-0.2,2));
                float4 node_7036 = tex2D(_MainTex,TRANSFORM_TEX(node_7408, _MainTex));
                float2 node_2283 = (i.uv0+node_499*float2(0.2,2));
                float4 node_5795 = tex2D(_MainTex,TRANSFORM_TEX(node_2283, _MainTex));
                float4 _FlowMask_var = tex2D(_FlowMask,TRANSFORM_TEX(i.uv0, _FlowMask));
                float4 node_4728 = _Time;
                float node_6590 = (_FlowMapSpeed*node_4728.r);
                float2 node_5503 = (i.uv0+node_6590*float2(0.2,1.5));
                float4 node_9985 = tex2D(_FlowMap,TRANSFORM_TEX(node_5503, _FlowMap));
                float2 node_3365 = (i.uv0+node_6590*float2(-0.2,1));
                float4 node_5088 = tex2D(_FlowMap,TRANSFORM_TEX(node_3365, _FlowMap));
                float2 node_5132 = (i.uv0+(_FlowMask_var.r*(float2(node_9985.r,node_5088.g)*_FlowStrength)));
                float4 node_6829 = tex2D(_MainTex,TRANSFORM_TEX(node_5132, _MainTex));
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + (saturate((float2(node_7036.r,node_5795.r)*node_6829.g*i.uv0.g))*_Reflaction*node_7036.rgb.rg);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
				UNITY_LIGHT_ATTENUATION(attenuation, i, i.posWorld.xyz);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuseColor = _Color.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                float2 node_6970 = (i.uv0+node_499*float2(-0.25,1));
                float4 node_1017 = tex2D(_MainTex,TRANSFORM_TEX(node_6970, _MainTex));
                float4 node_7244 = _Time;
                float2 node_1640 = (i.uv0+node_7244.g*float2(-0.25,2));
                float4 node_7410 = tex2D(_MainTex,TRANSFORM_TEX(node_1640, _MainTex));
                float node_8100 = (node_1017.r*node_7410.r*node_6829.g);
                float node_5133 = (i.uv0.g*node_8100*3.0);
                float node_5546 = (1.0 - i.uv0.g);
                float node_9596 = ((saturate((node_1017.b*node_7410.b))*node_5546)*_FlowMask_var.r*2.0);
                float2 node_2571 = (node_5132+node_499*float2(0,1));
                float4 node_9074 = tex2D(_MainTex,TRANSFORM_TEX(node_2571, _MainTex));
                fixed4 finalRGBA = fixed4(finalColor * (_Opacity*saturate((((node_5133+node_9596)*2.0)+(node_6829.g*(node_6829.g+node_9074.a))))),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _VertexOffset;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _WaterSplash;
            uniform sampler2D _FlowMask; uniform float4 _FlowMask_ST;
            uniform float _FlowStrength;
            uniform sampler2D _FlowMap; uniform float4 _FlowMap_ST;
            uniform float _FlowMapSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float4 node_6200 = _Time;
                float node_499 = (_WaterSplash*node_6200.r);
                float2 node_6970 = (o.uv0+node_499*float2(-0.25,1));
                float4 node_1017 = tex2Dlod(_MainTex,float4(TRANSFORM_TEX(node_6970, _MainTex),0.0,0));
                float4 node_240 = _Time;
                float2 node_1640 = (o.uv0+node_240.g*float2(-0.25,2));
                float4 node_7410 = tex2Dlod(_MainTex,float4(TRANSFORM_TEX(node_1640, _MainTex),0.0,0));
                float4 _FlowMask_var = tex2Dlod(_FlowMask,float4(TRANSFORM_TEX(o.uv0, _FlowMask),0.0,0));
                float4 node_4728 = _Time;
                float node_6590 = (_FlowMapSpeed*node_4728.r);
                float2 node_5503 = (o.uv0+node_6590*float2(0.2,1.5));
                float4 node_9985 = tex2Dlod(_FlowMap,float4(TRANSFORM_TEX(node_5503, _FlowMap),0.0,0));
                float2 node_3365 = (o.uv0+node_6590*float2(-0.2,1));
                float4 node_5088 = tex2Dlod(_FlowMap,float4(TRANSFORM_TEX(node_3365, _FlowMap),0.0,0));
                float2 node_5132 = (o.uv0+(_FlowMask_var.r*(float2(node_9985.r,node_5088.g)*_FlowStrength)));
                float4 node_6829 = tex2Dlod(_MainTex,float4(TRANSFORM_TEX(node_5132, _MainTex),0.0,0));
                float node_8100 = (node_1017.r*node_7410.r*node_6829.g);
                float node_7018 = (node_8100*_VertexOffset);
                v.vertex.xyz += float3(node_7018,node_7018,node_7018);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
