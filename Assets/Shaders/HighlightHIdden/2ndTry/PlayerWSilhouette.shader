Shader "Unlit/PlayerWSilhouette"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        _Color ("Tint", Color) = (1,1,1,1)
        _OutlineColor ("Outline Color", Color) = (0,0,1,1)
    }
    SubShader
    {
        Tags 
        { 
            "Queue"="Transparent+1" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True" 
        }
        LOD 100

        Cull Off
        Lighting Off
        ZWrite On
        Blend One OneMinusSrcAlpha
        
        Pass
        {
            Name "ForeGround"
            ZTest LEqual
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color    : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                fixed4 color    : COLOR;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color;

                #ifdef PIXELSNAP_ON
                #endif
                
                return o;
            }

          sampler2D _AlphaTex;
          float _AlphaSplitEnabled;

          fixed4 SampleSpriteTexture (float2 uv)
          {
              fixed4 color = tex2D (_MainTex, uv);

  #if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
              if (_AlphaSplitEnabled)
                  color.a = tex2D (_AlphaTex, uv).r;
  #endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

              return color;
          }

            fixed4 frag (v2f i) : SV_Target
            {
                //fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 col = SampleSpriteTexture(i.uv) * i.color;
                col.rgb *= col.a;
                return col;
            }
            ENDCG
        }

        Pass
        {
            Name "Background"
          ZTest Greater
          Blend SrcAlpha OneMinusSrcAlpha 

          CGPROGRAM 

          #pragma vertex vert 
          #pragma fragment frag

          float4 vert(float4 vertexPos : POSITION) : SV_POSITION 
          {
          return UnityObjectToClipPos(vertexPos);
          }

          float4 _OutlineColor;

          float4 frag(void) : Color 
          {
              return _OutlineColor;
          }

          ENDCG  
        }
    }
}
