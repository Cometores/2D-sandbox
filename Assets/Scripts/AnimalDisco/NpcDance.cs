using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDance : DanceMovements
{
    int iRand;

    void Start()
    {
        base.Start();
        InvokeRepeating(nameof(RandomDance), 0f, 3f);
    }

    void RandomDance()
    {
        int newRand = Random.Range(0, 3);
        if (iRand == newRand) newRand = (newRand + 1) % 3;
        iRand = newRand;

        switch (iRand)
        {
            case 0:
                TryToStartPositionDanceMove();
                break;
            case 1:
                TryToStartRotationDanceMove();
                break;
            default:
                TryToStartScalingDanceMove();
                break;
        }
    }

    void Update()
    {
        base.Update();
    }
}
