using UnityEngine;

namespace FlappyBird
{
    /// <summary>
    /// Manages the movement of obstacles in the game.
    /// Rocks, Pills
    /// </summary>
    public class ObstacleMover : MonoBehaviour
    {
        private Rigidbody2D _rb;
        [SerializeField] private float speed = 3f;

        private void Awake() => _rb = GetComponent<Rigidbody2D>();
        private void Start() => _rb.linearVelocity = Vector2.left * speed;
        private void OnBecameInvisible() => Destroy(gameObject);
    }
}