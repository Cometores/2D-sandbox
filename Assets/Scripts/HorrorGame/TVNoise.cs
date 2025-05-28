using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* * * Script for animating noise on the TV in rooms 9, 11, 12 * * */
public class TVNoise : MonoBehaviour
{
    [SerializeField] RawImage img;
    [SerializeField] float x, y;
    [SerializeField] AudioClip noiseClip;
    AudioSource tvAudio;

    void Awake() => tvAudio = GetComponent<AudioSource>();

    void Start()
    {
        tvAudio.clip = noiseClip;
        tvAudio.Play();
    }

    void Update() => img.uvRect = new Rect(img.uvRect.position + new Vector2(x, y) * Time.deltaTime, img.uvRect.size);
}