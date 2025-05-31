using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace FlappyBird
{
    /// <summary>
    /// This class handles movement for player in the Flappy Bird game.
    /// Is also responsible for scoring.
    /// </summary>
    public class Player : MonoBehaviour
    {
        #region Fields

        private Rigidbody2D _rb;
        private Animator _animator;

        [FormerlySerializedAs("flightForce")]
        [Header("Movement")]
        [SerializeField, Range(3, 10)] private float jumpForce;
        [SerializeField, Range(12, 20)] private int rotationSpeed;
        private FB_InputActions _playerControls;
        private InputAction _jump;

        [Header("Scoring")]
        [SerializeField, Range(5, 15)] private int powerUpTime;
        [SerializeField, Range(2, 4)] private int powerUpMultiplier;
        [SerializeField] private GameObject score;
        [SerializeField] private GameObject powerUp;
        private TextMeshProUGUI _scoreText;
        private TextMeshProUGUI _powerUpText;
        private int _scoreCount;
        private float _powerUpLeft;
        private bool _isPowerUp;

        [Header("Sounds")]
        [SerializeField] private AudioClip jumpClip;
        [SerializeField] private AudioClip hitClip;
        [SerializeField] private AudioClip pointClip;
        private AudioSource _audioSource;
        private bool _hitPlayed;

        [Header("Effects")]
        [SerializeField] private ParticleSystem jumpParticles;
        
        private bool _isFail;
        private static readonly int Hit = Animator.StringToHash("hit");

        #endregion
        
        #region Unity methods

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _scoreText = score.GetComponent<TextMeshProUGUI>();
            _powerUpText = powerUp.GetComponent<TextMeshProUGUI>();
            _playerControls = new FB_InputActions();
        }

        private void OnEnable()
        {
            _jump = _playerControls.Keyboard.Jump;
            _jump.performed += Jump;
            _jump.Enable();
        }

        private void OnDisable()
        {
            _jump.performed -= Jump;
            _jump.Disable();
        }

        private void Update()
        {
            // You can control the plane until you hit an obstacle
            if (_isFail) return;

            HandlePowerUp();

            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
        
        #endregion
        
        #region Collision handling

        /// <summary>
        /// Player hits an obstacle. Restarting the game.
        /// </summary>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Celling") return;

            if (!_hitPlayed)
            {
                _audioSource.PlayOneShot(hitClip);
                _hitPlayed = true;
            }
            
            _isFail = true;
            _animator.SetTrigger(Hit);
            Invoke(nameof(RestartGame), 1.2f);
        }
        
        /// <summary>
        /// Responsible for scoring and special effects
        /// </summary>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            _audioSource.PlayOneShot(pointClip);

            if (collision.CompareTag($"SpecialScore"))
            {
                _scoreCount += _isPowerUp ? 2 * powerUpMultiplier : 2; 
                _scoreText.text = _scoreCount.ToString();
            }

            if (collision.CompareTag($"Score"))
            {
                _scoreCount += _isPowerUp ? powerUpMultiplier : 1; 
                _scoreText.text = _scoreCount.ToString();
            }

            if (collision.CompareTag($"PowerUp"))
            {
                _isPowerUp = true;
                _powerUpLeft = powerUpTime;
                powerUp.SetActive(true);
                Destroy(collision.gameObject);
            }
        }

        #endregion
        
        private void HandlePowerUp()
        {
            if (_isPowerUp && _powerUpLeft <= 0)
            {
                powerUp.SetActive(false);
                _isPowerUp = false;
            }
            else if (_isPowerUp)
            {
                _powerUpText.text = $"Power Up: {_powerUpLeft:0.0}";
                _powerUpLeft -= Time.deltaTime;
            }
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (_isFail) return;

            // Taking off a plane with the space bar
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            transform.rotation = Quaternion.identity;
            
            // Particles
            Instantiate(jumpParticles, transform.position, Quaternion.identity);
            
            // Play sound
            _audioSource.PlayOneShot(jumpClip);
        }

        private void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}