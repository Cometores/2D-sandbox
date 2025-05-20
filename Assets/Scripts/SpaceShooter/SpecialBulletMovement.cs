using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * Script, that describes Special Bullet movement for Player in form of a sin-wave * * */
public class SpecialBulletMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed = 15f;
    [SerializeField] float frequency = 8;

    float yPos;         // actual y-Position
    float startYPos;    // y-Position at Start
    float timer;        // timer for Sin. Need to make bullet start at yPos = startYPos

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startYPos = transform.position.y;
    }

    void Start() => rb.velocity = Vector2.right * speed;

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        yPos = Mathf.Sin(timer * frequency) + startYPos;
        transform.position = new Vector3(transform.position.x, yPos, 0);
    }

    void OnBecameInvisible() => Destroy(gameObject);
}
