using UnityEngine;

/* * * Script, that describes Special Bullet movement for Player in form of a sin-wave * * */
namespace SpaceShooter
{
    public class SinusBulletMovement : MonoBehaviour
    {
        private Rigidbody2D _rb;

        [Header("Movement")]
        [SerializeField] private float speed = 15f;
        [SerializeField] private float frequency = 8;
        private float _yPos;         // actual y-Position
        private float _startYPos;    // y-Position at Start
        private float _timer;        // timer for Sin. Need to make bullet start at yPos = startYPos

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _startYPos = transform.position.y;
        }

        private void Start() => _rb.velocity = Vector2.right * speed;

        private void FixedUpdate()
        {
            _timer += Time.deltaTime;
            _yPos = Mathf.Sin(_timer * frequency) + _startYPos;
            transform.position = new Vector3(transform.position.x, _yPos, 0);
        }

        private void OnBecameInvisible() => Destroy(gameObject);
    }
}
