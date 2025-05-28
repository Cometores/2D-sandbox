using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * The script adds npcs directional movement to the player * * */
public class PursuitMovement : MonoBehaviour
{
    public GameObject player;
    [SerializeField] float convergenceSpeed = 5;

    void Update() => transform.position = Vector3.MoveTowards(transform.position, player.transform.position, convergenceSpeed * Time.deltaTime);
}
