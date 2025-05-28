using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScientistSpawner : Bank
{
    [SerializeField] Multiplier scientistMultiplier;
    [SerializeField] GameObject scientist;
    [SerializeField] GameObject max;
    [SerializeField] GameObject min;

    Vector3 xyMax;
    Vector3 xyMin;

    [SerializeField] GameObject scientistCounterObj;
    [SerializeField] GameObject scientistCostObj;

    static TextMeshProUGUI scientistCounterText;
    static TextMeshProUGUI scientistCostText;

    int normalScientistCost;
    int scientistCnt;

    void Awake()
    {
        normalScientistCost = scientistMultiplier.baseCost;
        xyMax = max.transform.position;
        xyMin = min.transform.position;

        scientistCounterText = scientistCounterObj.GetComponent<TextMeshProUGUI>();
        scientistCostText = scientistCostObj.GetComponent<TextMeshProUGUI>();
    }

    public void ScientistOnClick()
    {
        if (Bank.account >= normalScientistCost && scientistCnt < scientistMultiplier.maxAmount)
        {
            // Spawn Scientist
            Vector3 newSpawnPos = new Vector3(Random.Range(xyMin.x, xyMax.x), Random.Range(xyMin.y, xyMax.y), 0);
            scientist.GetComponent<ScientistMovement>().xyMin = xyMin;
            scientist.GetComponent<ScientistMovement>().xyMax = xyMax;
            GameObject newScientist = Instantiate(scientist, newSpawnPos, Quaternion.identity);

            Bank.account -= normalScientistCost;

            // Cost changes && amount changes
            normalScientistCost = (int)(scientistMultiplier.baseCost * Mathf.Pow(scientistMultiplier.multiplier, scientistCnt));
            scientistCnt++;

            scientistCounterText.text = $"{scientistCnt}";
            scientistCostText.text = $"Cost: {normalScientistCost} ˆ";

            Bank.amountPerSec += Bank.studentPrice;
            Bank.UpdateAmountPerSec();
        }
    }
}
