using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace DungeonRPG
{
    /// <summary>
    /// Class representing a top-down player controller in the game.
    /// </summary>
    public class TopDownPlayerController : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Animator _anim;

        [Header("Movement")] [SerializeField] private float speed = 5f;
        [SerializeField] private float runMultiplier = 1.5f;
        private Vector2 _moveVector;

        [SerializeField] private GameObject teleportOne; // Position after First Door
        [SerializeField] private GameObject teleportTwo; // Position after Second Door
        private static Vector3 _afterFightPos; // Position near slime to safe
        private bool _isFirstSlime; // Is player near First Slime
        private bool _isSecondSlime; // Is player near Second Slime

        private B2Input _input;
        private InputAction _move;
        private InputAction _movingFaster;
        private InputAction _slimeInteract;

        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _input = new B2Input();

            transform.position = _afterFightPos; // (0,0,0) is default Spawn position
        }

        private void OnEnable() => EnableInput();

        private void OnDisable() => DisableInput();

        private void Update()
        {
            HandleInput();
            SetAnimations();
        }

        private void FixedUpdate() => _rb.linearVelocity = _moveVector * speed;

        private void EnableInput()
        {
            _move = _input.Player.Move;
            _move.Enable();

            _movingFaster = _input.Player.MovingFaster;
            _movingFaster.Enable();

            _slimeInteract = _input.Player.SlimeInteract;
            _slimeInteract.Enable();
        }

        private void DisableInput()
        {
            _move.Disable();
            _movingFaster.Disable();
            _slimeInteract.Disable();
        }

        private void HandleInput()
        {
            // Handle Movement
            _moveVector = _move.ReadValue<Vector2>();
            if (Mathf.Approximately(_movingFaster.ReadValue<float>(), 1))
            {
                _moveVector *= runMultiplier;
            }

            // Handle slime logic
            if (Mathf.Approximately(_slimeInteract.ReadValue<float>(), 1))
            {
                if (_isFirstSlime)
                {
                    _afterFightPos = transform.position;
                    SceneManager.LoadScene("Fight1");
                }

                if (_isSecondSlime)
                {
                    _afterFightPos = transform.position;
                    SceneManager.LoadScene("Fight2");
                }
            }
        }

        private void SetAnimations()
        {
            if (_moveVector != Vector2.zero)
            {
                // Trigger transition to moving state
                _anim.SetBool(IsMoving, true);

                // Set X and Y values for Blend Tree
                _anim.SetFloat(MoveX, _moveVector.x);
                _anim.SetFloat(MoveY, _moveVector.y);
            }
            else
            {
                _anim.SetBool(IsMoving, false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            /* * * Doors Logic * * */
            if (collision.gameObject.name == "Door1")
                transform.position = teleportOne.transform.position;

            if (collision.gameObject.name == "Door2")
                transform.position = teleportTwo.transform.position;

            /* * * Slime Logic * * */
            if (collision.gameObject.name == "SlimeCollider1") _isFirstSlime = true;
            if (collision.gameObject.name == "SlimeCollider2") _isSecondSlime = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "SlimeCollider1") _isFirstSlime = false;
            if (collision.gameObject.name == "SlimeCollider2") _isSecondSlime = false;
        }
    }
}