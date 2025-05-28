using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * plays the clip after some number of seconds, when you enable Game object * * */
public class PlayClipAfter : MonoBehaviour
{
    AudioSource auSource;
    [SerializeField] AudioClip clip;
    [SerializeField] float seconds;

    void Awake() => auSource = GetComponent<AudioSource>();

    void OnEnable() => Invoke(nameof(PlayClip), seconds);

    void PlayClip() => auSource.PlayOneShot(clip);
}
