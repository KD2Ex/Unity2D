Shader "Unlit/Stencil test"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color (RGBA)", Color) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags {"RenderType"="Opaque" "Queue"="Transparent"}
        LOD 100
        ZTest Off
        ZWrite On
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        
        Pass
        {
            Stencil
            {
                Ref 1
                Comp Equal
            }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 position : SV_POSITION;
                float4 screenPosition : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenPosition = ComputeScreenPos(o.position);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float2 textureCoordinate = i.screenPosition.xy / i.screenPosition.w;
                fixed4 colTest = tex2D(_MainTex, textureCoordinate);
                fixed4 col = tex2D(_MainTex, i.uv);
                
                if (col.a > 0.1)
                {
                    col = _Color;
                }
                //col = fixed4(col.r, col.g, col.b * _Color.b, col.a * _Color.a);
                /*if (col.a >= .98)
                {
                    col = fixed4(0, 0, 0, 0);
                } else
                {
                }*/
                //col = _Color;
                
                return col;
            }
            ENDCG
        }
    }
    Fallback "Standard"
}
