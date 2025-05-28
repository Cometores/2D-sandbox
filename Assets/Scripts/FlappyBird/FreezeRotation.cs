using UnityEngine;

/* forbid the special trigger to rotate with the boss. Couldn't find a more elegant way to do it */
public class FreezeRotation : MonoBehaviour
{
    void Update() => transform.rotation = Quaternion.identity;
}
