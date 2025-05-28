using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* * * Script for eye-rolling animation in rooms 2, 5, 10, 14 * * */
public class RotatingEyes : MonoBehaviour
{
    [SerializeField] GameObject rightEye;
    [SerializeField] GameObject leftEye;

    Image rightImg;
    Image leftImg;

    [SerializeField] float rotationSpeed = 2300f;


    private void Awake()
    {
        rightImg = rightEye.GetComponent<Image>();
        leftImg = leftEye.GetComponent<Image>();
    }
    

    void Update()
    {
        rightImg.transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
        leftImg.transform.Rotate(new Vector3(0, 0, -rotationSpeed) * Time.deltaTime);
    }
}
