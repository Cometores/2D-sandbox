using UnityEngine;
using UnityEngine.UI;

/* * * Script for eye-rolling animation in rooms 2, 5, 10, 14 * * */
namespace HorrorGame
{
    public class RotatingEyes : MonoBehaviour
    {
        [SerializeField] private GameObject rightEye;
        [SerializeField] private GameObject leftEye;

        private Image _rightImg;
        private Image _leftImg;

        [SerializeField] private float rotationSpeed = 2300f;


        private void Awake()
        {
            _rightImg = rightEye.GetComponent<Image>();
            _leftImg = leftEye.GetComponent<Image>();
        }


        private void Update()
        {
            _rightImg.transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
            _leftImg.transform.Rotate(new Vector3(0, 0, -rotationSpeed) * Time.deltaTime);
        }
    }
}
