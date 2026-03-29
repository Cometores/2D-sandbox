using UnityEngine;

namespace CookieClicker
{
    public class ProfessorMovement : MonoBehaviour
    {
        //Coordinates that define a rectangle for movement within it
        public Vector3 xyMax;
        public Vector3 xyMin;

        private Vector3 _newPosition;// Position for moving forward

        [SerializeField] private float walkSpeed = 1f;
        private int _direction = 1;

        private SpriteRenderer _sr;

        private void Awake() => _sr = GetComponent<SpriteRenderer>();

        private void Start() => transform.rotation = Quaternion.Euler(0, 0, 90);

        private void Update()
        {
            _newPosition = (transform.position + transform.right * (walkSpeed * Time.deltaTime * _direction));
            //Restrict the scientist's exit from the rectangle with the clamp function
            transform.position = new Vector3(Mathf.Clamp(_newPosition.x, xyMin.x, xyMax.x), Mathf.Clamp(_newPosition.y, xyMin.y, xyMax.y), 0);

            // Change movement and sprite direction if necessary
            if (_newPosition.y >= xyMax.y || _newPosition.y <= xyMin.y)
            {
                _direction *= -1;
                _sr.flipX = !_sr.flipX;
            }
        }
    }
}
