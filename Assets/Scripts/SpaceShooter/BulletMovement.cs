using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * Script, that describes Bullet Movement for Player and Enemy * * */
public class BulletMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 15f;
    [SerializeField] float movementDirection; // 1 for Player bullet (right), -1 for Enemy bullet (left)

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Start() => rb.velocity = Vector2.right * speed * movementDirection;

    void OnBecameInvisible() => Destroy(gameObject);
}
