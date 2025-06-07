using UnityEngine;
using UnityEngine.InputSystem;

namespace FlappyBird.Input
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler Instance { get; private set; }
        
        private FB_InputActions _actions;
        private InputAction _jump;
        private InputAction _pause;

        public System.Action OnJump;
        public System.Action OnPause;

        private void OnJumpPerformed(InputAction.CallbackContext ctx) => OnJump?.Invoke();
        private void OnPausePerformed(InputAction.CallbackContext ctx) => OnPause?.Invoke();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _actions = new FB_InputActions();
            _jump = _actions.Keyboard.Jump;
            _pause = _actions.Keyboard.Pause;
        }

        private void OnEnable()
        {
            _jump.performed += OnJumpPerformed;
            _pause.performed += OnPausePerformed;
            _jump.Enable();
            _pause.Enable();
        }

        private void OnDisable()
        {
            _jump.performed -= OnJumpPerformed;
            _pause.performed -= OnPausePerformed;
            _jump.Disable();
            _pause.Disable();
        }
    }
}
