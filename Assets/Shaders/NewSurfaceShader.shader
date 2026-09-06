Shader "Custom/AnimatedColorTransitionShaderTransparent" {
    Properties {
        _Color1 ("Color 1", Color) = (1, 0, 0, 0.5)
        _Color2 ("Color 2", Color) = (0, 0, 1, 0.5)
        _Speed ("Speed", Range(0.1, 10.0)) = 1.0
    }
 
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100
 
        Pass {
            Blend SrcAlpha OneMinusSrcAlpha // Добавляем прозрачность
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            // Входные параметры
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            // Выходные параметры
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            // Цвета для перехода
            float4 _Color1;
            float4 _Color2;
            // Скорость анимации
            float _Speed;
 
            // Шейдерная функция вершин
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            // Шейдерная функция фрагментов
            fixed4 frag (v2f i) : SV_Target {
                // Вычисляем текущий коэффициент линейной интерполяции
                float t = sin(_Time.y * _Speed);
                t = (t + 1.0) / 2.0;
 
                // Вычисляем текущий цвет в зависимости от коэффициента линейной интерполяции
                fixed4 currentColor = lerp(_Color1, _Color2, t);
                currentColor.a = (currentColor.r + currentColor.g + currentColor.b) / 3; // Добавляем прозрачность
                return currentColor;
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
}
