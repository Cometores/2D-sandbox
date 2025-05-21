using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Represents a class responsible for spawning enemy objects in a game and passing .
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bottomLeft;     // Spawn Edge 1
        [SerializeField] private GameObject topRight;       // Spawn Edge 2
        [SerializeField] private GameObject bossLogic;      // pass to the enemy and then to the boss
        [SerializeField] private GameObject score;
        [SerializeField] private GameObject[] enemy;

        private int _enemyCnt;
        private int _actualEnemyI;

        private TextMeshProUGUI _scoreTxt;
        private Vector3 _xyMax;
        private Vector3 _xyMin;

        private void Start()
        {
            _xyMax = topRight.transform.position;
            _xyMin = bottomLeft.transform.position;

            _enemyCnt = enemy.Length;

            _scoreTxt = score.GetComponent<TextMeshProUGUI>();
            Spawner();
        }

        private void Spawner()
        {
            // random spawn position from bounds
            Vector3 spawnPos = new Vector3(Random.Range(_xyMin.x, _xyMax.x), Random.Range(_xyMin.y, _xyMax.y), 0); 
            
            // spawn enemy
            GameObject newEnemy = Instantiate(enemy[_actualEnemyI], spawnPos, Quaternion.Euler(0,0,-90));
            _actualEnemyI = (_actualEnemyI + 1) % _enemyCnt;

            // transfer variables to the enemy
            var enemyScript = newEnemy.GetComponent<EnemyActions>();
            enemyScript.scoreTxt = _scoreTxt;
            enemyScript.bossLogic = bossLogic;
        
            Invoke(nameof(Spawner), 2);
        }
    }
}
