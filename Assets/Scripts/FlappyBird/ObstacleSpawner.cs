using UnityEngine;

/* Spawner for stones, boss, and bonus pills */
namespace FlappyBird
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private float spawnTimer;
        [SerializeField] private GameObject rockUp;
        [SerializeField] private GameObject rockDown;
        [SerializeField] private GameObject boss;
        [SerializeField] private GameObject bonusPill;
        [SerializeField] private GameObject rocks;

        [SerializeField] private GameObject rockUpSpawnPos;
        [SerializeField] private GameObject rockDownSpawnPos;
        [SerializeField] private GameObject bossSpawnPos;
        [SerializeField] private GameObject bonusPillSpawnPos;
        [SerializeField] private GameObject rocksSpawnPos;

        [SerializeField] private float rocksTimer = 9f;
        
        private float _spawnCounter;

        private void Start()
        {
            InvokeRepeating(nameof(Spawner), 0f, 2f);
            InvokeRepeating(nameof(SpawnRocks), 0f, rocksTimer);
            
        }

        private void SpawnRocks()
        {
            Instantiate(rocks, rocksSpawnPos.transform.position, Quaternion.identity);
        }
        
        private void Spawner()
        {
            _spawnCounter++;

            // Spawn either a rock or a boss
            if (_spawnCounter % 7 == 0)
                Instantiate(boss, bossSpawnPos.transform.position, Quaternion.identity);
            else if (_spawnCounter % 2 == 0)
                Instantiate(rockDown, rockDownSpawnPos.transform.position, Quaternion.identity);
            else 
                Instantiate(rockUp, rockUpSpawnPos.transform.position, Quaternion.identity);

            // Sometimes we also spawn a bonus pill
            if (_spawnCounter % 12 == 0 || _spawnCounter == 3)
                Instantiate(bonusPill, bonusPillSpawnPos.transform.position, Quaternion.identity);
        }
    }
}
