using UnityEngine;
using UnityEngine.UI;

/* * * A script that creates the feeling of lights going off in room 4. * * *
 * * * Works almost exactly like LightAnimation Script * * */
namespace HorrorGame
{
    public class LightOff : MonoBehaviour
    {
        private Image _lightOn; // A picture of a lighter room
        [SerializeField] private float animationSpeed = 0.5f;
        private float _transp;

        private void Awake() => _lightOn = GetComponent<Image>();


        private void OnEnable()
        {
            _transp = 1;
            _lightOn.enabled = true;
        }


        private void Update()
        {
            if (_transp <= 0) _lightOn.enabled = false;

            _transp += -animationSpeed * Time.deltaTime;
            _lightOn.color = new Color(1, 1, 1, _transp);
        }
    }
}
