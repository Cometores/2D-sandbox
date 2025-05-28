using TMPro;
using UnityEngine;

namespace CookieClicker
{
    public class StudentSpawner : Bank
    {
        [SerializeField] private Multiplier studentMultiplier;
        [SerializeField] private GameObject student;
        [SerializeField] private GameObject max;
        [SerializeField] private GameObject min;

        private Vector3 xyMax;
        private Vector3 xyMin;

        [SerializeField] private GameObject studentCounterObj;
        [SerializeField] private GameObject studentCostObj;

        private static TextMeshProUGUI studentCounterText;
        private static TextMeshProUGUI studentCostText;

        private int studentCnt;
        private int normalStudentCost;

        private void Awake()
        {
            normalStudentCost = studentMultiplier.baseCost;
            xyMax = max.transform.position;
            xyMin = min.transform.position;

            studentCounterText = studentCounterObj.GetComponent<TextMeshProUGUI>();
            studentCostText = studentCostObj.GetComponent<TextMeshProUGUI>();
        }

        public void StudentOnClick()
        {
            if (Bank.Account >= normalStudentCost && studentCnt < studentMultiplier.maxAmount)
            {
                // Spawn Student
                Vector3 newSpawnPos = new Vector3(Random.Range(xyMin.x, xyMax.x), Random.Range(xyMin.y, xyMax.y), 0);
                Instantiate(student, newSpawnPos, Quaternion.identity);

                Bank.Account -= studentMultiplier.baseCost;

                // Cost changes && amount changes
                normalStudentCost = (int)(studentMultiplier.baseCost * Mathf.Pow(studentMultiplier.multiplier, studentCnt));
                studentCnt++;

                studentCounterText.text = $"{studentCnt}";
                studentCostText.text = $"Cost: {normalStudentCost} �";

                Bank.AmountPerSec += Bank.StudentPrice;
                Bank.UpdateAmountPerSec();
            }
        }
    }
}
