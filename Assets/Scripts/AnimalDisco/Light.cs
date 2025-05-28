using System.Collections;
using UnityEngine;


/*** Script, that changes color of Light randomly ***/
namespace AnimalDisco
{
    public class Light : MonoBehaviour
    {
        private SpriteRenderer sr;

        private void Awake() => sr = GetComponent<SpriteRenderer>();

        private void Start() => StartCoroutine(ChangeColorCouroutine());


        // HSV = Hue (base color), Saturation (how much of that color), Value (black <-> white)
        private void ChangeColor() => sr.color = Random.ColorHSV(0f, 1f, 0.75f, 1f, 1f, 1f);

        private IEnumerator ChangeColorCouroutine()
        {
            while (true)
            {
                ChangeColor();
                yield return new WaitForSeconds(4f);
                ChangeColor();
                yield return new WaitForSeconds(5f);
            }
        }
    }
}
