using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfessorSpawner : Bank
{
    [SerializeField] Multiplier professorMultiplier;
    [SerializeField] GameObject professor;
    [SerializeField] GameObject max;
    [SerializeField] GameObject min;

    Vector3 xyMax;
    Vector3 xyMin;

    [SerializeField] GameObject professorCounterObj;
    [SerializeField] GameObject professorCostObj;

    static TextMeshProUGUI professorCounterText;
    static TextMeshProUGUI professorCostText;

    int normalProfessortCost;
    int professorCnt;

    void Awake()
    {
        normalProfessortCost = professorMultiplier.baseCost;
        xyMax = max.transform.position;
        xyMin = min.transform.position;

        professorCounterText = professorCounterObj.GetComponent<TextMeshProUGUI>();
        professorCostText = professorCostObj.GetComponent<TextMeshProUGUI>();
    }

    public void ProfessorOnClick()
    {
        if (Bank.account >= normalProfessortCost && professorCnt < professorMultiplier.maxAmount)
        {
            // Spawn Professor
            Vector3 newSpawnPos = new Vector3(Random.Range(xyMin.x, xyMax.x), Random.Range(xyMin.y, xyMax.y), 0);
            professor.GetComponent<ProfessorMovement>().xyMin = xyMin;
            professor.GetComponent<ProfessorMovement>().xyMax = xyMax;
            Instantiate(professor, newSpawnPos, Quaternion.identity);

            Bank.account -= normalProfessortCost;

            // Cost changes && amount changes
            normalProfessortCost = (int)(professorMultiplier.baseCost * Mathf.Pow(professorMultiplier.multiplier, professorCnt));
            professorCnt++;

            professorCounterText.text = $"{professorCnt}";
            professorCostText.text = $"Cost: {normalProfessortCost} ˆ";

            Bank.amountPerSec += Bank.studentPrice;
            Bank.UpdateAmountPerSec();
        }
    }
}
