using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* * * Ribbon animation in the last room. Uses "fill" * * */
public class RibbonAnimation : MonoBehaviour
{
    [SerializeField] GameObject leftRibbon;
    [SerializeField] GameObject rightRibbon;

    Image leftImg;
    Image rightImg;

    [SerializeField] float speed = 3f;

    private void Awake()
    {
        leftImg = leftRibbon.GetComponent<Image>();
        rightImg = rightRibbon.GetComponent<Image>();
    }

    void Start() => StartCoroutine(RibbonFill());


    IEnumerator RibbonFill()
    {
        yield return new WaitForSeconds(0.7f);
        while (true)
        {
            if (rightImg.fillAmount == 1) StopAllCoroutines();
            leftImg.fillAmount += speed * Time.deltaTime;
            rightImg.fillAmount += speed * Time.deltaTime;
            yield return null;
        }
    }
}
