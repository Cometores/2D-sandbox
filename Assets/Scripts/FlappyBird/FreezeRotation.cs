using UnityEngine;

/* forbid the special trigger to rotate with the boss. Couldn't find a more elegant way to do it */
namespace FlappyBird
{
    public class FreezeRotation : MonoBehaviour
    {
        private void Update() => transform.rotation = Quaternion.identity;
    }
}
