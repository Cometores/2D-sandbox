using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* * * A script that creates the feeling of lights going off in room 4. * * *
 * * * Works almost exactly like LightAnimation Script * * */
public class LightOff : MonoBehaviour
{
    Image lightOn; // A picture of a lighter room
    [SerializeField] float animationSpeed = 0.5f;
    float transp;

    void Awake() => lightOn = GetComponent<Image>();


    private void OnEnable()
    {
        transp = 1;
        lightOn.enabled = true;
    }


    void Update()
    {
        if (transp <= 0) lightOn.enabled = false;

        transp += -animationSpeed * Time.deltaTime;
        lightOn.color = new Color(1, 1, 1, transp);
    }
}
