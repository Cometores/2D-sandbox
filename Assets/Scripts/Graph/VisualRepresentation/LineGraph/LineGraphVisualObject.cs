using System;
using Graph.Contracts;
using Shared;
using UnityEngine;

namespace Graph.VisualRepresentation.LineGraph
{
    public class LineGraphVisualObject : IGraphVisualObject
    {
        public event EventHandler OnChangedGraphVisualObjectInfo;

        private GameObject _dotGameObject;
        private GameObject _dotConnectionGameObject;
        private LineGraphVisualObject _lastVisualObject;

        public LineGraphVisualObject(GameObject dotGameObject, GameObject dotConnectionGameObject,
            LineGraphVisualObject lastVisualObject)
        {
            _dotGameObject = dotGameObject;
            _dotConnectionGameObject = dotConnectionGameObject;
            _lastVisualObject = lastVisualObject;

            if (lastVisualObject != null)
            {
                lastVisualObject.OnChangedGraphVisualObjectInfo += LastVisualObject_OnChangedGraphVisualObjectInfo;
            }
        }

        private void LastVisualObject_OnChangedGraphVisualObjectInfo(object sender, EventArgs e)
        {
            UpdateDotConnection();
        }

        public void SetGraphVisualObjectInfo(Vector2 graphPosition, float graphPositionWidth, string tooltipText)
        {
            RectTransform rectTransform = _dotGameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = graphPosition;

            UpdateDotConnection();

            ButtonUI dotButtonUI = _dotGameObject.GetComponent<ButtonUI>();

            // Show Tooltip on Mouse Over
            dotButtonUI.MouseOverOnceFunc = () => WindowGraph.ShowTooltip_Static(tooltipText, graphPosition);

            // Hide Tooltip on Mouse Out
            dotButtonUI.MouseOutOnceFunc = WindowGraph.HideTooltip_Static;

            if (OnChangedGraphVisualObjectInfo != null) OnChangedGraphVisualObjectInfo(this, EventArgs.Empty);
        }

        public void CleanUp()
        {
            UnityEngine.Object.Destroy(_dotGameObject);
            UnityEngine.Object.Destroy(_dotConnectionGameObject);
        }

        public Vector2 GetGraphPosition()
        {
            RectTransform rectTransform = _dotGameObject.GetComponent<RectTransform>();
            return rectTransform.anchoredPosition;
        }

        private void UpdateDotConnection()
        {
            if (_dotConnectionGameObject != null)
            {
                RectTransform dotConnectionRectTransform = _dotConnectionGameObject.GetComponent<RectTransform>();
                Vector2 dir = (_lastVisualObject.GetGraphPosition() - GetGraphPosition()).normalized;
                float distance = Vector2.Distance(GetGraphPosition(), _lastVisualObject.GetGraphPosition());
                dotConnectionRectTransform.sizeDelta = new Vector2(distance, 3f);
                dotConnectionRectTransform.anchoredPosition = GetGraphPosition() + dir * distance * .5f;
                dotConnectionRectTransform.localEulerAngles =
                    new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
            }
        }
    }
}
