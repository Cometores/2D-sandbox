using Graph.Contracts;
using Shared;
using UnityEngine;

namespace Graph.VisualRepresentation.BarChart
{
    public class BarChartVisualObject : IGraphVisualObject
    {
        private GameObject _barGameObject;
        private float _barWidthMultiplier;

        public BarChartVisualObject(GameObject barGameObject, float barWidthMultiplier)
        {
            _barGameObject = barGameObject;
            _barWidthMultiplier = barWidthMultiplier;
        }

        public void SetGraphVisualObjectInfo(Vector2 graphPosition, float graphPositionWidth, string tooltipText)
        {
            RectTransform rectTransform = _barGameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(graphPosition.x, 0f);
            rectTransform.sizeDelta = new Vector2(graphPositionWidth * _barWidthMultiplier, graphPosition.y);

            ButtonUI barButtonUI = _barGameObject.GetComponent<ButtonUI>();

            // Show Tooltip on Mouse Over
            barButtonUI.MouseOverOnceFunc = () => { WindowGraph.ShowTooltip_Static(tooltipText, graphPosition); };

            // Hide Tooltip on Mouse Out
            barButtonUI.MouseOutOnceFunc = WindowGraph.HideTooltip_Static;
        }

        public void CleanUp()
        {
            Object.Destroy(_barGameObject);
        }
    }
}
