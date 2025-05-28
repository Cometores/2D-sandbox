using UnityEngine;
using UnityEngine.UI;

/* * * A script for lighting animation, which essentially just makes another picture transparent * * *
 * * * Used in rooms 5, 7, 11 * * */

namespace HorrorGame
{
    public class LightAnimation : MonoBehaviour
    {
        private Image _lightImg; // A picture of a lighter room
        [SerializeField] private float animationSpeed = 1.8f;
        private float _transp;

        private void Awake() => _lightImg = GetComponent<Image>();

        private void Update()
        {
            _transp += animationSpeed * Time.deltaTime;
            if (_transp >= 1 || _transp <= 0) animationSpeed *= -1;
            _lightImg.color = new Color(1, 1, 1, _transp);
        }
    }
}
