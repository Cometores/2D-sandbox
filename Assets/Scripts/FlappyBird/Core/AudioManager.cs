using FlappyBird.Config;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FlappyBird.Core
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        public bool IsMuted => _Volume <= 0.05;
        [SerializeField] private float _Volume;

        public float Volume => _Volume;
        [SerializeField] private float _VolumeBeforeMute = 0;

        public EventHandler<VolumenChangedEventArgs> OnVolumeChanged { get; set; }
        
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
            
            
            _Volume = PlayerPrefs.GetFloat("Volume");
            ApplyNewVolume();
        }
        
        public void ToggleMute()
        {
            if (IsMuted)
            {
                _Volume = _VolumeBeforeMute;
                if (_Volume < 0.05f)
                    _Volume = 0.5f;
            }
            else
            {
                _VolumeBeforeMute = AudioManager.Instance.Volume;
                _Volume = 0;
            }

            PlayerPrefs.SetFloat("Volume", _Volume);
            PlayerPrefs.Save();
            ApplyNewVolume();
            OnVolumeChanged?.Invoke(this, new VolumenChangedEventArgs() { OldVolume = 0, NewVolume = _Volume });
        }

        public void SetVolume(float newVolume)
        {
            float old = _Volume;
            _Volume = newVolume;

            if (newVolume < 0.05)
                _Volume = 0;

            PlayerPrefs.SetFloat("Volume", _Volume);
            PlayerPrefs.Save();
            ApplyNewVolume();
            OnVolumeChanged?.Invoke(this, new VolumenChangedEventArgs(){ OldVolume = old, NewVolume = _Volume} );
        }

        private void ApplyNewVolume() => _aSource.volume = _Volume;
        public void PlayJump() => _aSource.PlayOneShot(config.jumpClip);
        public void PlayHit() => _aSource.PlayOneShot(config.hitClip);
        public void PlayEat() => _aSource.PlayOneShot(config.eatClip);
        public void PlayRandomPoint()
        {
            if (config.pointClips == null || config.pointClips.Length == 0) return;
            _aSource.PlayOneShot(config.pointClips[Random.Range(0, config.pointClips.Length)]);
        }
        public void PlayUIHover()
        {
            if (!IsMuted)
                _aSource.PlayOneShot(config.hoverClip);
        }
    }

    public sealed class VolumenChangedEventArgs
    {
        public float OldVolume { get; set; }
        public float NewVolume { get; set; }
    }
}