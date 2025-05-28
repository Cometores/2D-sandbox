using TMPro;
using UnityEngine;

namespace CookieClicker
{
    public class ScientistSpawner : Bank
    {
        [SerializeField] private Multiplier scientistMultiplier;
        [SerializeField] private GameObject scientist;
        [SerializeField] private GameObject max;
        [SerializeField] private GameObject min;

        private Vector3 xyMax;
        private Vector3 xyMin;

        [SerializeField] private GameObject scientistCounterObj;
        [SerializeField] private GameObject scientistCostObj;

        private static TextMeshProUGUI scientistCounterText;
        private static TextMeshProUGUI scientistCostText;

        private int normalScientistCost;
        private int scientistCnt;

        private void Awake()
        {
            normalScientistCost = scientistMultiplier.baseCost;
            xyMax = max.transform.position;
            xyMin = min.transform.position;

            scientistCounterText = scientistCounterObj.GetComponent<TextMeshProUGUI>();
            scientistCostText = scientistCostObj.GetComponent<TextMeshProUGUI>();
        }

        public void ScientistOnClick()
        {
            if (Bank.Account >= normalScientistCost && scientistCnt < scientistMultiplier.maxAmount)
            {
                // Spawn Scientist
                Vector3 newSpawnPos = new Vector3(Random.Range(xyMin.x, xyMax.x), Random.Range(xyMin.y, xyMax.y), 0);
                scientist.GetComponent<ScientistMovement>().xyMin = xyMin;
                scientist.GetComponent<ScientistMovement>().xyMax = xyMax;
                GameObject newScientist = Instantiate(scientist, newSpawnPos, Quaternion.identity);

                Bank.Account -= normalScientistCost;

                // Cost changes && amount changes
                normalScientistCost = (int)(scientistMultiplier.baseCost * Mathf.Pow(scientistMultiplier.multiplier, scientistCnt));
                scientistCnt++;

                scientistCounterText.text = $"{scientistCnt}";
                scientistCostText.text = $"Cost: {normalScientistCost} �";

                Bank.AmountPerSec += Bank.StudentPrice;
                Bank.UpdateAmountPerSec();
            }
        }
    }
}
