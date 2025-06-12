using UnityEngine;

namespace FlappyBird.Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/FlappyBird/Player")]
    public class PlayerConfig : ScriptableObject
    {
        public float jumpForce = 5f;
        public float rotateSpeed = 15f;
        public int powerUpDuration = 10;
        public int scoreMultiplier = 2;
    }
}
