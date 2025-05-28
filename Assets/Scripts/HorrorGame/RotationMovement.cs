using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* * * Script for pizza rotation animation on the start screen, and poison in room 3 * * */
public class RotationMovement : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 20f;
    [SerializeField] float rotationTime = 1f;
    float timer;

    // Divide the animation time by 2, so that the left and right animation is symmetrical
    private void Awake() => timer = rotationTime / 2;

    void Update()
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
