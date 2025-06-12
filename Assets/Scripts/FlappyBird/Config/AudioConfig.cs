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
        
        [Header( "UI")]
        public AudioClip[] pointClips;
        public AudioClip hoverClip;
    }
}
