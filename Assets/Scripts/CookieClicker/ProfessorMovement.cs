using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorMovement : MonoBehaviour
{
    //Coordinates that define a rectangle for movement within it
    public Vector3 xyMax;
    public Vector3 xyMin;

    Vector3 newPosition;// Position for moving forward

    [SerializeField] float walkSpeed = 1f;
    int direction = 1;

    SpriteRenderer sr;

    void Awake() => sr = GetComponent<SpriteRenderer>();

    void Update()
    {   
        newPosition = (transform.position + Vector3.up * walkSpeed * Time.deltaTime * direction);
        //Restrict the scientist's exit from the rectangle with the clamp function
        transform.position = new Vector3(Mathf.Clamp(newPosition.x, xyMin.x, xyMax.x), Mathf.Clamp(newPosition.y, xyMin.y, xyMax.y), 0);

        // Change movement and sprite direction if necessary
        if (newPosition.y >= xyMax.y || newPosition.y <= xyMin.y)
        {
            direction *= -1;
            sr.flipX = !sr.flipX;
        }
    }
}
