using UnityEngine;
using UnityEngine.InputSystem;


/* * * A script that shows the walkthrough when you press Tab * * */
namespace HorrorGame
{
    public class WalkthroughScript : MonoBehaviour
    {
        [SerializeField] private GameObject walkthrough;

        private InputAction _toggleWalkthroughAction;

        private void OnEnable()
        {
            _toggleWalkthroughAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/tab");
            _toggleWalkthroughAction.performed += OnToggleWalkthrough;
            _toggleWalkthroughAction.Enable();
        }

        private void OnDisable()
        {
            _toggleWalkthroughAction.performed -= OnToggleWalkthrough;
            _toggleWalkthroughAction.Disable();
        }

        private void OnToggleWalkthrough(InputAction.CallbackContext context)
        {
            walkthrough.SetActive(!walkthrough.activeSelf);
        }
    }
}
