// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:Mobile/Diffuse,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3138,x:33441,y:32692,varname:node_3138,prsc:2|emission-2191-OUT,custl-5158-OUT;n:type:ShaderForge.SFN_Tex2d,id:2999,x:32729,y:32483,ptovrint:False,ptlb:Main Texture,ptin:_MainTexture,varname:_MainTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-3942-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:3942,x:32468,y:32482,varname:node_3942,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:1894,x:32718,y:33013,ptovrint:False,ptlb:Shadow Map,ptin:_ShadowMap,varname:_ShadowMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-5449-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:5449,x:32445,y:33017,varname:node_5449,prsc:2,uv:1;n:type:ShaderForge.SFN_Multiply,id:5158,x:33141,y:32839,varname:node_5158,prsc:2|A-2999-RGB,B-1894-RGB,C-2365-OUT,D-8036-RGB;n:type:ShaderForge.SFN_LightAttenuation,id:2365,x:32935,y:33030,varname:node_2365,prsc:2;n:type:ShaderForge.SFN_AmbientLight,id:2418,x:32760,y:32763,varname:node_2418,prsc:2;n:type:ShaderForge.SFN_LightColor,id:8036,x:32935,y:33239,varname:node_8036,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2191,x:33043,y:32530,varname:node_2191,prsc:2|A-2999-RGB,B-2418-RGB;proporder:2999-1894;pass:END;sub:END;*/

Shader "Unity5xAnimations/ToonWithShadows" {
    Properties {
        _MainTexture ("Main Texture", 2D) = "white" {}
        _ShadowMap ("Shadow Map", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform sampler2D _ShadowMap; uniform float4 _ShadowMap_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                LIGHTING_COORDS(2,3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float4 _MainTexture_var = tex2D(_MainTexture,TRANSFORM_TEX(i.uv0, _MainTexture));
                float3 emissive = (_MainTexture_var.rgb*UNITY_LIGHTMODEL_AMBIENT.rgb);
                float4 _ShadowMap_var = tex2D(_ShadowMap,TRANSFORM_TEX(i.uv1, _ShadowMap));
                float3 finalColor = emissive + (_MainTexture_var.rgb*_ShadowMap_var.rgb*attenuation*_LightColor0.rgb);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform sampler2D _ShadowMap; uniform float4 _ShadowMap_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                LIGHTING_COORDS(2,3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _MainTexture_var = tex2D(_MainTexture,TRANSFORM_TEX(i.uv0, _MainTexture));
                float4 _ShadowMap_var = tex2D(_ShadowMap,TRANSFORM_TEX(i.uv1, _ShadowMap));
                float3 finalColor = (_MainTexture_var.rgb*_ShadowMap_var.rgb*attenuation*_LightColor0.rgb);
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Mobile/Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
