using UnityEngine;

/// <summary>
/// Controls the movement of the boss character.
/// </summary>
public class BossMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    // Boss movement speed
    [SerializeField] private float speedX;
    [SerializeField] private float speedY;

    // Boss rotation variables
    private float _rotationSpeed;
    private int _rotationDirection = 1;

    [SerializeField] private Sprite[] bossSprites;
    private int _spriteIndex; // actual index in bossSprites array
    private int _spriteLen;

    [SerializeField] float animationTime; // How often the sprite will change
    private float _timer; // auxiliary variable for animation


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _spriteLen = bossSprites.Length;
    }


    // set the initial speed and rotation
    void Start()
    {
        RotateUpdate();
        _rb.velocity = new Vector2(-speedX, speedY);
    } 
        

    private void FixedUpdate()
    {
        // Change the movement direction from bottom to top and back, if necessary
        if (transform.position.y >= 3.8f)
            _rb.velocity = new Vector2(-speedX, -speedY);

        else if (transform.position.y <= -3.4f)
            _rb.velocity = new Vector2(-speedX, speedY);
    }


    void Update()
    {
        /* * * Sprite animation behavior * * */ 
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _spriteIndex = (_spriteIndex + 1) % _spriteLen;
            _sr.sprite = bossSprites[_spriteIndex];
            _timer = animationTime;
        }

        /* * * Rotation Behavior * * */
        transform.Rotate(new Vector3(0, 0, _rotationSpeed * _rotationDirection) * Time.deltaTime);
    }


    void RotateUpdate()
    {
        /* change direction and speed at time intervals */
        _rotationSpeed = Random.Range(180f, 400f);
        _rotationDirection *= -1;

        // Boss spins random time at random speed
        Invoke(nameof(RotateUpdate), Random.Range(0.9f, 1.5f));
    }


    private void OnBecameInvisible() => Destroy(gameObject);
}