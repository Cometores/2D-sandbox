using UnityEngine;

/* Spawner for stones, boss, and bonus pills */
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTimer;
    [SerializeField] private GameObject rockUp;
    [SerializeField] private GameObject rockDown;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject bonusPill;

    [SerializeField] private GameObject rockUpSpawnPos;
    [SerializeField] private GameObject rockDownSpawnPos;
    [SerializeField] private GameObject bossSpawnPos;
    [SerializeField] private GameObject bonusPillSpawnPos;

    private float _spawnCounter;

    private void Start() => InvokeRepeating(nameof(Spawner), 0f, 2f);

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
