using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* * * Script that describes the behavior of a boss * * */
public class BossAction : MonoBehaviour
{
    Rigidbody2D rb;
    AudioSource auSource;
    
    [SerializeField] GameObject attack;
    [SerializeField] GameObject explosion;
    [SerializeField] AudioClip hitClip;
    [SerializeField] GameObject hitLight;
    [SerializeField] float movementSpeed = 3f;

    float movementDirection = 1;    // 1 = Up; -1 = Down
    int life = 15;
    int attackSpawnCnt = 3;     // need for different attack pos
    int actualAttack;

    [HideInInspector] public TextMeshProUGUI scoreTxt;
    [HideInInspector] public List<Vector3> attackSpawnPos = new List<Vector3>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        auSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        BossFire(); // looped function for firing 
        BossChangeDirection(); // special movement behavior
        rb.velocity = Vector2.up * movementSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // When the boss rests up or down the screen, it changes the direction of movement
        if (collision.gameObject.name == "UpperBound")
        {
            movementDirection = -1;
            rb.velocity = movementDirection * movementSpeed * Vector2.up;
        }
        else if (collision.gameObject.name == "LowerBound")
        {
            movementDirection = 1;
            rb.velocity = movementDirection * movementSpeed * Vector2.up;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);  // Destroy Bullet
            life--;

            if (life <= 0)
            {
                Instantiate(explosion, transform.position, Quaternion.Euler(-90,0,0));
                scoreTxt.text = (int.Parse(scoreTxt.text) + 1).ToString();
                Destroy(gameObject);
            }
            else
            {
                auSource.PlayOneShot(hitClip);
                hitLight.SetActive(true); // Set hit color with Light
                
                Invoke(nameof(ReturnColor), 0.1f);  // Set color to normal
            }
        }
    }

    void BossFire()
    {
        // 3 same attacks at different positions 
        Instantiate(attack, attackSpawnPos[actualAttack], Quaternion.identity);
        actualAttack = (actualAttack + 1) % attackSpawnCnt;
        Invoke(nameof(BossFire), 2.5f);
    }

    void BossChangeDirection()
    {
        // random movement behavior
        movementDirection = Random.Range(-1, 2);
        rb.velocity = movementDirection * movementSpeed * Vector2.up;
        Invoke(nameof(BossChangeDirection), 1.5f);
    }

    void ReturnColor() => hitLight.SetActive(false);
}
