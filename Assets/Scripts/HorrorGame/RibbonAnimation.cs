using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/* * * Ribbon animation in the last room. Uses "fill" * * */
namespace HorrorGame
{
    public class RibbonAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject leftRibbon;
        [SerializeField] private GameObject rightRibbon;

        private Image _leftImg;
        private Image _rightImg;

        [SerializeField] private float speed = 3f;

        private void Awake()
        {
            _leftImg = leftRibbon.GetComponent<Image>();
            _rightImg = rightRibbon.GetComponent<Image>();
        }

        private void Start() => StartCoroutine(RibbonFill());


        private IEnumerator RibbonFill()
        {
            yield return new WaitForSeconds(0.7f);
            while (true)
            {
                if (_rightImg.fillAmount == 1) StopAllCoroutines();
                _leftImg.fillAmount += speed * Time.deltaTime;
                _rightImg.fillAmount += speed * Time.deltaTime;
                yield return null;
            }
        }
    }
}
