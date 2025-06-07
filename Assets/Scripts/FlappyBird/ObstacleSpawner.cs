using UnityEngine;

namespace FlappyBird
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject rockUp;
        [SerializeField] private GameObject rockDown;
        [SerializeField] private GameObject rockDouble;
        [SerializeField] private GameObject boss;
        [SerializeField] private GameObject bonusPill;
        [SerializeField] private GameObject rocks;

        [SerializeField] private Transform rockUpSpawn;
        [SerializeField] private Transform rockDownSpawn;
        [SerializeField] private Transform rockDoubleSpawn;
        [SerializeField] private Transform bossSpawn;
        [SerializeField] private Transform bonusPillSpawn;
        [SerializeField] private Transform rocksSpawn;

        [SerializeField] private float spawnInterval = 2f;
        [SerializeField] private float rocksInterval = 6f;

        private int _spawnCounter;

        private void Start()
        {
            InvokeRepeating(nameof(SpawnObstacle), 0f, spawnInterval);
            InvokeRepeating(nameof(SpawnRocks), 0f, rocksInterval);
        }

        private void SpawnObstacle()
        {
            _spawnCounter++;

            // Spawn either a rock or a boss
            if (_spawnCounter % 7 == 0)
                Instantiate(boss, bossSpawn.position, Quaternion.identity);
            else if (_spawnCounter % 2 == 0)
                Instantiate(rockDown, rockDownSpawn.position, Quaternion.identity);
            else if (_spawnCounter % 5 == 0)
                Instantiate(rockDouble, rockDoubleSpawn.position, Quaternion.identity);
            else
                Instantiate(rockUp, rockUpSpawn.position, Quaternion.identity);

            // Spawn bonus bat
            if (_spawnCounter % 12 == 0 || _spawnCounter == 3)
                Instantiate(bonusPill, bonusPillSpawn.position, Quaternion.identity);
        }

        private void SpawnRocks()
        {
            Instantiate(rocks, rocksSpawn.position, Quaternion.identity);
        }
    }
}
