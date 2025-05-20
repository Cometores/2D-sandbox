using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/* * * Script, that desribes Player Movement and Game Logic* * */
public class TopDownPlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    [SerializeField] float speed = 5f;
    [SerializeField] float runMultiplier = 1.5f;
    Vector2 moveVector;

    bool isFirstSlime; // Is player near First Slime
    bool isSecondSlime; // Is player near Second Slime

    static Vector3 afterFightPos;               // Position near slime to safe
    [SerializeField] GameObject teleportOne;    // Position after First Door
    [SerializeField] GameObject teleportTwo;    // Position after Second Door

    public B2Input input;
    InputAction move;
    InputAction movingFaster;
    InputAction slimeInteract;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        input = new B2Input();

        transform.position = afterFightPos; // (0,0,0) is default Spawn pos
    }

    private void OnEnable()
    {
        move = input.Player.Move;
        move.Enable();

        movingFaster = input.Player.MovingFaster;
        movingFaster.Enable();

        slimeInteract = input.Player.SlimeInteract;
        slimeInteract.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        movingFaster.Disable();
        slimeInteract.Disable();
    }

    private void Update()
    {
        GetInput();
        SetAnimations();
    }

    private void FixedUpdate() => rb.velocity = moveVector * speed;

    private void GetInput()
    {
        moveVector = move.ReadValue<Vector2>();
        if (movingFaster.ReadValue<float>() == 1) moveVector *= runMultiplier;
        if (slimeInteract.ReadValue<float>() == 1)
        {
            if (isFirstSlime)
            {
                afterFightPos = transform.position;
                SceneManager.LoadScene("Fight1");
            }
            if (isSecondSlime)
            {
                afterFightPos = transform.position;
                SceneManager.LoadScene("Fight2");
            }
        }
    }

    private void SetAnimations()
    {
        // If the player is moving
        if (moveVector != Vector2.zero)
        {
            // Trigger transition to moving state
            anim.SetBool("IsMoving", true);

            // Set X and Y values for Blend Tree
            anim.SetFloat("MoveX", moveVector.x);
            anim.SetFloat("MoveY", moveVector.y);
        }
        else
            anim.SetBool("IsMoving", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* * * Doors Logic * * */
        if (collision.gameObject.name == "Door1")
            transform.position = teleportOne.transform.position;

        if (collision.gameObject.name == "Door2")
            transform.position = teleportTwo.transform.position;

        /* * * Slime Logic * * */
        if (collision.gameObject.name == "SlimeCollider1") isFirstSlime = true;
        if (collision.gameObject.name == "SlimeCollider2") isSecondSlime = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SlimeCollider1") isFirstSlime = false;
        if (collision.gameObject.name == "SlimeCollider2") isSecondSlime = false;
    }
}