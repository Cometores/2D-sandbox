using UnityEngine;

namespace FlappyBird
{
    /// <summary>
    /// Forbid the special trigger to rotate with the boss.
    /// </summary>
    public class FreezeRotation : MonoBehaviour
    {
        private void Update() => transform.rotation = Quaternion.identity;
    }
}
