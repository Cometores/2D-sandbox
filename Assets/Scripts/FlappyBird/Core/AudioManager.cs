using FlappyBird.Config;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FlappyBird.Core
{
    public class AudioManager : MonoBehaviour
    {
        private const float MUTE_THRESHOLD = 0.05f;
        private const float DEFAULT_VOLUME = 0.5f;
        
        [SerializeField] private AudioConfig config;
        public static AudioManager Instance { get; private set; }
        public bool IsMuted => _volume <= MUTE_THRESHOLD;
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
            _volume = PlayerPrefs.GetFloat("Volume", DEFAULT_VOLUME);
            ApplyVolume(_volume);
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

            ApplyVolume(_volume);
            RaiseVolumeChangedEvent(oldVolume, _volume);
        }

        public void SetVolume(float newVolume)
        {
            float clamped = VolumeUtils.ClampVolume(newVolume, MUTE_THRESHOLD);
            float oldVolume = _volume;

            if (Mathf.Approximately(oldVolume, clamped)) return;

            _volume = clamped;
            ApplyVolume(_volume);
            RaiseVolumeChangedEvent(oldVolume, _volume);
        }

        private void ApplyVolume(float volume)
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

        public void PlayJump() => PlaySound(config.jumpClip);
        public void PlayHit() => PlaySound(config.hitClip);
        public void PlayEat() => PlaySound(config.eatClip);
        public void PlayRandomScoringPoint()
        {
            if (config.pointClips.Length > 0)
            {
                PlaySound(config.pointClips[Random.Range(0, config.pointClips.Length)]);
            }
        }
        public void PlayUIHover() => PlaySound(config.hoverClip);

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

    public sealed class VolumeChangedEventArgs : EventArgs
    {
        public float OldVolume { get; }
        public float NewVolume { get; }

        public VolumeChangedEventArgs(float oldVolume, float newVolume)
        {
            OldVolume = oldVolume;
            NewVolume = newVolume;
        }
    }
}
