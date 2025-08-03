using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.Enemies
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [Header("Obstacles")]
        [SerializeField] private List<SpawnItem> obstacleMappings;

        [Header("Fallback obstacle")]
        [SerializeField] private GameObject fallbackRock;
        [SerializeField] private Transform fallbackPoint;

        [Header("Collectables")]
        [SerializeField] private List<SpawnItem> collectableMappings;

        [Header("Bottom")]
        [SerializeField] private GameObject rocks;
        [SerializeField] private Transform rocksSpawn;

        [Header("Structure")]
        [SerializeField] private Transform spawnContainer;

        [Header("Timing")]
        [SerializeField] private float startSpawnInterval = 2f;
        [SerializeField] private float finalSpawnInterval = 1f;
        [SerializeField] private float timeToAchieve = 30f;
        [SerializeField] private float rocksEverySeconds = 5.6f;

        private float _spawnInterval;
        private int _spawnCounter;

        private void Awake()
        {
            ValidateMappings(obstacleMappings, "Obstacle");
            ValidateMappings(collectableMappings, "Collectable");
        }

        private void Start()
        {
            _spawnInterval = startSpawnInterval;
            StartCoroutine(SpawnLoop());
            StartCoroutine(RocksLoop());
        }

        private IEnumerator SpawnLoop()
        {
            float elapsed = 0f;

            while (true)
            {
                SpawnObstacle();

                elapsed += _spawnInterval;
                float t = Mathf.Clamp01(elapsed / timeToAchieve);
                _spawnInterval = Mathf.Lerp(startSpawnInterval, finalSpawnInterval, t);

                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        private IEnumerator RocksLoop()
        {
            while (true)
            {
                SpawnRocks();
                yield return new WaitForSeconds(rocksEverySeconds);
            }
        }

        private void SpawnObstacle()
        {
            _spawnCounter++;
            SpawnCollectables();

            foreach (var mapping in obstacleMappings)
            {
                if (_spawnCounter % mapping.spawnEveryNth == 0)
                {
                    Instantiate(mapping.prefab, mapping.point.position, Quaternion.identity, spawnContainer);
                    return;
                }
            }

            Instantiate(fallbackRock, fallbackPoint.position, Quaternion.identity, spawnContainer);
        }

        private void SpawnCollectables()
        {
            foreach (var collectable in collectableMappings)
            {
                if (_spawnCounter % collectable.spawnEveryNth == 0)
                {
                    Instantiate(collectable.prefab, collectable.point.position, Quaternion.identity, spawnContainer);
                }
            }
        }

        private void SpawnRocks()
        {
            if (rocks != null && rocksSpawn != null)
            {
                Instantiate(rocks, rocksSpawn.position, Quaternion.identity);
            }
        }

        private void ValidateMappings(List<SpawnItem> list, string label)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var m = list[i];
                if (m.prefab == null || m.point == null || m.spawnEveryNth <= 0)
                {
                    Debug.LogWarning($"[ObstacleSpawner] Invalid {label} mapping at index {i} removed.");
                    list.RemoveAt(i);
                }
            }
        }
    }
}
