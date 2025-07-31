using UnityEngine;
using UnityEngine.Serialization;

namespace FlappyBird
{
    /// <summary>
    /// Manages the movement of obstacles in the game.
    /// Rocks, Pills
    /// </summary>
    public class ObstacleMover : MonoBehaviour
    {
        protected Rigidbody2D _rb;

        [FormerlySerializedAs("speed")][SerializeField] protected float horizontalSpeed = 3f;

        protected virtual void Awake() => _rb = GetComponent<Rigidbody2D>();

        protected virtual void Start() => ApplyHorizontalVelocity();

        protected virtual void ApplyHorizontalVelocity()
        {
            Vector2 v = _rb.linearVelocity;
            v.x = -horizontalSpeed;
            _rb.linearVelocity = v;
        }

        protected virtual void OnBecameInvisible() => Destroy(gameObject);
    }
}