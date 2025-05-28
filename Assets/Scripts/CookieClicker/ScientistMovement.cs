using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script that sets the movement behavior of the Scientist */
public class ScientistMovement : MonoBehaviour
{
    //Coordinates that define a rectangle for movement within it
    public Vector3 xyMax;
    public Vector3 xyMin;

    Vector3 newPosition; // Position for moving forward

    [SerializeField] float walkSpeed = 2f; 
    float rotationSpeed; // Scientist can rotate at a random speed
    bool isRotating;
    int rotationDirection = 1;


    void Start() => RotateUpdate();

    void Update()
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

    void RotateUpdate()
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
