using UnityEngine;
using UnityEngine.EventSystems;

namespace FlappyBird.UI
{
    public class BestScore : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private GameObject glowingVFX;
        
        private Animator _animator;
        
        private bool _animPlayed;
        private bool _pointerEntered;
        private static readonly int Pressed = Animator.StringToHash("pressed");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _animPlayed = false;
        }

        private void OnDisable()
        {
            _animPlayed = false;
            glowingVFX.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _pointerEntered = true;
            
            if (_animPlayed)
            {
                glowingVFX.SetActive(true);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _pointerEntered = false;
            glowingVFX.SetActive(false);
        }
        
        public void SetAnimPlayed()
        {
            _animPlayed = true;

            if (_pointerEntered)
            {
                glowingVFX.SetActive(true);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_animPlayed)
            {
                _animator.SetTrigger(Pressed);
            }
        }
    }
}