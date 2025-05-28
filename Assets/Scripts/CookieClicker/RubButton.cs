using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class RubButton : Bank, IPointerDownHandler
{
    [SerializeField] GameObject incText;
    Camera camMain;
    private void Awake()
    {
        camMain = Camera.main;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject newIncounterText = Instantiate(incText, Input.mousePosition, Quaternion.identity);
        newIncounterText.GetComponent<TextMeshProUGUI>().text = $"+{clickPrice}";
        newIncounterText.transform.SetParent(transform);

        Bank.account += clickPrice;
    }
}
