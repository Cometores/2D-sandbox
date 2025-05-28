using UnityEngine;

/* Stones always move to the left */
// We use this also for bonus pills, since the movement pattern is the same
public class RockMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float speed = 3f;

    private void Awake() => _rb = GetComponent<Rigidbody2D>();

    private void Start() => _rb.velocity = Vector2.left * speed;

    private void OnBecameInvisible() => Destroy(gameObject);
}
