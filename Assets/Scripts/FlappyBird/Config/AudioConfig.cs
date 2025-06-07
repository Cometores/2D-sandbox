using UnityEngine;

namespace FlappyBird.Config
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Configs/Audio")]
    public class AudioConfig : ScriptableObject
    {
        public AudioClip jumpClip;
        public AudioClip hitClip;
        public AudioClip eatClip;
        public AudioClip[] pointClips;
    }
}
