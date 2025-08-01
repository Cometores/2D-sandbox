using System.Globalization;
using TMPro;
using UnityEngine;

namespace FlappyBird.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI powerUpText;
        [SerializeField] private GameObject powerUpDisplay;

        public void UpdateScoreTxt(int score) => scoreText.text = score.ToString();

        public void ShowPowerUpFor(float time)
        {
            powerUpDisplay.SetActive(true);
            powerUpText.text = $"Power Up {time.ToString("0.0", CultureInfo.InvariantCulture)}";
        }

        public void HidePowerUp() => powerUpDisplay.SetActive(false);
    }
}
