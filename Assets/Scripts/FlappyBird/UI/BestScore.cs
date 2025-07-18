using FlappyBird.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FlappyBird.UI
{
    [RequireComponent(typeof(Animator))]
    public class BestScore : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private GameObject beatenGlowingVFX;
        [SerializeField] private TextMeshProUGUI bestScoreTxt;

        private Animator _animator;
        private bool _appearAnimationPlayed;
        private bool _pointerEntered;
        
        private static readonly int Pressed = Animator.StringToHash("pressed");
        private static readonly int IsBeaten = Animator.StringToHash("isBeaten");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            if (beatenGlowingVFX == null)
                Debug.LogError($"{nameof(beatenGlowingVFX)} is not assigned", this);
            if (bestScoreTxt == null)
                Debug.LogError($"{nameof(bestScoreTxt)} is not assigned", this);
        }

        private void Start()
        {
            bestScoreTxt.text = GameManager.BestScore.ToString();
        }

        private void OnEnable()
        {
            _appearAnimationPlayed = false;
            _animator.SetBool(IsBeaten, GameManager.IsBeaten);
        }

        private void OnDisable()
        {
            _appearAnimationPlayed = false;
            beatenGlowingVFX.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _pointerEntered = true;
            
            if (_appearAnimationPlayed) 
                beatenGlowingVFX.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _pointerEntered = false;
            beatenGlowingVFX.SetActive(false);
        }
        
        public void SetAnimPlayed()
        {
            _appearAnimationPlayed = true;

            if (_pointerEntered) 
                beatenGlowingVFX.SetActive(true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_appearAnimationPlayed && GameManager.IsBeaten) 
                _animator.SetTrigger(Pressed);
        }
    }
}