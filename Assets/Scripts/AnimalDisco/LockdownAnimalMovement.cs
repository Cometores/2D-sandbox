using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * A script that describes the movement of a masked cow on the screen * * */
public class LockdownAnimalMovement : MonoBehaviour
{
    Camera camMain;
    [SerializeField] float speed = 7f;

    void Awake() => camMain = Camera.main;

    void Update()
    {
        transform.position += Time.deltaTime * Vector3.right * speed;

        Vector2 bottomLeft = camMain.ViewportToWorldPoint(Vector3.zero);
        Vector2 topRight = camMain.ViewportToWorldPoint(Vector3.one);

        // If the object leaves the screen, destroy it
        if (transform.position.x < bottomLeft.x || transform.position.x > topRight.x ||
            transform.position.y < bottomLeft.y || transform.position.y > topRight.y)
            Destroy(gameObject);
    }
}
