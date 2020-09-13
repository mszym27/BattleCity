using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;

    public float spawnDelay;
    public int maxEnemyNum;

    private int currentEnemyNum;
    private float nextSpawnTime;

    private GameObject enemyNumber;

    void Start()
    {
        currentEnemyNum = 0;

        nextSpawnTime = 0; // spawn an enemy immediately, and then wait the delay

        enemyNumber = GameObject.Find("EnemyNumber");
    }

    void Update()
    {
        if(currentEnemyNum < maxEnemyNum)
        {
            if (nextSpawnTime < Time.time)
            {
                Instantiate(enemy, this.transform.position, this.transform.rotation);

                currentEnemyNum++;

                nextSpawnTime = nextSpawnTime + spawnDelay;

                var scriptRef = enemyNumber.GetComponent<TextValue>();
                scriptRef.currentNumberOfEnemies = currentEnemyNum;
            }
        }
    }
}
