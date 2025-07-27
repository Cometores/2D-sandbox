using System;
using FlappyBird.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FlappyBird.UI
{
    public class VolumeSlider : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image previousImage;
        private Image _fillImage;

        void Awake()
        {
            _fillImage = GetComponent<Image>();
        }

        void Start()
        {
            _fillImage.fillAmount = AudioManager.Instance.Volume;
            AudioManager.Instance.VolumeChanged += OnVolumeChanged;
        }
        
        private void OnEnable()
        {
            AudioManager.Instance.VolumeChanged += OnVolumeChanged;
        }

        private void OnDisable()
        {
            AudioManager.Instance.VolumeChanged -= OnVolumeChanged;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            var imageWidth = _fillImage.rectTransform.rect.width * 2;
            var fixedClick = eventData.position.x - _fillImage.transform.position.x;
            var percentage = fixedClick / imageWidth;
            var final = MathF.Round(percentage * 20) / 20;
            
            _fillImage.fillAmount = final;
            if (final == 0)
            {
                AudioManager.Instance.ToggleMute();
            }
            
            AudioManager.Instance.SetVolume(final);
        }
        
        private void OnVolumeChanged(object sender, VolumeChangedEventArgs e)
        {
            if (Mathf.Approximately(e.NewVolume, e.OldVolume))
            {
                return;
            }
            if (e.NewVolume == 0)
            {
                previousImage.gameObject.SetActive(true);
                previousImage.fillAmount = e.OldVolume;
            }
            if (previousImage && e.NewVolume >= 0.05f)
            {
                previousImage.gameObject.SetActive(false);
            }
                
            _fillImage.fillAmount = e.NewVolume;
        }
    }
}
