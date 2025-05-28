using UnityEngine;
using UnityEngine.UI;

/* * * Script for animating noise on the TV in rooms 9, 11, 12 * * */
namespace HorrorGame
{
    public class TVNoise : MonoBehaviour
    {
        [SerializeField] private RawImage img;
        [SerializeField] private float x, y;
        [SerializeField] private AudioClip noiseClip;
        private AudioSource _tvAudio;

        private void Awake() => _tvAudio = GetComponent<AudioSource>();

        private void Start()
        {
            _tvAudio.clip = noiseClip;
            _tvAudio.Play();
        }

        private void Update() => img.uvRect = new Rect(img.uvRect.position + new Vector2(x, y) * Time.deltaTime, img.uvRect.size);
    }
}