using FlappyBird.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

namespace FlappyBird.UI
{
    [RequireComponent(typeof(Image))]
    public class MenuButton : UIElementBase, IPointerClickHandler
    {
        [Header("Sprites")]
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite toggledSprite;

        [Header("Animation")]
        [SerializeField] private float hoverScale = 2f;

        [Header("Events")]
        [SerializeField] private UnityEvent onClick;

        private Vector3 _originalScale;
        private bool _isToggled;

        protected override void Awake()
        {
            base.Awake();
            _originalScale = transform.localScale;
            _isToggled = AudioManager.Instance.IsMuted;
            Image.sprite = _isToggled ? toggledSprite : normalSprite;
        }

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
            _isToggled = !_isToggled;
            Image.sprite = _isToggled ? toggledSprite : normalSprite;

            if (AudioManager.Instance != null)
                AudioManager.Instance.ToggleMute();
            else
                Debug.LogWarning("AudioManager.Instance is null. Cannot toggle mute.");

            onClick?.Invoke();
        }
    }
}