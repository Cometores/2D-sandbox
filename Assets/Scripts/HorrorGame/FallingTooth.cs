using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* The script for the falling tooth in room 5 */
public class FallingTooth : MonoBehaviour
{
    [SerializeField] float speed = 3f; // tooth fall speed
    Vector3 normalPosition;


    void OnEnable()
    {
        normalPosition = transform.position;
        GetComponent<Image>().enabled = true;
        StartCoroutine(ToothFall());
    }


    IEnumerator ToothFall()
    {
        yield return new WaitForSeconds(0.7f);
        while (true)
        {
            // If a tooth falls where it needs to, we turn it off
            if (transform.position.y <= 380)
            {
                GetComponent<Image>().enabled = false;
                transform.position = normalPosition;
                StopAllCoroutines();
            }
            transform.position += Vector3.down * speed * Time.deltaTime;
            yield return null;
        }
    }
}
