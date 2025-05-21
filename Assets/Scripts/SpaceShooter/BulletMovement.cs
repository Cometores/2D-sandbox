using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Handles the movement of a bullet in the game.
    /// </summary>
    public class BulletMovement : MonoBehaviour
    {
        private Rigidbody2D _rb;
        
        [Header("Movement")]
        [SerializeField] private float speed = 15f;
        [SerializeField] private float movementDirection; // 1 for Player bullet (right), -1 for Enemy bullet (left)

        private void Awake() => _rb = GetComponent<Rigidbody2D>();

        private void Start() => _rb.velocity = Vector2.right * speed * movementDirection;

        private void OnBecameInvisible() => Destroy(gameObject);
    }
}
