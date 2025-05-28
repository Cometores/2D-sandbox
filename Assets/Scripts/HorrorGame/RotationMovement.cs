using UnityEngine;

/* * * Script for pizza rotation animation on the start screen, and poison in room 3 * * */
namespace HorrorGame
{
    public class RotationMovement : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 20f;
        [SerializeField] private float rotationTime = 1f;
        private float timer;

        // Divide the animation time by 2, so that the left and right animation is symmetrical
        private void Awake() => timer = rotationTime / 2;

        private void Update()
        {
            if (timer >= rotationTime) 
            {
                // change the direction of the animation if necessary
                rotationSpeed *= -1;
                timer = 0;
            }

            transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
            timer += Time.deltaTime;
        }
    }
}
