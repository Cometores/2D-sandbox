using UnityEngine;

/* * * A script that describes the movement of a masked cow on the screen * * */
namespace AnimalDisco
{
    public class LockdownAnimalMovement : MonoBehaviour
    {
        private Camera camMain;
        [SerializeField] private float speed = 7f;

        private void Awake() => camMain = Camera.main;

        private void Update()
        {
            transform.position += Time.deltaTime * Vector3.right * speed;

            Vector2 bottomLeft = camMain.ViewportToWorldPoint(Vector3.zero);
            Vector2 topRight = camMain.ViewportToWorldPoint(Vector3.one);

            // If the object leaves the screen, destroy it
            if (transform.position.x < bottomLeft.x || transform.position.x > topRight.x ||
                transform.position.y < bottomLeft.y || transform.position.y > topRight.y)
                Destroy(gameObject);
        }
    }
}
