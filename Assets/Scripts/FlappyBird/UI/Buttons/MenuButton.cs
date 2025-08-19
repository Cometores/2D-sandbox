using System;
using FlappyBird.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FlappyBird.UI.Buttons
{
    [RequireComponent(typeof(Animator))]
    public class MenuButton : ButtonBase
    {
        public event Action Selected;
        public event Action Unselected;
        
        private Animator _animator;
        private static readonly int SelectedHash = Animator.StringToHash("selected");

        protected override void Awake()
        {
            base.Awake();
            
            _animator = GetComponent<Animator>();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            
            AudioManager.Instance?.PlayRandomUIHover();
            _animator.SetTrigger(SelectedHash);
            
            Selected?.Invoke();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            
            Unselected?.Invoke();
        }

        protected override void OnDisable()
        {
            Unselected?.Invoke();
            base.OnDisable();
        }
    }
}