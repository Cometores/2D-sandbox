using FlappyBird.Config;
using UnityEngine;

namespace FlappyBird.Core
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        public bool IsMuted { get; private set; }
        
        [SerializeField] private AudioConfig config;
        private AudioSource _aSource;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
            
            _aSource = GetComponent<AudioSource>();
            IsMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
            ApplyMute();
        }
        
        public void ToggleMute()
        {
            IsMuted = !IsMuted;
            ApplyMute();
            PlayerPrefs.SetInt("Muted", IsMuted ? 1 : 0);
            PlayerPrefs.Save();
        }

        private void ApplyMute() => AudioListener.volume = IsMuted ? 0f : 1f;
        public void PlayJump() => _aSource.PlayOneShot(config.jumpClip);
        public void PlayHit() => _aSource.PlayOneShot(config.hitClip);
        public void PlayEat() => _aSource.PlayOneShot(config.eatClip);
        public void PlayRandomPoint()
        {
            if (config.pointClips == null || config.pointClips.Length == 0) return;
            _aSource.PlayOneShot(config.pointClips[Random.Range(0, config.pointClips.Length)]);
        }
    }
}