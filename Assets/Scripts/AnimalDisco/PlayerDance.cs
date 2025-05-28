using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * A class describing a dance for player * * */
public class PlayerDance : DanceMovements
{
    Quaternion lastDanceRotation;
    float lerpAlpha;

    void Update()
    {
        if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D))) StopDancing();
        if (Input.GetKey(KeyCode.Alpha1)) TryToStartRotationDanceMove();
        if (Input.GetKey(KeyCode.Alpha2)) TryToStartScalingDanceMove();
        if (Input.GetKey(KeyCode.Alpha3)) TryToStartPositionDanceMove();
        base.Update();

        // returning to normal rotation
        if (currentDanceMove != DanceMoveState.positionDanceMove) transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, 0.01f);
        if (currentDanceMove != DanceMoveState.rotationDanceMove)
        {
            if (lerpAlpha <= 1)
            {
                // returning to normal rotation
                transform.rotation = Quaternion.Lerp(lastDanceRotation, Quaternion.identity, lerpAlpha);
                lerpAlpha += Time.deltaTime / 0.5f;
            }
        }
    }

    void StopDancing()
    {
        if (currentDanceMove == DanceMoveState.rotationDanceMove)
        {
            // is needed in order to return the player to normal rotation
            lastDanceRotation = transform.rotation;
            lerpAlpha = 0;
        }
        base.StopDancing();
    }
}
