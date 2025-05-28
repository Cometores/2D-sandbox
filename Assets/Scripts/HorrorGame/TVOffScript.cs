using UnityEngine;
using UnityEngine.UI;

/* * * A script for a TV shutdown animation that uses 3 black pictures. One is the top bar, the bottom bar, and a black background across the screen. Used in room 12. * * */
namespace HorrorGame
{
    public class TVOffScript : MonoBehaviour
    {
        [SerializeField] private GameObject topBlack;
        [SerializeField] private GameObject bottomBlack;
        [SerializeField] private GameObject backgroundBlack;

        [SerializeField] private AudioClip tvOffClip;
        [SerializeField] private AudioClip tvOnClip;
        private AudioSource _tvAudio;

        private bool _wasFilled;
        private float _speed; // speed for animation
        private float _transp;

        private Image _topImg;
        private Image _bottomImg;
        private Image _backroundImg;


        private void Awake()
        {
            _tvAudio = GetComponent<AudioSource>();
            _topImg = topBlack.GetComponent<Image>();
            _bottomImg = bottomBlack.GetComponent<Image>();
            _backroundImg = backgroundBlack.GetComponent<Image>();
        }


        private void OnEnable()
        {
            // Restore the component's values if you enter the room a second time
            _speed = 2;
            _topImg.fillAmount = 0;
            _bottomImg.fillAmount = 0;
            _backroundImg.color = new Color(1, 1, 1, 0);
            _transp = 0;
            _topImg.enabled = true;
            _backroundImg.enabled = true;
            _bottomImg.enabled = true;
            _wasFilled = false;

            // TV on and off sound
            _tvAudio.PlayOneShot(tvOffClip);
            Invoke(nameof(PlayClipOn), 0.5f);
        }


        private void Update()
        {
            if (_wasFilled && _topImg.fillAmount == 0f)
            {
                // animation is complete
                _topImg.enabled = false;
                _backroundImg.enabled = false;
                _bottomImg.enabled = false;
            }

            else if (_topImg.fillAmount == 1f)
            {
                // The TV is off, change the direction of the animation
                _speed *= -1;
                _wasFilled = true;
            }

            _transp += _speed * Time.deltaTime;
            _topImg.fillAmount += _speed * Time.deltaTime;
            _bottomImg.fillAmount += _speed * Time.deltaTime;
            _backroundImg.color = new Color(1,1,1, _transp);
        }


        private void PlayClipOn() => _tvAudio.PlayOneShot(tvOnClip);
    }
}
