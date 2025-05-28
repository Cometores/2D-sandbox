using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script that sets the movement behavior of the Student */
public class StudentMover : MonoBehaviour
{
    bool isRotating;    // Student can rotate or wait
    int direction = 1;  // Student can rotate clockwise or counterclockwise
    float rotationSpeed; // Student can rotate at a random speed


    void Start() => RotateUpdate();

    void Update()
    {
        if (isRotating)
            transform.Rotate(new Vector3(0, 0, rotationSpeed * direction) * Time.deltaTime);
    }

    void RotateUpdate()
    {
        isRotating = Random.Range(0f, 1f) > 0.3;    // Chance of rotation 70%
        rotationSpeed = Random.Range(180f, 400f);
        direction = isRotating ? -direction : direction;

        // Student can rotate or wait from 0.9 to 1.5 seconds
        Invoke(nameof(RotateUpdate), Random.Range(0.9f, 1.5f));
    }
}
