using UnityEngine;

namespace FlappyBird
{
    /// <summary>
    /// Controls the movement and animation of the boss character.
    /// </summary>
    public class BossMovement : MonoBehaviour
    {
        #region Fields

        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRenderer;

        [Header("Movement")]
        [SerializeField] private float horizontalSpeed = 3f;
        [SerializeField] private float verticalSpeed = 2f;
        [SerializeField] private float upperYLimit = 3.8f;
        [SerializeField] private float lowerYLimit = -3.4f;

        [Header("Rotation")]
        private float _rotationSpeed;
        private int _rotationDirection = 1;

        [Header("Animation")]
        [SerializeField] private Sprite[] bossSprites;
        [SerializeField] private float spriteChangeInterval = 0.2f;
        private int _spriteIndex;
        private int _spriteCount;
        private float _animationTimer;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteCount = bossSprites.Length;

            if (_spriteCount == 0)
            {
                Debug.LogWarning("BossMovement: No sprites assigned.");
                enabled = false;
            }
        }

        private void Start()
        {
            _rb.velocity = new Vector2(-horizontalSpeed, verticalSpeed);
            StartRotationCycle();
        }

        private void FixedUpdate()
        {
            if (transform.position.y >= upperYLimit)
                _rb.velocity = new Vector2(-horizontalSpeed, -verticalSpeed);

            else if (transform.position.y <= lowerYLimit)
                _rb.velocity = new Vector2(-horizontalSpeed, verticalSpeed);
        }

        private void Update()
        {
            UpdateSpriteAnimation();
            Rotate();
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        #endregion

        #region Animation & Rotation

        private void UpdateSpriteAnimation()
        {
            _animationTimer -= Time.deltaTime;

            if (_animationTimer <= 0f)
            {
                _spriteIndex = (_spriteIndex + 1) % _spriteCount;
                _spriteRenderer.sprite = bossSprites[_spriteIndex];
                _animationTimer = spriteChangeInterval;
            }
        }

        private void Rotate()
        {
            transform.Rotate(0f, 0f, _rotationSpeed * _rotationDirection * Time.deltaTime);
        }

        private void StartRotationCycle()
        {
            _rotationSpeed = Random.Range(180f, 400f);
            _rotationDirection *= -1;
            Invoke(nameof(StartRotationCycle), Random.Range(0.9f, 1.5f));
        }

        #endregion
    }
}
