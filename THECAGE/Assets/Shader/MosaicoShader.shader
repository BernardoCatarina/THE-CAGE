Shader "Custom/MosaicoMagico"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Cor da Tela", Color) = (0,0,0,1)
        _Cutoff ("Progresso", Range(-0.5, 3.5)) = -0.5
        _Tamanho ("Quantidade de Losangos", Float) = 15
    }

    SubShader
    {
        Tags { "Queue"="Transparent+100" "RenderType"="Transparent" }
        Cull Off Lighting Off ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t { float4 vertex : POSITION; float4 color : COLOR; float2 texcoord : TEXCOORD0; };
            struct v2f { float4 vertex : SV_POSITION; fixed4 color : COLOR; float2 texcoord : TEXCOORD0; };

            fixed4 _Color;
            float _Cutoff;
            float _Tamanho;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                float2 uv = IN.texcoord;

                
                float diagonal = uv.x + uv.y;

                uv.x *= 1.77;

                
                float2 grid = frac(uv * _Tamanho);
                float dist = abs(grid.x - 0.5) + abs(grid.y - 0.5);

               
                if (dist + diagonal > _Cutoff) return half4(0,0,0,0);
                
                return IN.color;
            }
            ENDCG
        }
    }
}