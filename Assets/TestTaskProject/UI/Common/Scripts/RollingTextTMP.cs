using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ProductMadness.TestTaskProject.UI
{
    public class RollingTextTMP : TextMeshProUGUI
    {
        [Header("Custom Parameters")]
        [SerializeField] private float duration = 1.5f;
        [SerializeField] private Ease ease = Ease.OutCubic;
        [SerializeField] private string template = "{0:N0} {1}";
        [SerializeField] private string currency = "COINS";
        
        [Header("Pop Scale")]
        [SerializeField] private bool doPopScale = true;
        [SerializeField] private float popScale = 0.25f;
        [SerializeField] private float popDuration = 0.15f;
        [SerializeField] private float checkSum = 10000;

        private Tween _tween;
        private RectTransform _transform;
        private int _lastThousands = -1;
        
        #if UNITY_EDITOR

        protected override void OnValidate()
        {
            base.OnValidate();
            if(!_transform) _transform = GetComponent<RectTransform>();
        }
        
        #endif

        protected override void Awake()
        {
            base.Awake();
            if(!_transform) _transform = GetComponent<RectTransform>();
        }

        public void SetValue(int value)
        {
            SetText(string.Format(template, value, currency));
        }

        public void Play(int from, int to)
        {
            _tween?.Kill();
            _transform.DOKill();
            
            _lastThousands = from / Convert.ToInt32(checkSum);

            var value = from;
            SetValue(value);

            _tween = DOTween.To(
                    () => value,
                    x =>
                    {
                        value = x;
                        SetValue(value);
                        
                        if (doPopScale)
                        {
                            CheckThousandPop(value);
                        }
                    },
                    to,
                    duration
                )
                .SetEase(ease);
        }

        public void Stop()
        {
            _tween?.Kill();
        }
        
        private void CheckThousandPop(int value)
        {
            var thousands = value / Convert.ToInt32(checkSum);
            if (thousands != _lastThousands)
            {
                _lastThousands = thousands;
                PlayPop();
            }
        }
        
        private void PlayPop()
        {
            _transform.DOPunchScale(Vector3.one * popScale, popDuration, vibrato: 1, elasticity: 0.5f);
        }
    }
}