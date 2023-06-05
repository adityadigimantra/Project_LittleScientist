Shader "Custom/BlurShader" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _BlurSize("Blur Size", Range(0.0, 10.0)) = 2.0
    }

        SubShader{
            Tags { "RenderType" = "Opaque" }
            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float _BlurSize;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    fixed4 col = fixed4(0, 0, 0, 0);
                    float2 blurSize = _BlurSize * _ScreenParams.xy;

                    col += tex2D(_MainTex, i.uv + float2(-1, -1) * blurSize) * 0.0625;
                    col += tex2D(_MainTex, i.uv + float2(-1, 0) * blurSize) * 0.125;
                    col += tex2D(_MainTex, i.uv + float2(-1, 1) * blurSize) * 0.0625;

                    col += tex2D(_MainTex, i.uv + float2(0, -1) * blurSize) * 0.125;
                    col += tex2D(_MainTex, i.uv) * 0.25;
                    col += tex2D(_MainTex, i.uv + float2(0, 1) * blurSize) * 0.125;

                    col += tex2D(_MainTex, i.uv + float2(1, -1) * blurSize) * 0.0625;
                    col += tex2D(_MainTex, i.uv + float2(1, 0) * blurSize) * 0.125;
                    col += tex2D(_MainTex, i.uv + float2(1, 1) * blurSize) * 0.0625;

                    return col;
                }
                ENDCG
            }
        }
}