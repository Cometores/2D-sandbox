using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shared
{
    /// <summary>
    /// Button in the UI
    /// </summary>
    /// <remarks>Took from Code Monkey</remarks>
    public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        public Action ClickFunc = null;
        public Action MouseRightClickFunc = null;
        public Action MouseMiddleClickFunc = null;
        public Action MouseDownOnceFunc = null;
        public Action MouseUpFunc = null;
        public Action MouseOverOnceTooltipFunc = null;
        public Action MouseOutOnceTooltipFunc = null;
        public Action MouseOverOnceFunc = null;
        public Action MouseOutOnceFunc = null;
        public Action MouseOverFunc = null;
        public Action MouseOverPerSecFunc = null; //Triggers every sec if mouseOver
        public Action MouseUpdate = null;
        public Action<PointerEventData> OnPointerClickFunc;

        public enum HoverBehaviour
        {
            CUSTOM,
            CHANGE_COLOR,
            CHANGE_COLOR_AUTO,
            CHANGE_IMAGE,
            CHANGE_SET_ACTIVE,
        }

        public HoverBehaviour HoverBehaviourType = HoverBehaviour.CUSTOM;
        private Action _hoverBehaviourFuncEnter, _hoverBehaviourFuncExit;

        public Color HoverBehaviourColorEnter;
        public Color HoverBehaviourColorExit;
        public Image HoverBehaviourImage;
        public Sprite HoverBehaviourSpriteExit;
        public Sprite HoverBehaviourSpriteEnter;
        public bool HoverBehaviourMove = false;
        public Vector2 HoverBehaviourMoveAmount = Vector2.zero;
        private Vector2 _posExit, _posEnter;
        public bool TriggerMouseOutFuncOnClick = false;
        private bool _mouseOver;
        private float _mouseOverPerSecFuncTimer;

        private Action _internalOnPointerEnterFunc = null;
        private Action _internalOnPointerExitFunc = null;
        private Action _internalOnPointerClickFunc = null;


        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (_internalOnPointerEnterFunc != null) _internalOnPointerEnterFunc();
            if (HoverBehaviourMove) transform.GetComponent<RectTransform>().anchoredPosition = _posEnter;
            if (_hoverBehaviourFuncEnter != null) _hoverBehaviourFuncEnter();
            if (MouseOverOnceFunc != null) MouseOverOnceFunc();
            if (MouseOverOnceTooltipFunc != null) MouseOverOnceTooltipFunc();
            _mouseOver = true;
            _mouseOverPerSecFuncTimer = 0f;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (_internalOnPointerExitFunc != null) _internalOnPointerExitFunc();
            if (HoverBehaviourMove) transform.GetComponent<RectTransform>().anchoredPosition = _posExit;
            if (_hoverBehaviourFuncExit != null) _hoverBehaviourFuncExit();
            if (MouseOutOnceFunc != null) MouseOutOnceFunc();
            if (MouseOutOnceTooltipFunc != null) MouseOutOnceTooltipFunc();
            _mouseOver = false;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (_internalOnPointerClickFunc != null) _internalOnPointerClickFunc();
            if (OnPointerClickFunc != null) OnPointerClickFunc(eventData);
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (TriggerMouseOutFuncOnClick)
                {
                    OnPointerExit(eventData);
                }

                if (ClickFunc != null) ClickFunc();
            }

            if (eventData.button == PointerEventData.InputButton.Right)
                if (MouseRightClickFunc != null)
                    MouseRightClickFunc();
            if (eventData.button == PointerEventData.InputButton.Middle)
                if (MouseMiddleClickFunc != null)
                    MouseMiddleClickFunc();
        }

        public void Manual_OnPointerExit() => OnPointerExit(null);

        public bool IsMouseOver() => _mouseOver;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (MouseDownOnceFunc != null) MouseDownOnceFunc();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (MouseUpFunc != null) MouseUpFunc();
        }

        private void Update()
        {
            if (_mouseOver)
            {
                if (MouseOverFunc != null) MouseOverFunc();
                _mouseOverPerSecFuncTimer -= Time.unscaledDeltaTime;
                if (_mouseOverPerSecFuncTimer <= 0)
                {
                    _mouseOverPerSecFuncTimer += 1f;
                    if (MouseOverPerSecFunc != null) MouseOverPerSecFunc();
                }
            }

            if (MouseUpdate != null) MouseUpdate();
        }

        private void Awake()
        {
            _posExit = transform.GetComponent<RectTransform>().anchoredPosition;
            _posEnter = transform.GetComponent<RectTransform>().anchoredPosition + HoverBehaviourMoveAmount;
            SetHoverBehaviourType(HoverBehaviourType);
        }

        public void SetHoverBehaviourType(HoverBehaviour hoverBehaviourType)
        {
            HoverBehaviourType = hoverBehaviourType;
            switch (hoverBehaviourType)
            {
                case HoverBehaviour.CHANGE_COLOR:
                    _hoverBehaviourFuncEnter = delegate() { HoverBehaviourImage.color = HoverBehaviourColorEnter; };
                    _hoverBehaviourFuncExit = delegate() { HoverBehaviourImage.color = HoverBehaviourColorExit; };
                    break;
                case HoverBehaviour.CHANGE_IMAGE:
                    _hoverBehaviourFuncEnter = delegate()
                    {
                        HoverBehaviourImage.sprite = HoverBehaviourSpriteEnter;
                    };
                    _hoverBehaviourFuncExit = delegate() { HoverBehaviourImage.sprite = HoverBehaviourSpriteExit; };
                    break;
                case HoverBehaviour.CHANGE_SET_ACTIVE:
                    _hoverBehaviourFuncEnter = delegate() { HoverBehaviourImage.gameObject.SetActive(true); };
                    _hoverBehaviourFuncExit = delegate() { HoverBehaviourImage.gameObject.SetActive(false); };
                    break;
                case HoverBehaviour.CHANGE_COLOR_AUTO:
                    Color color = HoverBehaviourImage.color;
                    if (color.r >= 1f) color.r = .9f;
                    if (color.g >= 1f) color.g = .9f;
                    if (color.b >= 1f) color.b = .9f;
                    Color colorOver = color * 1.3f; // Over color lighter
                    _hoverBehaviourFuncEnter = delegate() { HoverBehaviourImage.color = colorOver; };
                    _hoverBehaviourFuncExit = delegate() { HoverBehaviourImage.color = color; };
                    break;
            }
        }

        public void RefreshHoverBehaviourType() => SetHoverBehaviourType(HoverBehaviourType);

        /// <summary>
        /// Class for temporarily intercepting a button action
        /// Useful for Tutorial disabling specific buttons
        /// </summary>
        public class InterceptActionHandler
        {
            private Action _removeInterceptFunc;

            public InterceptActionHandler(Action removeInterceptFunc)
            {
                _removeInterceptFunc = removeInterceptFunc;
            }

            public void RemoveIntercept() => _removeInterceptFunc();
        }

        public InterceptActionHandler InterceptActionClick(Func<bool> testPassthroughFunc) =>
            InterceptAction("ClickFunc", testPassthroughFunc);

        public InterceptActionHandler InterceptAction(string fieldName, Func<bool> testPassthroughFunc) =>
            InterceptAction(GetType().GetField(fieldName), testPassthroughFunc);

        public InterceptActionHandler InterceptAction(System.Reflection.FieldInfo fieldInfo,
            Func<bool> testPassthroughFunc)
        {
            Action backFunc = fieldInfo.GetValue(this) as Action;
            InterceptActionHandler interceptActionHandler =
                new InterceptActionHandler(() => fieldInfo.SetValue(this, backFunc));
            fieldInfo.SetValue(this, (Action)delegate()
            {
                if (testPassthroughFunc())
                {
                    // Passthrough
                    interceptActionHandler.RemoveIntercept();
                    backFunc();
                }
            });

            return interceptActionHandler;
        }
    }
}
