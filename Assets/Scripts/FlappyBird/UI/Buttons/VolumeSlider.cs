using System;
using FlappyBird.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FlappyBird.UI.Buttons
{
    [RequireComponent(typeof(Image))]
    public class VolumeSlider : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image previousImage;
        private Image _fillImage;

        private void Awake()
        {
            _fillImage = GetComponent<Image>();
        }

        private void Start()
        {
            var volume = PlayerPrefs.GetFloat("Volume", Constants.DEFAULT_VOLUME);
            SetFillAmountSafe(volume);
        }

        private void OnEnable()
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.VolumeChanged += OnVolumeChanged;
        }

        private void OnDisable()
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.VolumeChanged -= OnVolumeChanged;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_fillImage == null) return;

            var imageWidth = _fillImage.rectTransform.rect.width * 2;
            var fixedClick = eventData.position.x - _fillImage.transform.position.x;
            var percentage = fixedClick / imageWidth;
            var final = MathF.Round(percentage * 20) / 20;

            SetFillAmountSafe(final);

            if (final == 0)
            {
                AudioManager.Instance?.ToggleMute();
            }
            else
            {
                AudioManager.Instance?.SetVolume(final);
            }
        }

        private void OnVolumeChanged(object sender, VolumeChangedEventArgs e)
        {
            if (!this || !isActiveAndEnabled) return;
            if (_fillImage == null) return;

            if (Mathf.Approximately(e.NewVolume, e.OldVolume))
                return;

            SetFillAmountSafe(e.NewVolume);

            if (previousImage != null)
            {
                if (e.NewVolume == 0)
                {
                    previousImage.gameObject.SetActive(true);
                    previousImage.fillAmount = e.OldVolume;
                }
                else if (e.NewVolume >= 0.05f)
                {
                    previousImage.gameObject.SetActive(false);
                }
            }
        }

        private void SetFillAmountSafe(float value)
        {
            if (_fillImage != null)
                _fillImage.fillAmount = value;
        }
    }
}
