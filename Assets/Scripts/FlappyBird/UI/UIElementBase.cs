using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FlappyBird.UI
{
    public class UIElementBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Colors")] 
        [SerializeField] private Color normalColor;
        [SerializeField] private Color hoveredColor;
        
        protected bool IsHovered;
        
        protected Image Image;

        protected virtual void Awake()
        {
            Image = GetComponent<Image>();
        }

        public virtual void OnPointerEnter(PointerEventData eventData) => Image.color = hoveredColor;
        public virtual void OnPointerExit(PointerEventData eventData) => Image.color = normalColor;

    }
}
