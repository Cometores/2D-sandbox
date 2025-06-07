using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird.UI
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private float speed = 0.1f;
        [SerializeField] private Vector2 direction = Vector2.left;

        private RawImage _image;
        private Rect _uvRect;

        private void Awake()
        {
            _image = GetComponent<RawImage>();
            _uvRect = _image.uvRect;
        }

        private void Update()
        {
            _uvRect.position += direction * (speed * Time.deltaTime);
            _image.uvRect = _uvRect;
        }
    }
}