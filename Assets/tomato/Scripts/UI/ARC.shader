Shader "Custom/RadialProgress"
{
    Properties
    {
        _Color ("Color", Color) = (0,1,1,1)
        _BackgroundColor ("Background Color", Color) = (0,0,0,0.1)
        _Thickness ("Thickness", Range(0, 1)) = 0.1
        _Progress ("Progress", Range(0, 1)) = 0.5
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

            fixed4 _Color;
            fixed4 _BackgroundColor;
            float _Thickness;
            float _Progress;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv * 2.0 - 1.0; // Transform UV to -1 to 1 range
                float angle = atan2(uv.y, uv.x) / (2.0 * 3.14159265359) + 0.5;
                float dist = length(uv);

                // Check if within progress angle and within the thickness range
                float inRing = step(_Thickness, dist) * step(dist, 1.0);
                float inProgress = step(angle, _Progress);

                // Combine conditions
                float mask = inRing * inProgress;

                // Return color based on mask
                return lerp(_BackgroundColor, _Color, mask);
            }
            ENDCG
        }
    }
}

