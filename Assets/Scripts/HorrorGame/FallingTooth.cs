using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/* The script for the falling tooth in room 5 */
namespace HorrorGame
{
    public class FallingTooth : MonoBehaviour
    {
        [SerializeField] private float speed = 3f; // tooth fall speed
        private Vector3 _normalPosition;

        private void OnEnable()
        {
            _normalPosition = transform.position;
            GetComponent<Image>().enabled = true;
            StartCoroutine(ToothFall());
        }

        private IEnumerator ToothFall()
        {
            yield return new WaitForSeconds(0.7f);
            while (true)
            {
                // If a tooth falls where it needs to, we turn it off
                if (transform.position.y <= 380)
                {
                    GetComponent<Image>().enabled = false;
                    transform.position = _normalPosition;
                    StopAllCoroutines();
                }
                transform.position += Vector3.down * (speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
