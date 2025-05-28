using UnityEngine;

namespace HorrorGame
{
    public class PlayEndMusic : MonoBehaviour
    {
        [SerializeField] private GameObject mainGameObject;
        private AudioSource _auSource;
        [SerializeField] private AudioClip endClip;

        private void Awake() => _auSource = mainGameObject.GetComponent<AudioSource>();

        private void OnEnable()
        {
            _auSource.clip = endClip;
            _auSource.Play();
        }
    }
}
