using System;
using System.Collections.Generic;
using Graph.Contracts;
using Graph.VisualRepresentation.BarChart;
using Graph.VisualRepresentation.LineGraph;
using Shared;
using UnityEngine;
using UnityEngine.UI;

namespace Graph
{
    public class WindowGraph : MonoBehaviour
    {
        private static WindowGraph _instance;

        [SerializeField] private Sprite _dotSprite;

        private RectTransform _graphContainer;
        private RectTransform _labelTemplateX;
        private RectTransform _labelTemplateY;
        private RectTransform _dashContainer;
        private RectTransform _dashTemplateX;
        private RectTransform _dashTemplateY;

        private List<GameObject> _gameObjectList;
        private List<IGraphVisualObject> _graphVisualObjectList;

        private GameObject _tooltipGameObject;
        private List<RectTransform> _yLabelList;

        // Cached values
        private List<int> _valueList;
        private IGraphVisual _graphVisual;
        private int _maxVisibleValueAmount;
        private Func<int, string> _getAxisLabelX;
        private Func<float, string> _getAxisLabelY;
        private float _xSize;
        private bool _startYScaleAtZero;

        private void Awake()
        {
            _instance = this;

            // Grab base objects references
            _graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
            _labelTemplateX = _graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
            _labelTemplateY = _graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
            _dashContainer = _graphContainer.Find("dashContainer").GetComponent<RectTransform>();
            _dashTemplateX = _dashContainer.Find("dashTemplateX").GetComponent<RectTransform>();
            _dashTemplateY = _dashContainer.Find("dashTemplateY").GetComponent<RectTransform>();
            _tooltipGameObject = _graphContainer.Find("tooltip").gameObject;

            _startYScaleAtZero = true;
            _gameObjectList = new List<GameObject>();
            _yLabelList = new List<RectTransform>();
            _graphVisualObjectList = new List<IGraphVisualObject>();

            IGraphVisual lineGraphVisual =
                new LineGraphVisual(_graphContainer, _dotSprite, Color.yellow, new Color(1, 1, 1, .5f));
            IGraphVisual barChartVisual = new BarChartVisual(_graphContainer, Color.white, .8f);

            // Set up buttons
            transform.Find("barChartBtn").GetComponent<ButtonUI>().ClickFunc = () => SetGraphVisual(barChartVisual);
            transform.Find("lineGraphBtn").GetComponent<ButtonUI>().ClickFunc = () => SetGraphVisual(lineGraphVisual);

            transform.Find("decreaseVisibleAmountBtn").GetComponent<ButtonUI>().ClickFunc = DecreaseVisibleAmount;
            transform.Find("increaseVisibleAmountBtn").GetComponent<ButtonUI>().ClickFunc = IncreaseVisibleAmount;

            transform.Find("dollarBtn").GetComponent<ButtonUI>().ClickFunc = ()
                => SetGetAxisLabelY(f => "$" + Mathf.RoundToInt(f));
            transform.Find("euroBtn").GetComponent<ButtonUI>().ClickFunc = ()
                => SetGetAxisLabelY(f => "€" + Mathf.RoundToInt(f / 1.18f));

            HideTooltip();

            // Set up base values
            List<int> valueList = new List<int> { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33 };
            ShowGraph(valueList, barChartVisual, -1, i => "Day " + (i + 1),
                f => "$" + Mathf.RoundToInt(f));

            /* You can uncomment one for fun */
            // UpdateGraphValuesPeriodically(barChartVisual, lineGraphVisual);
            // IncreaseAllPeriodically();
        }

        #region ToolTip

        public static void ShowTooltip_Static(string tooltipText, Vector2 anchoredPosition) =>
            _instance.ShowTooltip(tooltipText, anchoredPosition);

        public static void HideTooltip_Static() => _instance.HideTooltip();

        /// <summary> Hides the currently displayed tooltip. </summary>
        private void HideTooltip() => _tooltipGameObject.SetActive(false);

        /// <summary>
        /// Displays a tooltip with the specified text at the specified anchored position.
        /// </summary>
        /// <param name="tooltipText">The text to display on the tooltip.</param>
        /// <param name="anchoredPosition">The anchored position of the tooltip on the screen.</param>
        /// <remarks>Background size is calculated based on the text size.</remarks>
        private void ShowTooltip(string tooltipText, Vector2 anchoredPosition)
        {
            _tooltipGameObject.SetActive(true);
            _tooltipGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;

            var tooltipUIText = _tooltipGameObject.transform.Find("text").GetComponent<Text>();
            tooltipUIText.text = tooltipText;

            var backgroundRectTransform = _tooltipGameObject.transform.Find("background").GetComponent<RectTransform>();
            backgroundRectTransform.sizeDelta = GetPreferredSizeFromText(tooltipUIText);

            // UI Visibility Sorting based on Hierarchy, SetAsLastSibling to show up on top
            _tooltipGameObject.transform.SetAsLastSibling();
        }

        private static Vector2 GetPreferredSizeFromText(Text tooltipUIText)
        {
            float textPaddingSize = 4f;
            Vector2 backgroundSize = new Vector2(
                tooltipUIText.preferredWidth + textPaddingSize * 2f,
                tooltipUIText.preferredHeight + textPaddingSize * 2f
            );
            return backgroundSize;
        }

        #endregion

        #region Redraw Graph

        /// <summary><see cref="ShowGraph" /> with optional parameters.</summary>
        private void RedrawGraph(
            List<int> valueList = null,
            IGraphVisual graphVisual = null,
            int? maxVisibleValueAmount = null,
            Func<int, string> getAxisLabelX = null,
            Func<float, string> getAxisLabelY = null)
        {
            ShowGraph(
                valueList ?? _valueList,
                graphVisual ?? _graphVisual,
                maxVisibleValueAmount ?? _maxVisibleValueAmount,
                getAxisLabelX ?? _getAxisLabelX,
                getAxisLabelY ?? _getAxisLabelY);
        }

        private void SetGetAxisLabelX(Func<int, string> getAxisLabelX) => RedrawGraph(getAxisLabelX: getAxisLabelX);

        private void SetGetAxisLabelY(Func<float, string> getAxisLabelY) => RedrawGraph(getAxisLabelY: getAxisLabelY);

        private void IncreaseVisibleAmount() => RedrawGraph(maxVisibleValueAmount: _maxVisibleValueAmount + 1);

        private void DecreaseVisibleAmount() => RedrawGraph(maxVisibleValueAmount: _maxVisibleValueAmount - 1);

        private void SetGraphVisual(IGraphVisual graphVisual) => RedrawGraph(graphVisual: graphVisual);

        #endregion

        #region Graph Drawing

        private void ShowGraph(
            List<int> valueList,
            IGraphVisual graphVisual,
            int maxVisibleValueAmount = -1,
            Func<int, string> getAxisLabelX = null,
            Func<float, string> getAxisLabelY = null)
        {
            _valueList = valueList;
            _graphVisual = graphVisual;
            _getAxisLabelX = getAxisLabelX;
            _getAxisLabelY = getAxisLabelY;

            if (maxVisibleValueAmount <= 0)
            {
                // Show all if no amount specified
                maxVisibleValueAmount = valueList.Count;
            }

            if (maxVisibleValueAmount > valueList.Count)
            {
                // Validate the amount to show the maximum
                maxVisibleValueAmount = 1;
            }

            _maxVisibleValueAmount = maxVisibleValueAmount;

            // Test for label defaults
            getAxisLabelX ??= i => i.ToString();
            getAxisLabelY ??= f => Mathf.RoundToInt(f).ToString();

            CleanUpGraphVisuals(graphVisual);

            // Grab the width and height from the container
            float graphWidth = _graphContainer.sizeDelta.x;
            float graphHeight = _graphContainer.sizeDelta.y;

            CalculateYScale(out var yMinimum, out var yMaximum);

            // Set the distance between each point on the graph
            _xSize = graphWidth / (maxVisibleValueAmount + 1);

            // Cycle through all visible data points
            int xIndex = 0;
            for (int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++)
            {
                float xPosition = _xSize + xIndex * _xSize;
                float yPosition = ((valueList[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight;

                // Add data point visual
                string tooltipText = getAxisLabelY(valueList[i]);
                IGraphVisualObject graphVisualObject =
                    graphVisual.CreateGraphVisualObject(new Vector2(xPosition, yPosition), _xSize, tooltipText);
                _graphVisualObjectList.Add(graphVisualObject);

                // Duplicate the x label template
                RectTransform labelX = Instantiate(_labelTemplateX, _graphContainer, false);
                labelX.gameObject.SetActive(true);
                labelX.anchoredPosition = new Vector2(xPosition, -7f);
                labelX.GetComponent<Text>().text = getAxisLabelX(i);
                _gameObjectList.Add(labelX.gameObject);

                // Duplicate the x dash template
                RectTransform dashX = Instantiate(_dashTemplateX, _dashContainer, false);
                dashX.gameObject.SetActive(true);
                dashX.anchoredPosition = new Vector2(xPosition, -3f);
                _gameObjectList.Add(dashX.gameObject);

                xIndex++;
            }

            // Set up separators on the y-axis
            int separatorCount = 10;
            for (int i = 0; i <= separatorCount; i++)
            {
                // Duplicate the label template
                RectTransform labelY = Instantiate(_labelTemplateY, _graphContainer, false);
                labelY.gameObject.SetActive(true);
                float normalizedValue = i * 1f / separatorCount;
                labelY.anchoredPosition = new Vector2(-20f, normalizedValue * graphHeight);
                labelY.GetComponent<Text>().text = getAxisLabelY(yMinimum + (normalizedValue * (yMaximum - yMinimum)));
                _yLabelList.Add(labelY);
                _gameObjectList.Add(labelY.gameObject);

                // Duplicate the dash template
                RectTransform dashY = Instantiate(_dashTemplateY, _dashContainer, false);
                dashY.gameObject.SetActive(true);
                dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);
                _gameObjectList.Add(dashY.gameObject);
            }
        }


        private void CalculateYScale(out float yMinimum, out float yMaximum)
        {
            // Identify y Min and Max values
            yMaximum = _valueList[0];
            yMinimum = _valueList[0];

            for (int i = Mathf.Max(_valueList.Count - _maxVisibleValueAmount, 0); i < _valueList.Count; i++)
            {
                int value = _valueList[i];
                if (value > yMaximum)
                {
                    yMaximum = value;
                }

                if (value < yMinimum)
                {
                    yMinimum = value;
                }
            }

            float yDifference = yMaximum - yMinimum;
            if (yDifference <= 0)
            {
                yDifference = 5f;
            }

            yMaximum += (yDifference * 0.2f);
            yMinimum -= (yDifference * 0.2f);

            if (_startYScaleAtZero)
            {
                yMinimum = 0f; // Start the graph at zero
            }
        }

        #endregion

        #region Cleaning Up

        private void CleanUpGraphVisuals(IGraphVisual graphVisual)
        {
            // Clean up previous graph
            foreach (GameObject graphGameObject in _gameObjectList)
                Destroy(graphGameObject);

            _gameObjectList.Clear();
            _yLabelList.Clear();

            foreach (IGraphVisualObject graphVisualObject in _graphVisualObjectList)
                graphVisualObject.CleanUp();

            _graphVisualObjectList.Clear();
            graphVisual.CleanUp();
        }

        #endregion

        #region Changing Graph Values

        /// <summary>
        /// Updates a value in the graph's data set and refreshes the graph visualization.
        /// </summary>
        /// <param name="index">The index of the value in the data set to update.</param>
        /// <param name="value">The new value to set at the specified index.</param>
        public void UpdateValue(int index, int value)
        {
            CalculateYScale(out var yMinimumBefore, out var yMaximumBefore);

            _valueList[index] = value;

            float graphWidth = _graphContainer.sizeDelta.x;
            float graphHeight = _graphContainer.sizeDelta.y;

            CalculateYScale(out var yMinimum, out var yMaximum);

            bool yScaleChanged = !Mathf.Approximately(yMinimumBefore, yMinimum) ||
                                 !Mathf.Approximately(yMaximumBefore, yMaximum);

            if (!yScaleChanged)
            {
                // Y Scale did not change, update only this value
                float xPosition = _xSize + index * _xSize;
                float yPosition = ((value - yMinimum) / (yMaximum - yMinimum)) * graphHeight;

                // Add data point visual
                string tooltipText = _getAxisLabelY(value);
                _graphVisualObjectList[index]
                    .SetGraphVisualObjectInfo(new Vector2(xPosition, yPosition), _xSize, tooltipText);

                return;
            }

            // Y scale changed, update the whole graph and y-axis labels
            // Cycle through all visible data points
            int xIndex = 0;
            for (int i = Mathf.Max(_valueList.Count - _maxVisibleValueAmount, 0); i < _valueList.Count; i++)
            {
                float xPosition = _xSize + xIndex * _xSize;
                float yPosition = ((_valueList[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight;

                // Add data point visual
                string tooltipText = _getAxisLabelY(_valueList[i]);
                _graphVisualObjectList[xIndex]
                    .SetGraphVisualObjectInfo(new Vector2(xPosition, yPosition), _xSize, tooltipText);

                xIndex++;
            }

            for (int i = 0; i < _yLabelList.Count; i++)
            {
                float normalizedValue = i * 1f / _yLabelList.Count;
                _yLabelList[i].GetComponent<Text>().text =
                    _getAxisLabelY(yMinimum + (normalizedValue * (yMaximum - yMinimum)));
            }
        }

        /// <summary>
        /// Function for visual tests.
        /// </summary>
        public void IncreaseAllPeriodically()
        {
            int index = 0;
            FunctionPeriodic.Create(() => { index = (index + 1) % _valueList.Count; }, .1f);
            FunctionPeriodic.Create(() =>
            {
                //int index = UnityEngine.Random.Range(0, valueList.Count);
                UpdateValue(index, _valueList[index] + UnityEngine.Random.Range(1, 3));
            }, .02f);
        }

        /// <summary>
        ///  Function for visual tests.
        /// </summary>
        public void UpdateGraphValuesPeriodically(IGraphVisual barChartVisual, IGraphVisual lineGraphVisual)
        {
            // Automatically modify graph values and visual
            bool useBarChart = true;
            FunctionPeriodic.Create(() =>
                {
                    for (int i = 0; i < _valueList.Count; i++)
                    {
                        _valueList[i] = Mathf.RoundToInt(_valueList[i] * UnityEngine.Random.Range(0.8f, 1.2f));
                        if (_valueList[i] < 0) _valueList[i] = 0;
                    }

                    if (useBarChart)
                    {
                        ShowGraph(_valueList, barChartVisual, -1, i => "Day " + (i + 1),
                            f => "$" + Mathf.RoundToInt(f));
                    }
                    else
                    {
                        ShowGraph(_valueList, lineGraphVisual, -1, i => "Day " + (i + 1),
                            f => "$" + Mathf.RoundToInt(f));
                    }

                    useBarChart = !useBarChart;
                },
                .5f);
        }

        #endregion
    }
}
