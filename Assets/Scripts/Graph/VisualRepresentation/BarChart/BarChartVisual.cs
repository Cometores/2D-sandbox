using Graph.Contracts;
using Shared;
using UnityEngine;
using UnityEngine.UI;

namespace Graph.VisualRepresentation.BarChart
{
    /// <summary>
    /// Displays data points as a Bar Chart
    /// </summary>
    public class BarChartVisual : IGraphVisual
    {
        private RectTransform _graphContainer;
        private Color _barColor;
        private float _barWidthMultiplier;

        public BarChartVisual(RectTransform graphContainer, Color barColor, float barWidthMultiplier)
        {
            _graphContainer = graphContainer;
            _barColor = barColor;
            _barWidthMultiplier = barWidthMultiplier;
        }

        public void CleanUp()
        {
        }

        public IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth,
            string tooltipText)
        {
            GameObject barGameObject = CreateBar(graphPosition, graphPositionWidth);

            BarChartVisualObject barChartVisualObject = new BarChartVisualObject(barGameObject, _barWidthMultiplier);
            barChartVisualObject.SetGraphVisualObjectInfo(graphPosition, graphPositionWidth, tooltipText);

            return barChartVisualObject;
        }

        private GameObject CreateBar(Vector2 graphPosition, float barWidth)
        {
            GameObject gameObject = new GameObject("bar", typeof(Image));
            gameObject.transform.SetParent(_graphContainer, false);
            gameObject.GetComponent<Image>().color = _barColor;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(graphPosition.x, 0f);
            rectTransform.sizeDelta = new Vector2(barWidth * _barWidthMultiplier, graphPosition.y);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.pivot = new Vector2(.5f, 0f);

            // Add Button_UI Component which captures UI Mouse Events
            gameObject.AddComponent<ButtonUI>();

            return gameObject;
        }
    }
}
