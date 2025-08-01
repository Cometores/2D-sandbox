using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird.UI
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private float speed = 0.1f;
        private static Vector2 _direction;

        private RawImage _image;
        private Rect _uvRect;

        private void Awake()
        {
            _image = GetComponent<RawImage>();
            _uvRect = _image.uvRect;
            _direction = Vector2.right;
        }

        private void Update()
        {
            _uvRect.position += _direction * (speed * Time.deltaTime);
            _image.uvRect = _uvRect;
        }

        public static void InvertSpeed() => _direction *= Vector2.left;
    }
}