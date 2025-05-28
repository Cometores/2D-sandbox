using TMPro;
using UnityEngine;

namespace CookieClicker
{
    public class ProfessorSpawner : Bank
    {
        [SerializeField] private Multiplier professorMultiplier;
        [SerializeField] private GameObject professor;
        [SerializeField] private GameObject max;
        [SerializeField] private GameObject min;

        private Vector3 xyMax;
        private Vector3 xyMin;

        [SerializeField] private GameObject professorCounterObj;
        [SerializeField] private GameObject professorCostObj;

        private static TextMeshProUGUI professorCounterText;
        private static TextMeshProUGUI professorCostText;

        private int normalProfessortCost;
        private int professorCnt;

        private void Awake()
        {
            normalProfessortCost = professorMultiplier.baseCost;
            xyMax = max.transform.position;
            xyMin = min.transform.position;

            professorCounterText = professorCounterObj.GetComponent<TextMeshProUGUI>();
            professorCostText = professorCostObj.GetComponent<TextMeshProUGUI>();
        }

        public void ProfessorOnClick()
        {
            if (Bank.Account >= normalProfessortCost && professorCnt < professorMultiplier.maxAmount)
            {
                // Spawn Professor
                Vector3 newSpawnPos = new Vector3(Random.Range(xyMin.x, xyMax.x), Random.Range(xyMin.y, xyMax.y), 0);
                professor.GetComponent<ProfessorMovement>().xyMin = xyMin;
                professor.GetComponent<ProfessorMovement>().xyMax = xyMax;
                Instantiate(professor, newSpawnPos, Quaternion.identity);

                Bank.Account -= normalProfessortCost;

                // Cost changes && amount changes
                normalProfessortCost = (int)(professorMultiplier.baseCost * Mathf.Pow(professorMultiplier.multiplier, professorCnt));
                professorCnt++;

                professorCounterText.text = $"{professorCnt}";
                professorCostText.text = $"Cost: {normalProfessortCost} �";

                Bank.AmountPerSec += Bank.StudentPrice;
                Bank.UpdateAmountPerSec();
            }
        }
    }
}
