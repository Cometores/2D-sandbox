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
            powerUpText.text = $"Power Up: {time:0.0}";
        }

        public void HidePowerUp() => powerUpDisplay.SetActive(false);
    }
}
