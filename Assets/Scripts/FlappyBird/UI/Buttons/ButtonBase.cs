using FlappyBird.Config;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FlappyBird.UI.Buttons
{
    [RequireComponent(typeof(Image))]
    public class ButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private StylesConfig style;
        protected Image Image;

        protected virtual void Awake() => Image = GetComponent<Image>();

        #region Color
        
        public virtual void OnPointerEnter(PointerEventData eventData) => Image.color = style.hoveredColor;
        public virtual void OnPointerExit(PointerEventData eventData) => Image.color = style.normalColor;
        protected virtual void OnDisable() => Image.color = style.normalColor;
        
        #endregion
    }
}
