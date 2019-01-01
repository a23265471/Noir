// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-7536-OUT;n:type:ShaderForge.SFN_Multiply,id:2347,x:32302,y:32655,varname:node_2347,prsc:2|A-3625-OUT,B-7959-RGB,C-3583-RGB,D-9909-OUT;n:type:ShaderForge.SFN_VertexColor,id:7959,x:32075,y:32826,varname:node_7959,prsc:2;n:type:ShaderForge.SFN_Color,id:3583,x:32075,y:32984,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3160377,c2:0.8086352,c3:1,c4:1;n:type:ShaderForge.SFN_Append,id:8891,x:30267,y:32674,varname:node_8891,prsc:2|A-4356-OUT,B-3723-OUT;n:type:ShaderForge.SFN_Time,id:1529,x:30269,y:32845,varname:node_1529,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:4356,x:30077,y:32640,ptovrint:False,ptlb:U speed,ptin:_Uspeed,varname:node_1056,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_ValueProperty,id:3723,x:30077,y:32738,ptovrint:False,ptlb:V speed,ptin:_Vspeed,varname:_node_1056_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:2577,x:30460,y:32617,varname:node_2577,prsc:2|A-8891-OUT,B-1529-T;n:type:ShaderForge.SFN_Add,id:5436,x:30641,y:32637,varname:node_5436,prsc:2|A-2577-OUT,B-2941-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2941,x:30460,y:32775,varname:node_2941,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2dAsset,id:7375,x:30641,y:32792,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_1004,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:541f518900e7879438c577e82fee8d2c,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6336,x:30833,y:32637,varname:node_9752,prsc:2,tex:541f518900e7879438c577e82fee8d2c,ntxv:0,isnm:False|UVIN-5436-OUT,TEX-7375-TEX;n:type:ShaderForge.SFN_Append,id:9802,x:30269,y:32995,varname:node_9802,prsc:2|A-4820-OUT,B-7076-OUT;n:type:ShaderForge.SFN_Time,id:2040,x:30269,y:33181,varname:node_2040,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:4820,x:30083,y:32995,ptovrint:False,ptlb:2U speed_copy,ptin:_2Uspeed_copy,varname:_Uspeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.2;n:type:ShaderForge.SFN_ValueProperty,id:7076,x:30083,y:33093,ptovrint:False,ptlb:2V speed_copy,ptin:_2Vspeed_copy,varname:_Vspeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.05;n:type:ShaderForge.SFN_Multiply,id:5441,x:30460,y:32974,varname:node_5441,prsc:2|A-9802-OUT,B-2040-T;n:type:ShaderForge.SFN_TexCoord,id:640,x:30460,y:33132,varname:node_640,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:9129,x:30654,y:32974,varname:node_9129,prsc:2|A-5441-OUT,B-640-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:2030,x:30833,y:32876,varname:node_7201,prsc:2,tex:541f518900e7879438c577e82fee8d2c,ntxv:0,isnm:False|UVIN-9129-OUT,TEX-7375-TEX;n:type:ShaderForge.SFN_Slider,id:6165,x:30058,y:32425,ptovrint:False,ptlb:Dissolve,ptin:_Dissolve,varname:node_276,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2,max:1;n:type:ShaderForge.SFN_OneMinus,id:851,x:30424,y:32428,varname:node_851,prsc:2|IN-6165-OUT;n:type:ShaderForge.SFN_RemapRange,id:6661,x:30620,y:32438,varname:node_6661,prsc:2,frmn:0,frmx:1,tomn:-0.6,tomx:0.5|IN-851-OUT;n:type:ShaderForge.SFN_Add,id:996,x:31043,y:32637,varname:node_996,prsc:2|A-6661-OUT,B-6336-R;n:type:ShaderForge.SFN_Add,id:2158,x:31043,y:32818,varname:node_2158,prsc:2|A-6661-OUT,B-2030-R;n:type:ShaderForge.SFN_Multiply,id:8678,x:31263,y:32637,varname:node_8678,prsc:2|A-996-OUT,B-2158-OUT;n:type:ShaderForge.SFN_RemapRange,id:4127,x:31407,y:32637,varname:node_4127,prsc:2,frmn:0,frmx:1,tomn:-3,tomx:3|IN-8678-OUT;n:type:ShaderForge.SFN_Clamp01,id:3625,x:31581,y:32637,varname:node_3625,prsc:2|IN-4127-OUT;n:type:ShaderForge.SFN_OneMinus,id:9241,x:31766,y:32637,varname:node_9241,prsc:2|IN-3625-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9909,x:32075,y:33197,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_8823,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:7536,x:32466,y:32523,varname:node_7536,prsc:2|A-8358-OUT,B-2347-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5952,x:32053,y:32476,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:_Opacity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Fresnel,id:8358,x:32285,y:32346,varname:node_8358,prsc:2|EXP-5952-OUT;proporder:3583-9909-5952-4356-3723-7375-4820-7076-6165;pass:END;sub:END;*/

Shader "Shader Forge/Bullet" {
    Properties {
        _TintColor ("Color", Color) = (0.3160377,0.8086352,1,1)
        _Opacity ("Opacity", Float ) = 2
        _Fresnel ("Fresnel", Float ) = 0
        _Uspeed ("U speed", Float ) = 0.2
        _Vspeed ("V speed", Float ) = 0.2
        _Noise ("Noise", 2D) = "white" {}
        _2Uspeed_copy ("2U speed_copy", Float ) = -0.2
        _2Vspeed_copy ("2V speed_copy", Float ) = 0.05
        _Dissolve ("Dissolve", Range(0, 1)) = 0.2
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
            Blend One One
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
            uniform float4 _TintColor;
            uniform float _Uspeed;
            uniform float _Vspeed;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _2Uspeed_copy;
            uniform float _2Vspeed_copy;
            uniform float _Dissolve;
            uniform float _Opacity;
            uniform float _Fresnel;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float node_6661 = ((1.0 - _Dissolve)*1.1+-0.6);
                float4 node_1529 = _Time;
                float2 node_5436 = ((float2(_Uspeed,_Vspeed)*node_1529.g)+i.uv0);
                float4 node_9752 = tex2D(_Noise,TRANSFORM_TEX(node_5436, _Noise));
                float4 node_2040 = _Time;
                float2 node_9129 = ((float2(_2Uspeed_copy,_2Vspeed_copy)*node_2040.g)+i.uv0);
                float4 node_7201 = tex2D(_Noise,TRANSFORM_TEX(node_9129, _Noise));
                float node_3625 = saturate((((node_6661+node_9752.r)*(node_6661+node_7201.r))*6.0+-3.0));
                float3 emissive = (pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel)*(node_3625*i.vertexColor.rgb*_TintColor.rgb*_Opacity));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
