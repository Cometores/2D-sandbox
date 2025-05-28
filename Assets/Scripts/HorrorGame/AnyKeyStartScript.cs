using UnityEngine;
using UnityEngine.InputSystem;

/* * * The script is tied to the text - "Press any key to start". 
 * Starts the tape animation and text animation, hides the game object responsible for the boot menu, and opens the main game * * */
namespace HorrorGame
{
    public class AnyKeyStartScript : MonoBehaviour
    {
        private RectTransform _rt;    // for text
        private Vector3 _normalScale; // normal Scale of our Text
        private Animator _videoTapeAnimator;     // Animator for Video Tape
        private AudioSource _aSource;
        private InputAction _anyKeyAction;
        
        [SerializeField] private GameObject loadScene;
        [SerializeField] private GameObject gameScene;
        [SerializeField] private GameObject videoTape;
        [SerializeField] private AudioClip tapeClip;
        [SerializeField] private float afterSec;    // Play Clip after the time after pressing Key
        [SerializeField] private float speed;
        
        private float _phase;    // phase for Text sin-Animation

        private void OnEnable()
        {
            _anyKeyAction = new InputAction("AnyKey", InputActionType.PassThrough, "<Keyboard>/anyKey");
            _anyKeyAction.performed += OnAnyKey;
            _anyKeyAction.Enable();
        }

        private void OnDisable()
        {
            _anyKeyAction.performed -= OnAnyKey;
            _anyKeyAction.Disable();
        }

        private void OnAnyKey(InputAction.CallbackContext context)
        {
            /* * * loading actual Game * * */
            _videoTapeAnimator.SetTrigger("ShowAnimation");  // Plays the Video-Tape Animation

            Invoke(nameof(PlayClip), afterSec);     // Plays Video-Tape-Clip
            Invoke(nameof(DestroyLoad), 2f);    // Load actual game
        }

        private void Start()
        {
            _aSource = GetComponent<AudioSource>();
            _rt = GetComponent<RectTransform>();
            _videoTapeAnimator = videoTape.GetComponent<Animator>();

            _normalScale = _rt.localScale;
        }


        private void Update()
        {
            /* * * Text animation * * */
            _rt.localScale = (Mathf.Abs(Mathf.Sin(_phase * speed)) + 1) * _normalScale;
            _phase += Time.deltaTime;
        }


        private void DestroyLoad()
        {
            loadScene.SetActive(false);
            gameScene.SetActive(true);
            Destroy(gameObject);
        }

        private void PlayClip() => _aSource.PlayOneShot(tapeClip);
    }
}
