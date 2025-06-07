using FlappyBird.Config;
using UnityEngine;

namespace FlappyBird.Core
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private AudioConfig config;
        private AudioSource _source;

        private void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _source = GetComponent<AudioSource>();
        }

        public void PlayJump() => _source.PlayOneShot(config.jumpClip);
        public void PlayHit() => _source.PlayOneShot(config.hitClip);
        public void PlayEat() => _source.PlayOneShot(config.eatClip);
        public void PlayRandomPoint() {
            if (config.pointClips == null || config.pointClips.Length == 0) return;
            _source.PlayOneShot(config.pointClips[Random.Range(0, config.pointClips.Length)]);
        }
    }
}
