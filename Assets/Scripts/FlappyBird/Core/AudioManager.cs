using FlappyBird.Config;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FlappyBird.Core
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioConfig config;
        public static AudioManager Instance { get; private set; }
        public bool IsMuted => _volume <= Constants.MUTE_THRESHOLD;
        public float Volume => _volume;
        public event EventHandler<VolumeChangedEventArgs> VolumeChanged;

        private AudioSource _audioSource;
        private float _volume;
        private float _volumeBeforeMute;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _volume = PlayerPrefs.GetFloat("Volume", Constants.DEFAULT_VOLUME);
            if (_volume == 0)
                _volume = Constants.DEFAULT_VOLUME;
            
            ApplyAndSaveVolume(_volume);
            RaiseVolumeChangedEvent(_volume, _volume);
        }

        public void ToggleMute()
        {
            float oldVolume = _volume;

            if (IsMuted)
            {
                _volume = _volumeBeforeMute;
            }
            else
            {
                _volumeBeforeMute = _volume;
                _volume = 0f;
            }

            ApplyAndSaveVolume(_volume);
            RaiseVolumeChangedEvent(oldVolume, _volume);
        }

        public void SetVolume(float newVolume)
        {
            float clamped = VolumeUtils.ClampVolume(newVolume, Constants.MUTE_THRESHOLD);
            float oldVolume = _volume;

            if (Mathf.Approximately(oldVolume, clamped)) return;

            _volume = clamped;
            ApplyAndSaveVolume(_volume);
            RaiseVolumeChangedEvent(oldVolume, _volume);
        }

        private void ApplyAndSaveVolume(float volume)
        {
            _audioSource.volume = volume;
            PlayerPrefs.SetFloat("Volume", volume);
            PlayerPrefs.Save();
        }

        private void RaiseVolumeChangedEvent(float oldVolume, float newVolume)
        {
            VolumeChanged?.Invoke(this, new VolumeChangedEventArgs(oldVolume, newVolume));
        }

        #region Sound Shots

        public void PlayJump() => PlayRandomSound(config.jumpClips);
        public void PlayHit() => PlaySound(config.hitClip);
        public void PlayEat() => PlaySound(config.eatClip);
        public void PlayRandomScoringPoint() => PlayRandomSound(config.pointClips);
        public void PlayRandomUIHover() => PlayRandomSound(config.hoverClips);
        private void PlayRandomSound(AudioClip[] configPointClips)
        {
            if (configPointClips.Length > 0)
            {
                PlaySound(configPointClips[Random.Range(0, configPointClips.Length)]);
            }
        }
        
        private void PlaySound(AudioClip clip)
        {
            if (!IsMuted && clip != null)
            {
                _audioSource.PlayOneShot(clip);
            }
        }

        #endregion
    }

    public static class VolumeUtils
    {
        public static float ClampVolume(float volume, float muteThreshold) =>
            volume < muteThreshold ? 0f : Mathf.Clamp01(volume);
    }
}
