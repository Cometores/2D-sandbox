using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncreaseBtn : Bank
{
    [SerializeField] Multiplier clickMultiplier;

    [SerializeField] GameObject clickCounterObj;
    [SerializeField] GameObject clickCostObj;

    static TextMeshProUGUI clickCounterText;
    static TextMeshProUGUI clickCostText;

    int normalClickCost;
    int clickCnt;

    void Awake()
    {
        normalClickCost = clickMultiplier.baseCost;

        clickCounterText = clickCounterObj.GetComponent<TextMeshProUGUI>();
        clickCostText = clickCostObj.GetComponent<TextMeshProUGUI>();
    }

    public void IncreaseOnClick()
    {
        if (Bank.account >= normalClickCost && clickCnt < clickMultiplier.maxAmount)
        {
            Bank.account -= normalClickCost;

            // Cost changes && amount changes
            normalClickCost = (int)(clickMultiplier.baseCost * Mathf.Pow(clickMultiplier.multiplier, clickCnt));
            clickCnt++;

            clickCounterText.text = $"{clickCnt}";
            clickCostText.text = $"Cost: {normalClickCost} ˆ";
            Bank.clickPrice++;
        }
    }
}
