using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* * * Script that describes the behavior of an ordinary enemy * * */
public class EnemyActions : MonoBehaviour
{
    Rigidbody2D rb;
    AudioSource auSource;
    SpriteRenderer sr;

    [SerializeField] Sprite[] enemySprites;     // Different sprites for destruction system
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject explosion;
    [SerializeField] AudioClip hitClip;
    [SerializeField] GameObject boss;

    [SerializeField] float movementSpeed = 5f;
    float movementDirection = 1;    // 1 = Up; -1 = Down
    int life = 3;
    int spriteNr;

    Color colorHit = new Color(0.8f, 0.5f, 0.5f);   // Damage received color
    Color colorNormal = new Color(1, 1, 1);

    [HideInInspector] public GameObject bossLogic;      // pass to the boss, when spawn
    [HideInInspector] public TextMeshProUGUI scoreTxt;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        auSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();

    }

    void Start()
    {
        EnemyFire(); // looped function for firing 
        rb.velocity = Vector2.up * movementSpeed;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // When the enemy rests up or down the screen, it changes the direction of movement
        if (collision.gameObject.name == "UpperBound" || collision.gameObject.name == "LowerBound")
        {
            movementDirection *= -1;
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
                Instantiate(explosion, transform.position, Quaternion.identity);

                var actualScore = (int.Parse(scoreTxt.text) + 1);
                scoreTxt.text = actualScore.ToString();

                if (actualScore % 10 == 0) BossSpawn();

                Destroy(gameObject);  // Destroy Enemy
            }
            else 
            {
                auSource.PlayOneShot(hitClip);
                sr.sprite = enemySprites[spriteNr]; // Ñhange the sprite to a destroyed sprite
                sr.color = colorHit;    // Set color to hit
                spriteNr++;

                Invoke(nameof(ReturnColor), 0.1f);  // Set color to normal
            }
        }
    }

    void EnemyFire()
    {
        Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 90));
        Invoke(nameof(EnemyFire), 2);
    }

    void BossSpawn()
    {
        Vector3 spawnPos = bossLogic.transform.GetChild(0).transform.position;
        GameObject newBoss = Instantiate(boss, spawnPos, Quaternion.Euler(0, 0, -90));
        var bossScript = newBoss.GetComponent<BossAction>();

        // transfer variables to the boss
        for (int i = 1; i < 4; i++)
        {
            bossScript.attackSpawnPos.Add(
                bossLogic.transform.GetChild(i).transform.position);
        }
        bossScript.scoreTxt = scoreTxt;
    }

    void ReturnColor() => sr.color = colorNormal;
}
