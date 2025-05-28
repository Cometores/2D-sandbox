using UnityEngine;

/* * * plays the clip after some number of seconds, when you enable Game object * * */
namespace HorrorGame
{
    public class PlayClipAfter : MonoBehaviour
    {
        private AudioSource _auSource;
        [SerializeField] private AudioClip clip;
        [SerializeField] private float seconds;

        private void Awake() => _auSource = GetComponent<AudioSource>();

        private void OnEnable() => Invoke(nameof(PlayClip), seconds);

        private void PlayClip() => _auSource.PlayOneShot(clip);
    }
}
