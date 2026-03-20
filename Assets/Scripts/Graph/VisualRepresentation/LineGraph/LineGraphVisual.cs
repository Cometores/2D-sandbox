using Graph.Contracts;
using Shared;
using UnityEngine;
using UnityEngine.UI;

namespace Graph.VisualRepresentation.LineGraph
{
    /// <summary>
    /// Displays data points as a Line Graph
    /// </summary>
    public class LineGraphVisual : IGraphVisual
    {
        private RectTransform _graphContainer;
        private Sprite _dotSprite;
        private LineGraphVisualObject _lastLineGraphVisualObject;
        private Color _dotColor;
        private Color _dotConnectionColor;

        public LineGraphVisual(RectTransform graphContainer, Sprite dotSprite, Color dotColor, Color dotConnectionColor)
        {
            _graphContainer = graphContainer;
            _dotSprite = dotSprite;
            _dotColor = dotColor;
            _dotConnectionColor = dotConnectionColor;
            _lastLineGraphVisualObject = null;
        }

        public void CleanUp()
        {
            _lastLineGraphVisualObject = null;
        }


        public IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth,
            string tooltipText)
        {
            GameObject dotGameObject = CreateDot(graphPosition);


            GameObject dotConnectionGameObject = null;
            if (_lastLineGraphVisualObject != null)
            {
                dotConnectionGameObject = CreateDotConnection(_lastLineGraphVisualObject.GetGraphPosition(),
                    dotGameObject.GetComponent<RectTransform>().anchoredPosition);
            }

            LineGraphVisualObject lineGraphVisualObject =
                new LineGraphVisualObject(dotGameObject, dotConnectionGameObject, _lastLineGraphVisualObject);
            lineGraphVisualObject.SetGraphVisualObjectInfo(graphPosition, graphPositionWidth, tooltipText);

            _lastLineGraphVisualObject = lineGraphVisualObject;

            return lineGraphVisualObject;
        }

        private GameObject CreateDot(Vector2 anchoredPosition)
        {
            GameObject gameObject = new GameObject("dot", typeof(Image));
            gameObject.transform.SetParent(_graphContainer, false);
            gameObject.GetComponent<Image>().sprite = _dotSprite;
            gameObject.GetComponent<Image>().color = _dotColor;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.sizeDelta = new Vector2(11, 11);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);

            // Add Button_UI Component which captures UI Mouse Events
            gameObject.AddComponent<ButtonUI>();

            return gameObject;
        }

        private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
        {
            GameObject gameObject = new GameObject("dotConnection", typeof(Image));
            gameObject.transform.SetParent(_graphContainer, false);
            gameObject.GetComponent<Image>().color = _dotConnectionColor;
            gameObject.GetComponent<Image>().raycastTarget = false;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            Vector2 dir = (dotPositionB - dotPositionA).normalized;
            float distance = Vector2.Distance(dotPositionA, dotPositionB);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.sizeDelta = new Vector2(distance, 3f);
            rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
            rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
            return gameObject;
        }
    }
}
