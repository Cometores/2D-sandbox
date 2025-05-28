using UnityEngine;
using UnityEngine.UI;

/* * * Script for the animation of hitting the TV in room 13 * * */
namespace HorrorGame
{
    public class TVMovement : MonoBehaviour
    {
        private Vector3 _normalPosition;
        [SerializeField] private float speed = 70;
        private float _phase; // phase for sin
        private Image _tvImg;

        private void Awake() => _tvImg = GetComponent<Image>();

        private void OnEnable()
        {
            _tvImg.enabled = true;
            _normalPosition = transform.position;
            Invoke(nameof(DestroyTv), 0.1f);
        }


        private void Update()
        {
            if (_tvImg.enabled)
            {
                // Use the displacement of the x-coordinate in the sine phase
                transform.position = _normalPosition + Vector3.right * ((Mathf.Sin(_phase * speed)) * 30);
                _phase += Time.deltaTime;
            }
        }

        private void DestroyTv()
        {
            _tvImg.enabled = false;
            transform.position = _normalPosition;
        }
    }
}
