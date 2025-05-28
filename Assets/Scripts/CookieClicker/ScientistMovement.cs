using UnityEngine;

/* Script that sets the movement behavior of the Scientist */
namespace CookieClicker
{
    public class ScientistMovement : MonoBehaviour
    {
        //Coordinates that define a rectangle for movement within it
        public Vector3 xyMax;
        public Vector3 xyMin;

        private Vector3 newPosition; // Position for moving forward

        [SerializeField] private float walkSpeed = 2f;
        private float rotationSpeed; // Scientist can rotate at a random speed
        private bool isRotating;
        private int rotationDirection = 1;


        private void Start() => RotateUpdate();

        private void Update()
        {
            if (isRotating)
                transform.Rotate(new Vector3(0, 0, rotationSpeed * rotationDirection) * Time.deltaTime);
            else
            {
                newPosition = (transform.position + transform.right * walkSpeed * Time.deltaTime);
                //Restrict the scientist's exit from the rectangle with the clamp function
                transform.position = new Vector3(Mathf.Clamp(newPosition.x, xyMin.x, xyMax.x), Mathf.Clamp(newPosition.y, xyMin.y, xyMax.y), 0);
            }
        }

        private void RotateUpdate()
        {
            isRotating = !isRotating;

            if (isRotating)
            {
                rotationDirection = -rotationDirection;
                rotationSpeed = Random.Range(180f, 400f);
                Invoke(nameof(RotateUpdate), Random.Range(0.3f, 0.8f));
            }
            else
                Invoke(nameof(RotateUpdate), Random.Range(0.9f, 1.5f));
        }
    }
}
