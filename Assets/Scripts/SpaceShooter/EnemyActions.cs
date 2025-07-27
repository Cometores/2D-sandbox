using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    public class EnemyActions : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private AudioSource _auSource;
        private SpriteRenderer _sr;

        [SerializeField] private Sprite[] enemySprites;     // Different sprites for destruction system
        [SerializeField] private GameObject bullet;
        [SerializeField] private GameObject explosion;
        [SerializeField] private AudioClip hitClip;
        [SerializeField] private GameObject boss;

        [SerializeField] private float movementSpeed = 5f;
        private float _movementDirection = 1;    // 1 = Up; -1 = Down
        private int _life = 3;
        private int _spriteNr;

        private readonly Color _colorHit = new(0.8f, 0.5f, 0.5f);   // Damage received color
        private readonly Color _colorNormal = new(1, 1, 1);

        [HideInInspector] public GameObject bossLogic;      // pass to the boss, when spawn
        [HideInInspector] public TextMeshProUGUI scoreTxt;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _auSource = GetComponent<AudioSource>();
            _sr = GetComponent<SpriteRenderer>();

        }

        private void Start()
        {
            EnemyFire(); // looped function for firing 
            _rb.linearVelocity = Vector2.up * movementSpeed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // When the enemy rests up or down the screen, it changes the direction of movement
            if (collision.gameObject.name == "UpperBound" || collision.gameObject.name == "LowerBound")
            {
                _movementDirection *= -1;
                _rb.linearVelocity = _movementDirection * movementSpeed * Vector2.up;
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
                    Instantiate(explosion, transform.position, Quaternion.identity);

                    var actualScore = (int.Parse(scoreTxt.text) + 1);
                    scoreTxt.text = actualScore.ToString();

                    if (actualScore % 10 == 0) BossSpawn();

                    Destroy(gameObject);  // Destroy Enemy
                }
                else 
                {
                    _auSource.PlayOneShot(hitClip);
                    _sr.sprite = enemySprites[_spriteNr]; // change the sprite to a destroyed sprite
                    _sr.color = _colorHit;    // Set color to hit
                    _spriteNr++;

                    Invoke(nameof(ReturnColor), 0.1f);  // Set color to normal
                }
            }
        }

        private void EnemyFire()
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 90));
            Invoke(nameof(EnemyFire), 2);
        }

        private void BossSpawn()
        {
            Vector3 spawnPos = bossLogic.transform.GetChild(0).transform.position;
            GameObject newBoss = Instantiate(boss, spawnPos, Quaternion.Euler(0, 0, -90));
            var bossScript = newBoss.GetComponent<BossAction>();

            // transfer variables to the boss
            for (int i = 1; i < 4; i++)
            {
                bossScript.attackSpawnPos.Add(
                    bossLogic.transform.GetChild(i).transform.position);
            }
            bossScript.scoreTxt = scoreTxt;
        }

        private void ReturnColor() => _sr.color = _colorNormal;
    }
}
