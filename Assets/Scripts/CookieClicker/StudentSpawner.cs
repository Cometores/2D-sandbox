using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StudentSpawner : Bank
{
    [SerializeField] Multiplier studentMultiplier;
    [SerializeField] GameObject student;
    [SerializeField] GameObject max;
    [SerializeField] GameObject min;

    Vector3 xyMax;
    Vector3 xyMin;

    [SerializeField] GameObject studentCounterObj;
    [SerializeField] GameObject studentCostObj;

    static TextMeshProUGUI studentCounterText;
    static TextMeshProUGUI studentCostText;

    int studentCnt;
    int normalStudentCost;

    void Awake()
    {
        normalStudentCost = studentMultiplier.baseCost;
        xyMax = max.transform.position;
        xyMin = min.transform.position;

        studentCounterText = studentCounterObj.GetComponent<TextMeshProUGUI>();
        studentCostText = studentCostObj.GetComponent<TextMeshProUGUI>();
    }

    public void StudentOnClick()
    {
        if (Bank.account >= normalStudentCost && studentCnt < studentMultiplier.maxAmount)
        {
            // Spawn Student
            Vector3 newSpawnPos = new Vector3(Random.Range(xyMin.x, xyMax.x), Random.Range(xyMin.y, xyMax.y), 0);
            Instantiate(student, newSpawnPos, Quaternion.identity);

            Bank.account -= studentMultiplier.baseCost;

            // Cost changes && amount changes
            normalStudentCost = (int)(studentMultiplier.baseCost * Mathf.Pow(studentMultiplier.multiplier, studentCnt));
            studentCnt++;

            studentCounterText.text = $"{studentCnt}";
            studentCostText.text = $"Cost: {normalStudentCost} ˆ";

            Bank.amountPerSec += Bank.studentPrice;
            Bank.UpdateAmountPerSec();
        }
    }
}
