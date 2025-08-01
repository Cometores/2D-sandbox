using UnityEngine;
using UnityEngine.Serialization;

namespace FlappyBird.Config
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Configs/FlappyBird/Audio")]
    public class AudioConfig : ScriptableObject
    {
        [Header("Player")]
        public AudioClip[] jumpClips;
        public AudioClip hitClip;
        public AudioClip eatClip;
        
        [Header("Soring")]
        public AudioClip[] pointClips;
        
        [Header( "UI")]
        public AudioClip[] hoverClips;
    }
}
