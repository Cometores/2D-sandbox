using FlappyBird.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FlappyBird.UI
{
    public class MuteButton : ButtonBase, IPointerClickHandler
    {
        [Header("Sprites")] [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite mutedSprite;

        [Header("Animation")] [SerializeField] private float hoverScale = 2f;

        private Vector3 _originalScale;
        private bool IsMuted => AudioManager.Instance;

        private void Start()
        {
            _originalScale = transform.localScale;
            
            var volume = PlayerPrefs.GetFloat("Volume", Constants.DEFAULT_VOLUME);
            Image.sprite = volume > 0 ? normalSprite : mutedSprite;
        }

        private void OnEnable()
        {
            if (AudioManager.Instance)
                AudioManager.Instance.VolumeChanged += OnVolumeChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            transform.localScale = _originalScale;
            AudioManager.Instance.VolumeChanged -= OnVolumeChanged;
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

        public void OnPointerClick(PointerEventData eventData) => AudioManager.Instance.ToggleMute();

        #endregion

        private void OnVolumeChanged(object sender, VolumeChangedEventArgs e)
        {
            if (e.NewVolume == 0f)
            {
                Debug.Log("Меняю спрайт на мьют");
                Image.sprite = mutedSprite;
            }
            else if (e.NewVolume >= Constants.MUTE_THRESHOLD)
                Image.sprite = normalSprite;
        }
    }
}