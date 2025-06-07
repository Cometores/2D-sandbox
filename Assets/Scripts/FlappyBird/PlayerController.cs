using FlappyBird.Config;
using FlappyBird.Core;
using FlappyBird.Input;
using FlappyBird.UI;
using UnityEngine;

namespace FlappyBird
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerConfig config;
        [SerializeField] private ParticleSystem jumpParticles;

        private Rigidbody2D _rb;
        private Animator _animator;
        private UIController _ui;
        private InputHandler _input;
        private int _score;
        private float _powerUpTimeLeft;
        private bool _isPowered;
        private bool _isDead;

        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int Eat = Animator.StringToHash("eat");

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _ui = FindObjectOfType<UIController>();
            _input = InputHandler.Instance;
            _input.OnJump += Jump;
        }

        private void Update()
        {
            if (_isDead) return;
            if (_isPowered)
            {
                _powerUpTimeLeft -= Time.deltaTime;
                if (_powerUpTimeLeft <= 0)
                {
                    _ui.HidePowerUp();
                    _isPowered = false;
                }
                else _ui.ShowPowerUp(_powerUpTimeLeft);
            }
            transform.Rotate(Vector3.back * (config.rotateSpeed * Time.deltaTime));
        }
        
        private void OnDestroy()
        {
            if (_input != null)
                _input.OnJump -= Jump;
        }

        private void Jump()
        {
            if (_isDead) return;
            
            _rb.AddForce(Vector2.up * config.jumpForce, ForceMode2D.Impulse);
            transform.rotation = Quaternion.identity;
            
            Instantiate(jumpParticles, transform.position, Quaternion.identity);
            AudioManager.Instance.PlayJump();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (_isDead || col.gameObject.name == "Ceiling") return;
            
            _isDead = true;
            _animator.SetTrigger(Hit);
            AudioManager.Instance.PlayHit();
            
            Invoke(nameof(Restart), 1.2f);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Score")) {
                _score += _isPowered ? config.scoreMultiplier : 1;
                _ui.UpdateScore(_score);
                AudioManager.Instance.PlayRandomPoint();
            }
            else if (col.CompareTag("SpecialScore")) {
                _score += _isPowered ? 2 * config.scoreMultiplier : 2;
                _ui.UpdateScore(_score);
                AudioManager.Instance.PlayRandomPoint();
            }
            else if (col.CompareTag("PowerUp")) {
                _isPowered = true;
                _powerUpTimeLeft = config.powerUpDuration;
                AudioManager.Instance.PlayEat();
                _animator.SetTrigger(Eat);
                Destroy(col.gameObject);
            }
        }

        private void Restart() => GameManager.Instance.RestartLevel();
    }
}