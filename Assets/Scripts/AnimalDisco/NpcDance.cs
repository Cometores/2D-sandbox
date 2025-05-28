using UnityEngine;

namespace AnimalDisco
{
    public class NpcDance : DanceMovements
    {
        private int iRand;

        private void Start()
        {
            base.Start();
            InvokeRepeating(nameof(RandomDance), 0f, 3f);
        }

        private void RandomDance()
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

        private void Update()
        {
            base.Update();
        }
    }
}
