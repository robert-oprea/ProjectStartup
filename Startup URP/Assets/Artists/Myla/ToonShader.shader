Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _Albedo("Albedo", Color) = (1, 1, 1, 1)
        _Shades("Shades", Range(1, 20)) = 3
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
            };

            float4 _Albedo;

            float _Shades;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Calculate the cosine of the angle between the normal vector and the light direction
                // The world space light direction is stored in _WorldSpaceLightPos0
                // The world space normal is stored in i.worldNormal
                
                float cosineAngle = dot(normalize(i.worldNormal), normalize(_WorldSpaceLightPos0.xyz));

                // Set the min to zero
                cosineAngle = max(cosineAngle, 0.0);
                cosineAngle = floor(cosineAngle * _Shades) / _Shades;

                return _Albedo * cosineAngle;
            }
            ENDCG
        }
    }

    Fallback "VertexLit"
}
