using FlappyBird.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FlappyBird.UI
{
    public class MuteButton : ButtonBase, IPointerClickHandler
    {
        [Header("Sprites")]
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite toggledSprite;

        [Header("Animation")]
        [SerializeField] private float hoverScale = 2f;

        private Vector3 _originalScale;
        private bool _isToggled;

        private void Start()
        {
            _originalScale = transform.localScale;
            _isToggled = AudioManager.Instance.IsMuted;
            Image.sprite = _isToggled ? toggledSprite : normalSprite;
            AudioManager.Instance.VolumeChanged += OnVolumeChanged;
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            transform.localScale = _originalScale;
        }

        #region Mouse pointer behaviour

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            transform.localScale = _originalScale * hoverScale;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            transform.localScale = _originalScale;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            AudioManager.Instance.ToggleMute();
        }

        #endregion
        
        private void OnVolumeChanged(object sender, VolumeChangedEventArgs e)
        {
            if (e.NewVolume == 0f || (e.NewVolume >= 0.05f && e.OldVolume == 0f))
                FlipSprite();
        }

        private void FlipSprite()
        {
            _isToggled = !_isToggled;
            Image.sprite = _isToggled ? toggledSprite : normalSprite;
        }
    }
}