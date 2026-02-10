using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UIHighlightAnimatorProxy : MonoBehaviour
    {
        private static readonly int Intensity = Shader.PropertyToID("_HighlightIntensity");
        private static readonly int Speed = Shader.PropertyToID("_HighlightSpeed");
        private static readonly int Width = Shader.PropertyToID("_HighlightWidth");
        private static readonly int Influence = Shader.PropertyToID("_LumaInfluence");
        private static readonly int HightlightAngle = Shader.PropertyToID("_HighlightAngle");

        [Range(0,5)]
        public float HighlightIntensity = 1;

        [Range(-10,10)]
        public float HighlightSpeed = 1;

        [Range(0,1)]
        public float HighlightWidth = 0.25f;

        [Range(0,1)]
        public float LumaInfluence = 1;
        
        [Range(0, 360)]
        public float Angle = 0;

        private Image image;
        private Material runtimeMat;

        private void Awake()
        {
            image = GetComponent<Image>();
            runtimeMat = Instantiate(image.material);
            image.material = runtimeMat;

            if (image.material.HasProperty(Intensity))
            {
                HighlightIntensity = image.material.GetFloat(Intensity);
            }
        }

        private void Update()
        {
            runtimeMat.SetFloat(Intensity, HighlightIntensity);
            runtimeMat.SetFloat(Speed, HighlightSpeed);
            runtimeMat.SetFloat(Width, HighlightWidth);
            runtimeMat.SetFloat(Influence, LumaInfluence);
            runtimeMat.SetFloat(HightlightAngle, Angle);
        }
    }
}