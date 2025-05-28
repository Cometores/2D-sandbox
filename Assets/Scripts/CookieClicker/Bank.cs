using TMPro;
using UnityEngine;

namespace CookieClicker
{
    public class Bank : MonoBehaviour
    {
        protected static int Account;
        protected static int AmountPerSec;
        
        protected static int ClickPrice = 1;
        protected const int StudentPrice = 10;
        protected const int ScientistPrice = 20;
        protected const int ProfessorPrice = 30;

        [SerializeField] private GameObject increaseTextObj;
        [SerializeField] private GameObject accountTextObj;

        private static TextMeshProUGUI increaseText;
        private static TextMeshProUGUI accountText;

        private void Awake()
        {
            increaseText = increaseTextObj.GetComponent<TextMeshProUGUI>();
            accountText = accountTextObj.GetComponent<TextMeshProUGUI>();
            IncreaseAccount();
        }


        protected static void UpdateAmountPerSec()
        {
            increaseText.text = $"+ {AmountPerSec} € / Second";
        }

        private void IncreaseAccount()
        {
            Account += AmountPerSec;
            Invoke(nameof(IncreaseAccount), 1f);
        }

        private void Update()
        {
            accountText.text = $"{Account} €";
        }
    }
}
