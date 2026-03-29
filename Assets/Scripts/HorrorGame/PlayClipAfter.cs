using UnityEngine;

/* * * plays the clip after some number of seconds, when you enable Game object * * */
namespace HorrorGame
{
    public class PlayClipAfter : MonoBehaviour
    {
        private AudioSource _auSource;
        [SerializeField] private AudioClip clip;
        [SerializeField] private float seconds;

        private bool _wasPlayed;

        private void Awake() => _auSource = GetComponent<AudioSource>();

        private void OnEnable() => Invoke(nameof(PlayClip), seconds);

        private void PlayClip()
        {
            _auSource.PlayOneShot(clip);
            _wasPlayed = true;
        }

        private void OnDisable()
        {
            if (_wasPlayed)
            {
                return;
            }

            Debug.Log(gameObject.name + " was disabled before playing the audio clip", this);
            CancelInvoke();
        }
    }
}
