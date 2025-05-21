using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Class representing the actions and behavior of a boss enemy in the game.
    /// </summary>
    public class BossAction : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private AudioSource _auSource;
    
        [SerializeField] private GameObject attack;
        [SerializeField] private GameObject explosion;
        [SerializeField] private AudioClip hitClip;
        [SerializeField] private GameObject hitLight;
        [SerializeField] private float movementSpeed = 3f;

        private float _movementDirection = 1;    // 1 = Up; -1 = Down
        private int _life = 15;
        private const int ATTACK_SPAWN_CNT = 3; // need for different attack pos
        private int _actualAttack;

        [HideInInspector] public TextMeshProUGUI scoreTxt;
        [HideInInspector] public List<Vector3> attackSpawnPos = new();

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _auSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            BossFire(); // looped function for firing 
            BossChangeDirection(); // special movement behavior
            _rb.velocity = Vector2.up * movementSpeed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // When the boss rests up or down the screen, it changes the direction of movement
            if (collision.gameObject.name == "UpperBound")
            {
                _movementDirection = -1;
                _rb.velocity = _movementDirection * movementSpeed * Vector2.up;
            }
            else if (collision.gameObject.name == "LowerBound")
            {
                _movementDirection = 1;
                _rb.velocity = _movementDirection * movementSpeed * Vector2.up;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet"))
            {
                Destroy(collision.gameObject);  // Destroy Bullet
                _life--;

                if (_life <= 0)
                {
                    Instantiate(explosion, transform.position, Quaternion.Euler(-90,0,0));
                    scoreTxt.text = (int.Parse(scoreTxt.text) + 1).ToString();
                    Destroy(gameObject);
                }
                else
                {
                    _auSource.PlayOneShot(hitClip);
                    hitLight.SetActive(true); // Set hit color with Light
                
                    Invoke(nameof(ReturnColor), 0.1f);  // Set color to normal
                }
            }
        }

        private void BossFire()
        {
            // 3 same attacks at different positions 
            Instantiate(attack, attackSpawnPos[_actualAttack], Quaternion.identity);
            _actualAttack = (_actualAttack + 1) % ATTACK_SPAWN_CNT;
            Invoke(nameof(BossFire), 2.5f);
        }

        private void BossChangeDirection()
        {
            // random movement behavior
            _movementDirection = Random.Range(-1, 2);
            _rb.velocity = _movementDirection * movementSpeed * Vector2.up;
            Invoke(nameof(BossChangeDirection), 1.5f);
        }

        private void ReturnColor() => hitLight.SetActive(false);
    }
}
