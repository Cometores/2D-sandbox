using FlappyBird.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FlappyBird.UI
{
    public class MenuButton : ButtonBase
    {
        [SerializeField] private GameObject hoveredVFX;
        
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            AudioManager.Instance?.PlayUIHover();
            hoveredVFX.SetActive(true);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            hoveredVFX.SetActive(false);
        }

        protected override void OnDisable()
        {
            hoveredVFX.SetActive(false);
            base.OnDisable();
        }
    }
}