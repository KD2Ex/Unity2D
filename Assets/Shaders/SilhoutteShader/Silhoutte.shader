Shader "Unlit/Silhoutte"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {

            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 color = fixed4(0.0, 1.0, 1.0, 1.0);
                fixed4 col = color * tex2D(_MainTex, i.uv);

                if (col.x + col.y + col.z < .01)
                {
                    
                    //return fixed4(1.0, 1.0, 1.0, 0.0);
                }

                if (col.a < 0.07)
                {
                    return fixed4(1.0, 1.0, 1.0, 1.0);
                }
                
                return col;
            }
            ENDCG
        }
    }
}
