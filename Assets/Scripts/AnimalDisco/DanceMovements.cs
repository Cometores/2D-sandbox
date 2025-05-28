using System;
using System.Collections;
using UnityEngine;


/*** Parent class that describes movements for the NPC and the player ***/
/* I apply this script to the sprite so that the camera doesn't follow and rotate with the object. */
namespace AnimalDisco
{
    public class DanceMovements : MonoBehaviour
    {
        [SerializeField] protected float danceSpeed = 20f;
        protected float phase; // needed for sin dependent motion functions

        protected Vector3 normalScale; // that after the dance we can return normal scale
        protected Vector3 normalPosition; // needed for sin-dependent postion movement

        protected enum DanceMoveState { invalidDanceMove, rotationDanceMove, scalingDanceMove, positionDanceMove }
        protected DanceMoveState currentDanceMove;


        protected void Start() => normalScale = transform.localScale;


        protected void Update()
        {
            // Return the Skaling to its original value if it was lost during the scaling dance
            if (currentDanceMove != DanceMoveState.scalingDanceMove && transform.localScale != normalScale)
                transform.localScale = Vector3.MoveTowards(transform.localScale, normalScale, 0.01f);
        }


        protected void TryToStartRotationDanceMove()
        {
            if (currentDanceMove != DanceMoveState.rotationDanceMove)
            {
                StopAllCoroutines();
                currentDanceMove = DanceMoveState.rotationDanceMove;
                StartCoroutine(DanceCouroutine(nameof(RotationDanceMove)));
            }
        }


        protected void TryToStartScalingDanceMove()
        {
            if (currentDanceMove != DanceMoveState.scalingDanceMove)
            {
                phase = 0f;
                StopAllCoroutines();
                currentDanceMove = DanceMoveState.scalingDanceMove;
                StartCoroutine(DanceCouroutine(nameof(ScalingDanceMove)));
            }
        }


        protected void TryToStartPositionDanceMove ()
        {
            if (currentDanceMove != DanceMoveState.positionDanceMove)
            {
                phase = 0f;
                normalPosition = transform.position;
                StopAllCoroutines();
                currentDanceMove = DanceMoveState.positionDanceMove;
                StartCoroutine(DanceCouroutine(nameof(PositionDanceMove)));
            }
        }


        protected void RotationDanceMove() => transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime * danceSpeed);


        protected void ScalingDanceMove()
        {
            transform.localScale = normalScale * (Math.Abs(Mathf.Sin(phase * danceSpeed)) + 1);
            phase += Time.deltaTime;
        }


        protected void PositionDanceMove()
        {
            transform.position = normalPosition + transform.right * (Mathf.Sin(phase * danceSpeed));
            phase += Time.deltaTime;
        }


        protected void StopDancing()
        {
            StopAllCoroutines();
            currentDanceMove = DanceMoveState.invalidDanceMove;
        }


        private IEnumerator DanceCouroutine(string funcName)
        {
            while (true)
            {
                Invoke(funcName, 0f);
                yield return null;
            }
        }
    }
}