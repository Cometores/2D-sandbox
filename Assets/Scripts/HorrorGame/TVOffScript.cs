using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* * * A script for a TV shutdown animation that uses 3 black pictures. One is the top bar, the bottom bar, and a black background across the screen. Used in room 12. * * */
public class TVOffScript : MonoBehaviour
{
    [SerializeField] GameObject topBlack;
    [SerializeField] GameObject bottomBlack;
    [SerializeField] GameObject backgroundBlack;

    [SerializeField] AudioClip tvOffClip;
    [SerializeField] AudioClip tvOnClip;
    AudioSource tvAudio;

    bool wasFilled;
    float speed; // speed for animation
    float transp;

    Image topImg;
    Image bottomImg;
    Image backroundImg;


    private void Awake()
    {
        tvAudio = GetComponent<AudioSource>();
        topImg = topBlack.GetComponent<Image>();
        bottomImg = bottomBlack.GetComponent<Image>();
        backroundImg = backgroundBlack.GetComponent<Image>();
    }


    void OnEnable()
    {
        // Restore the component's values if you enter the room a second time
        speed = 2;
        topImg.fillAmount = 0;
        bottomImg.fillAmount = 0;
        backroundImg.color = new Color(1, 1, 1, 0);
        transp = 0;
        topImg.enabled = true;
        backroundImg.enabled = true;
        bottomImg.enabled = true;
        wasFilled = false;

        // TV on and off sound
        tvAudio.PlayOneShot(tvOffClip);
        Invoke(nameof(PlayClipOn), 0.5f);
    }


    void Update()
    {
        if (wasFilled && topImg.fillAmount == 0f)
        {
            // animation is complete
            topImg.enabled = false;
            backroundImg.enabled = false;
            bottomImg.enabled = false;
        }

        else if (topImg.fillAmount == 1f)
        {
            // The TV is off, change the direction of the animation
            speed *= -1;
            wasFilled = true;
        }

        transp += speed * Time.deltaTime;
        topImg.fillAmount += speed * Time.deltaTime;
        bottomImg.fillAmount += speed * Time.deltaTime;
        backroundImg.color = new Color(1,1,1, transp);
    }


    void PlayClipOn() => tvAudio.PlayOneShot(tvOnClip);
}
