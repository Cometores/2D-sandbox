using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playEndMusic : MonoBehaviour
{
    [SerializeField] GameObject mainGameObject;
    AudioSource auSource;
    [SerializeField] AudioClip endClip;

    private void Awake() => auSource = mainGameObject.GetComponent<AudioSource>();

    void OnEnable()
    {
        auSource.clip = endClip;
        auSource.Play();
    }
}
