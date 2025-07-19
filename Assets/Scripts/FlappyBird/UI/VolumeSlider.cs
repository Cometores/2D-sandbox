using System;
using FlappyBird.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FlappyBird.UI
{
    public class VolumeSlider : MonoBehaviour, IPointerDownHandler
    {
        private Image _fillImage;

        void Awake()
        {
            _fillImage = GetComponent<Image>();

            AudioManager.Instance.VolumeChanged += (sender, args) =>
            {
                _fillImage.fillAmount = args.NewVolume;
            };
        }

        void Start()
        {
            _fillImage.fillAmount = AudioManager.Instance.Volume;
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
    }
}
