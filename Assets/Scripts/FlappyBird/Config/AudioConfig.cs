using UnityEngine;

namespace FlappyBird.Config
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Configs/FlappyBird/Audio")]
    public class AudioConfig : ScriptableObject
    {
        [Header("Player")]
        public AudioClip jumpClip;
        public AudioClip hitClip;
        public AudioClip eatClip;
        
        [Header("Soring")]
        public AudioClip[] pointClips;
        
        [Header( "UI")]
        public AudioClip hoverClip;
    }
}
