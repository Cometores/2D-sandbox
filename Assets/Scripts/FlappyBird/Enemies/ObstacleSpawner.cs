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
        [SerializeField] private float spawnInterval = 2f;

        private float _rocksInterval;
        private int _spawnCounter;

        private void Awake()
        {
            ValidateMappings(obstacleMappings, "Obstacle");
            ValidateMappings(collectableMappings, "Collectable");
        }

        private void Start()
        {
            _rocksInterval = spawnInterval * 2.8f;

            InvokeRepeating(nameof(SpawnObstacle), 0f, spawnInterval);
            InvokeRepeating(nameof(SpawnRocks), 0f, _rocksInterval);
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