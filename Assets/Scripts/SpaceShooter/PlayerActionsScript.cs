using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class PlayerActionsScript : MonoBehaviour
    {
        private AudioSource _auSource;
        private SpriteRenderer _sr;
        private Rigidbody2D _rb;

        [SerializeField] private float moveSpeed = 5f;
        private float _moveDirection = 0;
        private bool _isFail;

        private SS_InputActions _playerControls;
        private InputAction _move;
        private InputAction _fire;
        private InputAction _specialFire;

        [SerializeField] private GameObject bullet;
        [SerializeField] private GameObject specialBullet;
        [SerializeField] private AudioClip shootClip;
        [SerializeField] private AudioClip explosionClip;
        [SerializeField] private GameObject explosion;
        [SerializeField] private GameObject turbo;

        [SerializeField] private GameObject scoreObj;
        [SerializeField] private GameObject bestScoreObj;
        private TextMeshProUGUI _scoreTxt;
        private TextMeshProUGUI _bestScoreTxt;
        private int _bestScoreValue;
        private int _actualScore;

        private void Awake()
        {
            _auSource = GetComponent<AudioSource>();
            _sr = GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody2D>();

            _playerControls = new SS_InputActions();

            _scoreTxt = scoreObj.GetComponent<TextMeshProUGUI>();
            _bestScoreTxt = bestScoreObj.GetComponent<TextMeshProUGUI>();

            // Setting the best score
            if (PlayerPrefs.HasKey("bestScore"))
            {
                _bestScoreValue = PlayerPrefs.GetInt("bestScore");
                _bestScoreTxt.text = _bestScoreValue.ToString();
            }
        }

        private void OnEnable()
        {
            _move = _playerControls.Player.Move;
            _move.Enable();

            _fire = _playerControls.Player.Fire;
            _fire.Enable();
            _fire.performed += Fire;

            _specialFire = _playerControls.Player.SpecialFire;
            _specialFire.Enable();
            _specialFire.performed += SpecialFire;
        }

        private void OnDisable()
        {
            _move.Disable();
            _fire.Disable();
            _specialFire.Disable();
        }

        private void Update()
        {
            if (!_isFail)
                _moveDirection = _move.ReadValue<float>();
        }

        private void FixedUpdate()
        {
            if (_isFail)
                _rb.velocity = Vector2.zero; // we can't move when we lost
            else
                _rb.velocity = new Vector2(0, _moveDirection * moveSpeed); // move Up or Down
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("EnemyBullet"))
            {
                Destroy(collision.gameObject); // Destroy Bullet
                PlayerHit();
            }
        }

        private void Fire(InputAction.CallbackContext context)
        {
            // can't fire when we lost
            if (!_isFail)
            {
                _auSource.PlayOneShot(shootClip);
                Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 90));
            }
        }

        private void SpecialFire(InputAction.CallbackContext context)
        {
            if (!_isFail)
            {
                _auSource.PlayOneShot(shootClip);
                Invoke(nameof(SpecialAttack), 0);
                Invoke(nameof(SpecialAttack), 0.1f);
                Invoke(nameof(SpecialAttack), 0.2f);
            }
        }

        private void SpecialAttack() => Instantiate(specialBullet, transform.position, Quaternion.Euler(0, 0, 90));

        private void PlayerHit()
        {
            // disable components
            _isFail = true;
            _sr.enabled = false;
            Destroy(turbo);
            _auSource.Pause();

            /* * * Best score check * * */
            _actualScore = int.Parse(_scoreTxt.text);
            // if we already have best score
            if (PlayerPrefs.HasKey("bestScore"))
            {
                if (_actualScore > _bestScoreValue)
                    PlayerPrefs.SetInt("bestScore", _actualScore);
            }
            // if best score doesn't exist
            else
                PlayerPrefs.SetInt("bestScore", _actualScore);

            Instantiate(explosion, transform.position, Quaternion.identity);
            Invoke(nameof(RestartGame), 1.2f);
        }

        private void RestartGame() => SceneManager.LoadScene("SpaceShooter");
    }
}