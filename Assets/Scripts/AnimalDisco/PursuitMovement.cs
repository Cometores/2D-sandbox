using UnityEngine;

/* * * The script adds npcs directional movement to the player * * */
namespace AnimalDisco
{
    public class PursuitMovement : MonoBehaviour
    {
        public GameObject player;
        [SerializeField] private float convergenceSpeed = 5;

        private void Update() => transform.position = Vector3.MoveTowards(transform.position, player.transform.position, convergenceSpeed * Time.deltaTime);
    }
}
