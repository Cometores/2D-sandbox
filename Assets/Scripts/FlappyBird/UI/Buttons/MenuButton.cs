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
        private static readonly int Rand = Animator.StringToHash("rand");

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
            _animator.SetTrigger(Selected);

            hoveredVFX.SetActive(true);
            var randI = Random.Range(0, 3);
            foreach (Animator animator in _vfxAnimators)
            {
                animator.SetInteger(Rand, randI);
                animator.SetTrigger(Selected);
            }
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            
            var randI = Random.Range(0, 3);
            foreach (Animator animator in _vfxAnimators)
            {
                animator.SetInteger(Rand, randI);
                animator.SetTrigger(LostFocus);
            }
        }

        public void TurnOffVFX()
        {
            hoveredVFX.SetActive(false);
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            TurnOffVFX();
        }
    }
}