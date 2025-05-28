using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * A script that describes player movements and sprite changes * * */
public class PlayerMovements : MonoBehaviour
{
    bool isStealth;
    bool isAcceleration;

    [SerializeField] float speed = 3f;
    [SerializeField] float stealthSpeed = 0.45f;    // factor for stealth moving
    [SerializeField] float acceleration = 1.25f;    // factor for acceleration moving

    SpriteRenderer sr;
    [SerializeField] Sprite[] sprites;  // Sprites-array for our hero
    int lenSprites;                     // length of this array
    int randSprite;                     // random Sprite-Index for Key-F


    /* Normal color for our hero and transparent color for stealth mode */
    Color[] colors = { new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 0.5f) };


    private void Awake()
    {
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        lenSprites = sprites.Length;
    }


    void Update()
    {
        /*** CHARACTER STATE ***/
        /* Is the character in a state of stealth or acceleration */
        // Acceleration proof:
        isAcceleration = Input.GetKey(KeyCode.LeftShift);

        // Stealth proof
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isStealth = !isStealth;
            sr.color = colors[isStealth ? 1 : 0];   //make hero transparent, if necessary
        }


        /*** PLAYER MOVEMENT ***/
        Vector3 moveVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) moveVector.y = 1;
        if (Input.GetKey(KeyCode.A)) moveVector.x = -1;
        if (Input.GetKey(KeyCode.S)) moveVector.y = -1;
        if (Input.GetKey(KeyCode.D)) moveVector.x = 1;

        moveVector.Normalize(); // Normalize - that the hero moves equally fast horizontally and diagonally 

        /* Calculation of speed relative to stealth and acceleration */
        transform.position += speed * Time.deltaTime * moveVector *
            ((isAcceleration && !isStealth) ? acceleration: 1) *
            (isStealth ? stealthSpeed : 1);


        /*** SPRITE CHANGES ***/
        // Random Sprite
        if (Input.GetKeyDown(KeyCode.F))
        {
            randSprite = Random.Range(0, lenSprites);
            // If our new Sprite is equal to the previous one
            if (sr.sprite == sprites[randSprite]) randSprite = (randSprite + 1) % lenSprites;
            sr.sprite = sprites[randSprite];
        }
    }
}
