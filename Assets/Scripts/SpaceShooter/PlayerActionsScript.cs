using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/* * * Script that describes player actions * * */
public class PlayerActionsScript : MonoBehaviour
{
    AudioSource auSource;
    SpriteRenderer sr;
    Rigidbody2D rb;

    [SerializeField] float moveSpeed = 5f;
    float moveDirection = 0;
    bool isFail;

    [HideInInspector] public PlayerInputActions playerControls;   // new Input System Asset
    InputAction move;
    InputAction fire;
    InputAction specialFire;
    
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject specialBullet;
    [SerializeField] AudioClip shootClip;
    [SerializeField] AudioClip explosionClip;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject turbo;

    [SerializeField] GameObject scoreObj;
    [SerializeField] GameObject bestScoreObj;
    TextMeshProUGUI scoreTxt;
    TextMeshProUGUI bestScoreTxt;
    int bestScoreValue;
    int actualScore;

    private void Awake()
    {
        auSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        playerControls = new PlayerInputActions();

        scoreTxt = scoreObj.GetComponent<TextMeshProUGUI>();
        bestScoreTxt = bestScoreObj.GetComponent<TextMeshProUGUI>();

        // BestScore text gets best score from Pref or nothing
        if (PlayerPrefs.HasKey("bestScore"))
        {
            bestScoreValue = PlayerPrefs.GetInt("bestScore");
            bestScoreTxt.text = bestScoreValue.ToString();
        }
    }

    void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;

        specialFire = playerControls.Player.SpecialFire;
        specialFire.Enable();
        specialFire.performed += SpecialFire;
    }

    void OnDisable()
    {
        move.Disable();
        fire.Disable();
        specialFire.Disable();
    }

    void Update()
    {
        if (!isFail)
            moveDirection = move.ReadValue<float>();
    }

    void FixedUpdate()
    {
        if (isFail)
            rb.velocity = Vector2.zero; // we can't move when we loss
        else
            rb.velocity = new Vector2(0, moveDirection * moveSpeed); // move Up or Down
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);  // Destroy Bullet
            PlayerHit();
        }
    }

    void Fire(InputAction.CallbackContext context)
    {
        // can't fire when we loss
        if (!isFail)
        {
            auSource.PlayOneShot(shootClip);
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 90));
        }
    }

    void SpecialFire(InputAction.CallbackContext context)
    {
        if (!isFail)
        {
            auSource.PlayOneShot(shootClip);
            Invoke(nameof(SpecialAttack), 0);
            Invoke(nameof(SpecialAttack), 0.1f);
            Invoke(nameof(SpecialAttack), 0.2f);
        }
    }

    void SpecialAttack() => Instantiate(specialBullet, transform.position, Quaternion.Euler(0, 0, 90));

    void PlayerHit()
    {
        // disable components
        isFail = true;
        sr.enabled = false;
        Destroy(turbo);
        auSource.Pause();

        /* * * Best score check * * */
        actualScore = int.Parse(scoreTxt.text);
        // if we already have best score
        if (PlayerPrefs.HasKey("bestScore"))
        {
            if (actualScore > bestScoreValue)
                PlayerPrefs.SetInt("bestScore", actualScore);
        }
        // if best score doesn't exist
        else
            PlayerPrefs.SetInt("bestScore", actualScore);

        Instantiate(explosion, transform.position, Quaternion.identity);
        Invoke(nameof(restartGame), 1.2f);
    }

    void restartGame() => SceneManager.LoadScene("SpaceShooter");
}
