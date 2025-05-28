using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/* The script is responsible for the flight of the plane, as well as the score of the game. */
public class PlaneMover : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject powerUp;
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _powerUpText;

    private AudioSource _auSource;
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip hitClip;
    [SerializeField] AudioClip pointClip;

    [SerializeField] float flightForce;
    int scoreCnt;
    float powerUpTime;
    bool isPowerUp;
    bool isFail;


    private void Awake() 
    {
        _rb = GetComponent<Rigidbody2D>();
        _auSource = GetComponent<AudioSource>();
        _scoreText = score.GetComponent<TextMeshProUGUI>();
        _powerUpText = powerUp.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // You can control the plane until you hit an obstacle
        if (!isFail)
        {
            // Taking off a plane with the space bar
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rb.AddForce(Vector2.up * flightForce, ForceMode2D.Impulse);
                _auSource.PlayOneShot(jumpClip);
                transform.rotation = Quaternion.identity;
            }

            // PowerUp end
            if (isPowerUp && powerUpTime >= 15)
            {
                powerUpTime = 0;
                powerUp.SetActive(false);
                isPowerUp = false;
            }
            else if (isPowerUp)
            {
                _powerUpText.text = $"Power Up: {powerUpTime.ToString("0.00")}";
                powerUpTime += Time.deltaTime;
            }

            transform.Rotate(0, 0, -30 * Time.deltaTime);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collision with an obstacle and restarting the game
        if (collision.gameObject.name != "Celling")
        {
            _auSource.PlayOneShot(hitClip);
            isFail = true;
            Invoke(nameof(restartGame), 1.2f);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        _auSource.PlayOneShot(pointClip);

        /* * * Trigger processing for scoring and power up * * */
        if (collision.CompareTag("SpecialScore"))
        {
            scoreCnt += isPowerUp ? 3 : 1; // Adding +2 for boss, when powerUp
            _scoreText.text = scoreCnt.ToString();
        }
        
        if (collision.CompareTag("Score"))
        {
            scoreCnt += isPowerUp ? 2 : 1; // Adding +2 for rocks, when powerUp
            _scoreText.text = scoreCnt.ToString();
        }

        if (collision.CompareTag("PowerUp"))
        {
            isPowerUp = true;
            powerUp.SetActive(true);
            Destroy(collision.gameObject);
        }
    }

    void restartGame() => SceneManager.LoadScene("FlappyBird");
}
