using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    protected static int account;
    protected static int amountPerSec;
    protected static int clickPrice = 1;
    protected static int studentPrice = 10;
    protected static int scientistPrice = 20;
    protected static int professorPrice = 30;

    [SerializeField] GameObject increaseTextObj;
    [SerializeField] GameObject accountTextObj;

    static TextMeshProUGUI increaseText;
    static TextMeshProUGUI accountText;

    private void Awake()
    {
        increaseText = increaseTextObj.GetComponent<TextMeshProUGUI>();
        accountText = accountTextObj.GetComponent<TextMeshProUGUI>();
        increaseAccount();
    }


    protected static void UpdateAmountPerSec()
    {
        increaseText.text = $"+ {amountPerSec} ˆ / Second";
    }

    void increaseAccount()
    {
        account += amountPerSec;
        Invoke(nameof(increaseAccount), 1f);
    }

    void Update()
    {
        accountText.text = $"{account} ˆ";
    }
}
