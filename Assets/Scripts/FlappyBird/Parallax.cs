using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird
{
    public class Parallax : MonoBehaviour
    {
        private RawImage _img;
        [SerializeField] private float speed;

        private void Awake() => _img = GetComponent<RawImage>();

        void Update() => _img.uvRect =
            new Rect(_img.uvRect.position + new Vector2(speed, 0) * Time.deltaTime, _img.uvRect.size);
    }
}