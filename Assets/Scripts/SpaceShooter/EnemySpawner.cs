using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* * * Script, that spawns enemies and passes them the necessary variables * * */
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject bottomLeft;     // Spawn Edge 1
    [SerializeField] GameObject topRight;       // Spawn Edge 2
    [SerializeField] GameObject bossLogic;      // pass to the enemy and then to the boss
    [SerializeField] GameObject score;
    [SerializeField] GameObject[] enemy;

    int enemyCnt;
    int actualEnemyI;

    TextMeshProUGUI scoreTxt;
    Vector3 xyMax;
    Vector3 xyMin;

    void Start()
    {
        xyMax = topRight.transform.position;
        xyMin = bottomLeft.transform.position;

        enemyCnt = enemy.Length;

        scoreTxt = score.GetComponent<TextMeshProUGUI>();
        Spawner();
    }

    void Spawner()
    {
        Vector3 spawnPos = new Vector3(Random.Range(xyMin.x, xyMax.x), Random.Range(xyMin.y, xyMax.y), 0);  // random position from bounds
        GameObject newEnemy = Instantiate(enemy[actualEnemyI], spawnPos, Quaternion.Euler(0,0,-90)); // spawn enemy
        actualEnemyI = (actualEnemyI + 1) % enemyCnt;

        // transfer variables to the enemy
        var enemyScript = newEnemy.GetComponent<EnemyActions>();
        enemyScript.scoreTxt = scoreTxt;
        enemyScript.bossLogic = bossLogic;
        
        Invoke(nameof(Spawner), 2);
    }
}
