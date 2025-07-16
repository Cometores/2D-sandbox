using FlappyBird.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FlappyBird.UI
{
    public class MenuButton : ButtonBase
    {
        [SerializeField] private GameObject hoveredVFX;
        
        private Animator[] _vfxAnimators;
        private Animator _animator; 
        private static readonly int LostFocus = Animator.StringToHash("lostFocus");
        private static readonly int Selected = Animator.StringToHash("selected");

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _vfxAnimators = GetComponentsInChildren<Animator>(true);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            AudioManager.Instance?.PlayUIHover();
            hoveredVFX.SetActive(true);
            _animator.SetTrigger(Selected);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            foreach (Animator animator in _vfxAnimators)
            {
                animator.SetTrigger(LostFocus);
            }
            hoveredVFX.SetActive(false);
        }

        protected override void OnDisable()
        {
            hoveredVFX.SetActive(false);
            base.OnDisable();
        }
    }
}