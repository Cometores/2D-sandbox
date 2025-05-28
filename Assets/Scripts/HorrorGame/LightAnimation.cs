using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* * * A script for lighting animation, which essentially just makes another picture transparent * * *
 * * * Used in rooms 5, 7, 11 * * */

public class LightAnimation : MonoBehaviour
{
    Image lightImg; // A picture of a lighter room
    [SerializeField] float animationSpeed = 1.8f;
    float transp;

    void Awake() => lightImg = GetComponent<Image>();

    void Update()
    {
        transp += animationSpeed * Time.deltaTime;
        if (transp >= 1 || transp <= 0) animationSpeed *= -1;
        lightImg.color = new Color(1, 1, 1, transp);
    }
}
