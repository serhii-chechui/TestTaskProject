using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProductMadness.TestTaskProject.UI
{
    public class PopUpAnimationController : MonoBehaviour
    {
        [Header("Shroud")]
        [SerializeField]
        private Image shroudImage;

        [Header("PopupContainer")]
        [SerializeField]
        private RectTransform popupTransform;

        [SerializeField]
        private CanvasGroup popupCanvasGroup;

        [Header("Title")]
        [SerializeField]
        private RectTransform titleImageTransform;

        [SerializeField]
        private Image titleImage;

        [SerializeField]
        private ParticleSystem titleParticlesA;

        [SerializeField]
        private ParticleSystem titleParticlesB;

        [SerializeField]
        private ParticleSystem titleParticlesC;
        
        [Header("Message")]
        [SerializeField]
        private TMP_Text messageText;
        
        [Header("Coins")]
        [SerializeField]
        private Image coinsPileImage;

        [SerializeField]
        private RectTransform coinsPileImageTransform;
        
        [SerializeField]
        private ParticleSystem coinsParticles;
        
        [SerializeField]
        private ParticleSystem glowParticles;
        
        [SerializeField]
        private RollingTextTMP coinsText;
        
        [Header("Confetti")]
        [SerializeField]
        private ParticleSystem confettiLeftParticles;
        
        [SerializeField]
        private ParticleSystem confettiRightParticles;

        private Sequence _sequence;
        
        [Header("Buttons")]
        [SerializeField]
        private Button closeButton;
        
        [SerializeField]
        private RectTransform closeButtonTransform;
        
        [SerializeField]
        private RectTransform actionButtonTransform;
        
        #if UNITY_EDITOR

        private void OnValidate()
        {
            if (popupTransform == null) popupTransform = GetComponent<RectTransform>();

            if (popupCanvasGroup == null) popupCanvasGroup = popupTransform.GetComponent<CanvasGroup>();
            
            if (closeButtonTransform == null) closeButtonTransform = closeButton.GetComponent<RectTransform>();
        }

        #endif

        private void Awake()
        {
            _sequence = DOTween.Sequence();

            shroudImage.DOFade(0, 0);

            popupTransform.DOLocalRotate(Vector3.forward * -6f, 0);
            popupTransform.DOScale(Vector3.zero, 0);
            popupCanvasGroup.DOFade(0, 0);

            titleImage.DOFade(0, 0);
            
            titleParticlesA.Stop();
            titleParticlesB.Stop();
            titleParticlesC.Stop();

            messageText.DOFade(0, 0);
            
            coinsPileImage.DOFade(0, 0);
            
            coinsParticles.Stop();
            glowParticles.Stop();
            
            coinsText.DOFade(0, 0);
            coinsText.SetValue(0);
            
            confettiLeftParticles.Stop();
            confettiRightParticles.Stop();
            
            closeButton.targetGraphic.DOFade(0, 0);
            closeButtonTransform.DOScale(Vector3.zero, 0);
            
            actionButtonTransform.DOScale(Vector3.zero, 0);
        }

        private void Start()
        {
            PlayAnimation();
        }

        private void OnDestroy()
        {
            _sequence.Kill();
        }

        [ExecuteInEditMode]
        public void PlayAnimation()
        {
            _sequence.Append(shroudImage.DOFade(0.6f, 1).SetEase(Ease.OutBack));

            _sequence.Insert(0.5f, popupTransform.DOLocalRotate(Vector3.zero, 1f).SetEase(Ease.OutBack));
            _sequence.Insert(0.5f, popupCanvasGroup.DOFade(1f, 0.5f).SetEase(Ease.Linear));
            _sequence.Insert(0.5f, popupTransform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack));

            _sequence.Insert(1.416f, titleImage.DOFade(1f, 0.25f).SetEase(Ease.OutQuart));

            _sequence.Insert(1.5f, titleImageTransform.DOScale(Vector3.one * 1.25f, 0.166f).SetEase(Ease.OutBack));
            _sequence.Insert(1.66f, titleImageTransform.DOScale(Vector3.one, 0.33f).SetEase(Ease.OutBack));
            
            _sequence.InsertCallback(2f, () => confettiLeftParticles.Play());
            _sequence.InsertCallback(2f, () => confettiRightParticles.Play());

            _sequence.InsertCallback(2f, () => { titleParticlesA.Play(); titleParticlesA.Emit(1); });
            _sequence.InsertCallback(2.25f, () => { titleParticlesB.Play(); titleParticlesB.Emit(1); });
            _sequence.InsertCallback(2.5f, () => { titleParticlesC.Play(); titleParticlesC.Emit(1); });

            _sequence.Insert(2f, messageText.DOFade(1f, 0.33f).SetEase(Ease.InOutQuad));
            
            _sequence.InsertCallback(3f, () => coinsParticles.Play());
            _sequence.InsertCallback(3.5f, () => glowParticles.Play());
            _sequence.Insert(4.6f, coinsPileImage.DOFade(1, 0.35f).SetEase(Ease.InOutQuad));
            _sequence.Insert(4.7f, coinsPileImageTransform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 5, 0.5f));
            _sequence.InsertCallback(4.7f, () => glowParticles.Stop());
            
            _sequence.Insert(6f, coinsText.DOFade(1, 0.35f).SetEase(Ease.InOutQuad));
            _sequence.InsertCallback(6.35f, () => coinsText.Play(0, 30000));
            
            _sequence.InsertCallback(6.35f, () => confettiLeftParticles.Play());
            _sequence.InsertCallback(6.35f, () => confettiRightParticles.Play());
            
            _sequence.Insert(7f, closeButton.targetGraphic.DOFade(1f, 0.5f).SetEase(Ease.OutQuart));
            _sequence.Insert(7f, closeButtonTransform.DOScale(Vector3.one, 0.75f).SetEase(Ease.OutBounce));

            _sequence.Insert(7f,actionButtonTransform.DOScale(Vector3.one, 0).SetEase(Ease.OutBounce));
        }
    }
}