using TMPro;
using UnityEngine;

namespace CookieClicker
{
    public class IncreaseBtn : Bank
    {
        [SerializeField] private Multiplier clickMultiplier;
        [SerializeField] private GameObject clickCounterObj;
        [SerializeField] private GameObject clickCostObj;

        private static TextMeshProUGUI _clickCounterText;
        private static TextMeshProUGUI _clickCostText;

        private int _normalClickCost;
        private int _clickCnt;

        private void Awake()
        {
            _normalClickCost = clickMultiplier.baseCost;

            _clickCounterText = clickCounterObj.GetComponent<TextMeshProUGUI>();
            _clickCostText = clickCostObj.GetComponent<TextMeshProUGUI>();
        }

        public void IncreaseOnClick()
        {
            if (Account >= _normalClickCost && _clickCnt < clickMultiplier.maxAmount)
            {
                Account -= _normalClickCost;

                // Cost changes && amount changes
                _normalClickCost = (int)(clickMultiplier.baseCost * Mathf.Pow(clickMultiplier.multiplier, _clickCnt));
                _clickCnt++;

                _clickCounterText.text = $"{_clickCnt}";
                _clickCostText.text = $"Cost: {_normalClickCost} �";
                ClickPrice++;
            }
        }
    }
}
