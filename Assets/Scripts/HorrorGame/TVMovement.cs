using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* * * Script for the animation of hitting the TV in room 13 * * */
public class TVMovement : MonoBehaviour
{
    Vector3 normalPosition;
    [SerializeField] float speed = 70;
    float phase; // phase for sin
    Image tvImg;

    private void Awake() => tvImg = GetComponent<Image>();

    void OnEnable()
    {
        tvImg.enabled = true;
        normalPosition = transform.position;
        Invoke(nameof(destoryTv), 0.1f);
    }


    void Update()
    {
        if (tvImg.enabled)
        {
            // Use the displacement of the x-coordinate in the sine phase
            transform.position = normalPosition + Vector3.right * (Mathf.Sin(phase * speed)) * 30;
            phase += Time.deltaTime;
        }
    }

    void destoryTv()
    {
        tvImg.enabled = false;
        transform.position = normalPosition;
    }
}
