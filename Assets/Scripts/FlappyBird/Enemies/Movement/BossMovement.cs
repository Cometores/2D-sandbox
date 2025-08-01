using UnityEngine;

namespace FlappyBird.Enemies.Movement
{
    /// <summary>
    /// Controls the movement and animation of the boss character.
    /// </summary>
    public class BossMovement : ObstacleMover
    {
        #region Fields

        [Header("Movement")]
        [SerializeField] private float verticalSpeed = 2f;
        [SerializeField] private float upperYLimit = 3.8f;
        [SerializeField] private float lowerYLimit = -3.4f;

        [Header("Animation")]
        [SerializeField] private Sprite[] bossSprites;
        [SerializeField] private float spriteChangeInterval = 0.2f;

        private SpriteRenderer _renderer;
        private float _rotationSpeed;
        private int _rotationDirection = 1;
        private int _spriteIndex;
        private float _timer;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            _renderer = GetComponent<SpriteRenderer>();
        }

        protected override void Start()
        {
            base.Start();
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, verticalSpeed);

            InvokeRepeating(nameof(UpdateRotation), 0f, Random.Range(0.9f, 1.5f));
        }

        private void FixedUpdate()
        {
            if (transform.position.y >= upperYLimit)
                _rb.linearVelocity = new Vector2(-horizontalSpeed, -verticalSpeed);
            else if (transform.position.y <= lowerYLimit)
                _rb.linearVelocity = new Vector2(-horizontalSpeed,  verticalSpeed);
        }

        private void Update()
        {
            Animate();
            transform.Rotate(0f, 0f, _rotationSpeed * _rotationDirection * Time.deltaTime);
        }

        #endregion

        #region Animation & Rotation

        private void Animate()
        {
            if (bossSprites == null || bossSprites.Length == 0) return;
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _spriteIndex = (_spriteIndex + 1) % bossSprites.Length;
                _renderer.sprite = bossSprites[_spriteIndex];
                _timer = spriteChangeInterval;
            }
        }

        private void UpdateRotation()
        {
            _rotationSpeed = Random.Range(180f, 400f);
            _rotationDirection *= -1;
        }

        #endregion
    }
}
