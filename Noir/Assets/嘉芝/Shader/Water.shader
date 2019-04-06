// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:33026,y:32544,varname:node_2865,prsc:2|diff-2691-OUT,spec-5141-OUT,gloss-8659-OUT,normal-8988-OUT,emission-8296-OUT,transm-504-OUT,lwrap-504-OUT,alpha-7566-OUT,refract-8318-OUT;n:type:ShaderForge.SFN_Multiply,id:2691,x:32565,y:32362,varname:node_2691,prsc:2|A-1781-OUT,B-5472-OUT,C-143-OUT;n:type:ShaderForge.SFN_Vector1,id:5141,x:32349,y:32601,varname:node_5141,prsc:2,v1:0;n:type:ShaderForge.SFN_Slider,id:8659,x:32214,y:32728,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_8659,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6923077,max:1;n:type:ShaderForge.SFN_Vector1,id:504,x:32362,y:32985,varname:node_504,prsc:2,v1:1;n:type:ShaderForge.SFN_Color,id:645,x:31786,y:32178,ptovrint:False,ptlb:color,ptin:_color,varname:node_645,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.2,c3:1,c4:1;n:type:ShaderForge.SFN_VertexColor,id:7263,x:31786,y:32342,varname:node_7263,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8318,x:32453,y:33415,varname:node_8318,prsc:2|A-2999-OUT,B-2439-OUT;n:type:ShaderForge.SFN_Normalize,id:8988,x:32362,y:32840,varname:node_8988,prsc:2|IN-9518-OUT;n:type:ShaderForge.SFN_Lerp,id:9406,x:31989,y:32880,varname:node_9406,prsc:2|A-4705-OUT,B-8693-RGB,T-6878-OUT;n:type:ShaderForge.SFN_Vector3,id:4705,x:31722,y:32899,varname:node_4705,prsc:2,v1:0,v2:0,v3:3;n:type:ShaderForge.SFN_Tex2d,id:8693,x:31786,y:33044,ptovrint:False,ptlb:node_8693,ptin:_node_8693,varname:node_8693,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d470eb335cb6f1c41b767eb202c15b4c,ntxv:3,isnm:True|UVIN-2693-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:2999,x:32025,y:33111,varname:node_2999,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-8693-RGB;n:type:ShaderForge.SFN_Multiply,id:2439,x:32065,y:33444,varname:node_2439,prsc:2|A-8294-OUT,B-6272-OUT;n:type:ShaderForge.SFN_Slider,id:8294,x:31651,y:33399,ptovrint:False,ptlb:Disortion,ptin:_Disortion,varname:_Reflection_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2166753,max:1;n:type:ShaderForge.SFN_Vector1,id:6272,x:31770,y:33474,varname:node_6272,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Panner,id:2693,x:31588,y:33051,varname:node_2693,prsc:2,spu:0.02,spv:0.02|UVIN-5371-OUT;n:type:ShaderForge.SFN_Slider,id:6878,x:31651,y:33266,ptovrint:False,ptlb:Reflection,ptin:_Reflection,varname:node_6878,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:-1,max:1;n:type:ShaderForge.SFN_Multiply,id:143,x:32101,y:32190,varname:node_143,prsc:2|A-645-RGB,B-7263-A;n:type:ShaderForge.SFN_Fresnel,id:5472,x:32098,y:32549,varname:node_5472,prsc:2|NRM-9406-OUT,EXP-8158-OUT;n:type:ShaderForge.SFN_Slider,id:8158,x:31629,y:32525,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_8158,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_NormalBlend,id:9518,x:32214,y:32840,varname:node_9518,prsc:2|BSE-9406-OUT,DTL-5918-RGB;n:type:ShaderForge.SFN_Tex2d,id:5918,x:31798,y:32670,ptovrint:False,ptlb:NormalDetail,ptin:_NormalDetail,varname:node_5918,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True|UVIN-4939-UVOUT;n:type:ShaderForge.SFN_Panner,id:4939,x:31629,y:32654,varname:node_4939,prsc:2,spu:-0.03,spv:-0.03|UVIN-7128-OUT;n:type:ShaderForge.SFN_Multiply,id:7128,x:31385,y:32654,varname:node_7128,prsc:2|A-477-UVOUT,B-4931-OUT;n:type:ShaderForge.SFN_TexCoord,id:477,x:31120,y:32880,varname:node_477,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector1,id:4931,x:31143,y:32728,varname:node_4931,prsc:2,v1:100;n:type:ShaderForge.SFN_Multiply,id:5371,x:31361,y:33061,varname:node_5371,prsc:2|A-477-UVOUT,B-2348-OUT;n:type:ShaderForge.SFN_Vector1,id:2348,x:31164,y:33210,varname:node_2348,prsc:2,v1:5;n:type:ShaderForge.SFN_Multiply,id:7566,x:32624,y:33013,varname:node_7566,prsc:2|A-9267-OUT,B-6623-A,C-8933-OUT;n:type:ShaderForge.SFN_VertexColor,id:6623,x:32362,y:33181,varname:node_6623,prsc:2;n:type:ShaderForge.SFN_Clamp01,id:9267,x:30394,y:33517,varname:node_9267,prsc:2|IN-1481-OUT;n:type:ShaderForge.SFN_Multiply,id:1481,x:30249,y:33341,varname:node_1481,prsc:2|A-4538-OUT,B-5756-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4538,x:30063,y:33283,varname:node_4538,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-5538-OUT;n:type:ShaderForge.SFN_Vector1,id:5756,x:30063,y:33449,varname:node_5756,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Clamp01,id:5538,x:29844,y:33258,varname:node_5538,prsc:2|IN-6577-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:3116,x:29584,y:33252,varname:node_3116,prsc:2|IN-4964-OUT,IMIN-952-OUT,IMAX-5573-OUT,OMIN-6011-OUT,OMAX-7105-OUT;n:type:ShaderForge.SFN_Slider,id:952,x:29178,y:33244,ptovrint:False,ptlb:FoamMin,ptin:_FoamMin,varname:node_7763,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:5573,x:29178,y:33333,ptovrint:False,ptlb:FoamMax,ptin:_FoamMax,varname:_FoamMin_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Vector1,id:6011,x:29307,y:33442,varname:node_6011,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:7105,x:29307,y:33539,varname:node_7105,prsc:2,v1:1;n:type:ShaderForge.SFN_Clamp01,id:4964,x:29307,y:33639,varname:node_4964,prsc:2|IN-5464-OUT;n:type:ShaderForge.SFN_Divide,id:5464,x:29122,y:33665,varname:node_5464,prsc:2|A-183-OUT,B-7008-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:7349,x:29658,y:33738,varname:node_7349,prsc:2|IN-4964-OUT,IMIN-5526-OUT,IMAX-6812-OUT,OMIN-905-OUT,OMAX-8159-OUT;n:type:ShaderForge.SFN_Slider,id:5526,x:29228,y:33901,ptovrint:False,ptlb:DepthMin,ptin:_DepthMin,varname:_FoamMin_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:6812,x:29228,y:33990,ptovrint:False,ptlb:DepthMax,ptin:_DepthMax,varname:_FoamMax_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_Vector1,id:905,x:29374,y:34065,varname:node_905,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:8159,x:29438,y:34129,varname:node_8159,prsc:2,v1:1;n:type:ShaderForge.SFN_DepthBlend,id:183,x:28921,y:33506,varname:node_183,prsc:2|DIST-7727-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7727,x:28676,y:33458,ptovrint:False,ptlb:Depth,ptin:_Depth,varname:node_4620,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Dot,id:7008,x:28735,y:33639,varname:node_7008,prsc:2,dt:4|A-3287-OUT,B-5983-OUT;n:type:ShaderForge.SFN_ViewVector,id:3287,x:28500,y:33659,varname:node_3287,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:5983,x:28500,y:33802,prsc:2,pt:False;n:type:ShaderForge.SFN_Lerp,id:1781,x:30337,y:33028,varname:node_1781,prsc:2|A-4265-RGB,B-7504-RGB,T-5538-OUT;n:type:ShaderForge.SFN_Color,id:4265,x:29978,y:32794,ptovrint:False,ptlb:BoardColor_A,ptin:_BoardColor_A,varname:node_9904,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:7504,x:29978,y:32957,ptovrint:False,ptlb:BoardColor_B,ptin:_BoardColor_B,varname:_node_9904_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.7933049,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:6577,x:29742,y:33087,varname:node_6577,prsc:2|A-1994-OUT,B-8823-OUT;n:type:ShaderForge.SFN_Clamp01,id:8823,x:29509,y:33120,varname:node_8823,prsc:2|IN-3116-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1994,x:29401,y:33047,ptovrint:False,ptlb:FoamPower,ptin:_FoamPower,varname:_Depth_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:8;n:type:ShaderForge.SFN_Multiply,id:8296,x:30321,y:32687,varname:node_8296,prsc:2|A-6545-OUT,B-4265-RGB;n:type:ShaderForge.SFN_ValueProperty,id:6545,x:29978,y:32711,ptovrint:False,ptlb:ColorEmission,ptin:_ColorEmission,varname:node_6545,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.4;n:type:ShaderForge.SFN_Slider,id:8933,x:32236,y:33094,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_8933,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;proporder:8659-645-8693-6878-8158-5918-8294-952-5573-7727-4265-7504-1994-6545-5526-6812-8933;pass:END;sub:END;*/

Shader "Shader Forge/Water" {
    Properties {
        _Gloss ("Gloss", Range(0, 1)) = 0.6923077
        _color ("color", Color) = (0,0.2,1,1)
        _node_8693 ("node_8693", 2D) = "bump" {}
        _Reflection ("Reflection", Range(-1, 1)) = -1
        _Fresnel ("Fresnel", Range(0, 1)) = 0
        _NormalDetail ("NormalDetail", 2D) = "bump" {}
        _Disortion ("Disortion", Range(0, 1)) = 0.2166753
        _FoamMin ("FoamMin", Range(0, 1)) = 0
        _FoamMax ("FoamMax", Range(0, 1)) = 1
        _Depth ("Depth", Float ) = 10
        _BoardColor_A ("BoardColor_A", Color) = (1,0,0,1)
        _BoardColor_B ("BoardColor_B", Color) = (0,0.7933049,1,1)
        _FoamPower ("FoamPower", Float ) = 8
        _ColorEmission ("ColorEmission", Float ) = 0.4
        _DepthMin ("DepthMin", Range(0, 1)) = 0
        _DepthMax ("DepthMax", Range(0, 10)) = 1
        _Opacity ("Opacity", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _CameraDepthTexture;
            uniform float _Gloss;
            uniform float4 _color;
            uniform sampler2D _node_8693; uniform float4 _node_8693_ST;
            uniform float _Disortion;
            uniform float _Reflection;
            uniform float _Fresnel;
            uniform sampler2D _NormalDetail; uniform float4 _NormalDetail_ST;
            uniform float _FoamMin;
            uniform float _FoamMax;
            uniform float _Depth;
            uniform float4 _BoardColor_A;
            uniform float4 _BoardColor_B;
            uniform float _FoamPower;
            uniform float _ColorEmission;
            uniform float _Opacity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD7;
                UNITY_FOG_COORDS(8)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD9;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
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
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 node_4720 = _Time;
                float2 node_2693 = ((i.uv0*5.0)+node_4720.g*float2(0.02,0.02));
                float3 _node_8693_var = UnpackNormal(tex2D(_node_8693,TRANSFORM_TEX(node_2693, _node_8693)));
                float3 node_9406 = lerp(float3(0,0,3),_node_8693_var.rgb,_Reflection);
                float2 node_4939 = ((i.uv0*100.0)+node_4720.g*float2(-0.03,-0.03));
                float3 _NormalDetail_var = UnpackNormal(tex2D(_NormalDetail,TRANSFORM_TEX(node_4939, _NormalDetail)));
                float3 node_9518_nrm_base = node_9406 + float3(0,0,1);
                float3 node_9518_nrm_detail = _NormalDetail_var.rgb * float3(-1,-1,1);
                float3 node_9518_nrm_combined = node_9518_nrm_base*dot(node_9518_nrm_base, node_9518_nrm_detail)/node_9518_nrm_base.z - node_9518_nrm_detail;
                float3 node_9518 = node_9518_nrm_combined;
                float3 normalLocal = normalize(node_9518);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + (_node_8693_var.rgb.rg*(_Disortion*0.2));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float perceptualRoughness = 1.0 - _Gloss;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = 0.0;
                float specularMonochrome;
                float node_4964 = saturate((saturate((sceneZ-partZ)/_Depth)/0.5*dot(viewDirection,i.normalDir)+0.5));
                float node_6011 = 0.0;
                float node_5538 = saturate((_FoamPower*saturate((node_6011 + ( (node_4964 - _FoamMin) * (1.0 - node_6011) ) / (_FoamMax - _FoamMin)))));
                float3 diffuseColor = (lerp(_BoardColor_A.rgb,_BoardColor_B.rgb,node_5538)*pow(1.0-max(0,dot(node_9406, viewDirection)),_Fresnel)*(_color.rgb*i.vertexColor.a)); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float node_504 = 1.0;
                float3 w = float3(node_504,node_504,node_504)*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                float3 backLight = max(float3(0.0,0.0,0.0), -NdotLWrap + w ) * float3(node_504,node_504,node_504);
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotLWrap);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((forwardLight+backLight) + ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL)) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = (_ColorEmission*_BoardColor_A.rgb);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,(saturate((node_5538.r*0.5))*i.vertexColor.a*_Opacity)),1);
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _CameraDepthTexture;
            uniform float _Gloss;
            uniform float4 _color;
            uniform sampler2D _node_8693; uniform float4 _node_8693_ST;
            uniform float _Disortion;
            uniform float _Reflection;
            uniform float _Fresnel;
            uniform sampler2D _NormalDetail; uniform float4 _NormalDetail_ST;
            uniform float _FoamMin;
            uniform float _FoamMax;
            uniform float _Depth;
            uniform float4 _BoardColor_A;
            uniform float4 _BoardColor_B;
            uniform float _FoamPower;
            uniform float _ColorEmission;
            uniform float _Opacity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD7;
                LIGHTING_COORDS(8,9)
                UNITY_FOG_COORDS(10)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
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
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 node_9794 = _Time;
                float2 node_2693 = ((i.uv0*5.0)+node_9794.g*float2(0.02,0.02));
                float3 _node_8693_var = UnpackNormal(tex2D(_node_8693,TRANSFORM_TEX(node_2693, _node_8693)));
                float3 node_9406 = lerp(float3(0,0,3),_node_8693_var.rgb,_Reflection);
                float2 node_4939 = ((i.uv0*100.0)+node_9794.g*float2(-0.03,-0.03));
                float3 _NormalDetail_var = UnpackNormal(tex2D(_NormalDetail,TRANSFORM_TEX(node_4939, _NormalDetail)));
                float3 node_9518_nrm_base = node_9406 + float3(0,0,1);
                float3 node_9518_nrm_detail = _NormalDetail_var.rgb * float3(-1,-1,1);
                float3 node_9518_nrm_combined = node_9518_nrm_base*dot(node_9518_nrm_base, node_9518_nrm_detail)/node_9518_nrm_base.z - node_9518_nrm_detail;
                float3 node_9518 = node_9518_nrm_combined;
                float3 normalLocal = normalize(node_9518);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + (_node_8693_var.rgb.rg*(_Disortion*0.2));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
				UNITY_LIGHT_ATTENUATION(attenuation, i, i.posWorld.xyz);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float perceptualRoughness = 1.0 - _Gloss;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = 0.0;
                float specularMonochrome;
                float node_4964 = saturate((saturate((sceneZ-partZ)/_Depth)/0.5*dot(viewDirection,i.normalDir)+0.5));
                float node_6011 = 0.0;
                float node_5538 = saturate((_FoamPower*saturate((node_6011 + ( (node_4964 - _FoamMin) * (1.0 - node_6011) ) / (_FoamMax - _FoamMin)))));
                float3 diffuseColor = (lerp(_BoardColor_A.rgb,_BoardColor_B.rgb,node_5538)*pow(1.0-max(0,dot(node_9406, viewDirection)),_Fresnel)*(_color.rgb*i.vertexColor.a)); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float node_504 = 1.0;
                float3 w = float3(node_504,node_504,node_504)*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                float3 backLight = max(float3(0.0,0.0,0.0), -NdotLWrap + w ) * float3(node_504,node_504,node_504);
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotLWrap);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((forwardLight+backLight) + ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL)) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * (saturate((node_5538.r*0.5))*i.vertexColor.a*_Opacity),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform float _Gloss;
            uniform float4 _color;
            uniform sampler2D _node_8693; uniform float4 _node_8693_ST;
            uniform float _Reflection;
            uniform float _Fresnel;
            uniform float _FoamMin;
            uniform float _FoamMax;
            uniform float _Depth;
            uniform float4 _BoardColor_A;
            uniform float4 _BoardColor_B;
            uniform float _FoamPower;
            uniform float _ColorEmission;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = (_ColorEmission*_BoardColor_A.rgb);
                
                float node_4964 = saturate((saturate((sceneZ-partZ)/_Depth)/0.5*dot(viewDirection,i.normalDir)+0.5));
                float node_6011 = 0.0;
                float node_5538 = saturate((_FoamPower*saturate((node_6011 + ( (node_4964 - _FoamMin) * (1.0 - node_6011) ) / (_FoamMax - _FoamMin)))));
                float4 node_8266 = _Time;
                float2 node_2693 = ((i.uv0*5.0)+node_8266.g*float2(0.02,0.02));
                float3 _node_8693_var = UnpackNormal(tex2D(_node_8693,TRANSFORM_TEX(node_2693, _node_8693)));
                float3 node_9406 = lerp(float3(0,0,3),_node_8693_var.rgb,_Reflection);
                float3 diffColor = (lerp(_BoardColor_A.rgb,_BoardColor_B.rgb,node_5538)*pow(1.0-max(0,dot(node_9406, viewDirection)),_Fresnel)*(_color.rgb*i.vertexColor.a));
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, 0.0, specColor, specularMonochrome );
                float roughness = 1.0 - _Gloss;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
