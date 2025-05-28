using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace CookieClicker
{
    public class RubButton : Bank, IPointerDownHandler
    {
        [SerializeField] private GameObject incText;
        private Camera _camMain;
    
        private void Awake()
        {
            _camMain = Camera.main;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            GameObject newIncounterText = Instantiate(incText, mousePos, Quaternion.identity);
            newIncounterText.GetComponent<TextMeshProUGUI>().text = $"+{ClickPrice}";
            newIncounterText.transform.SetParent(transform);

            Bank.Account += ClickPrice;
        }
    }
}
