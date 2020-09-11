using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;

    public float spawnDelay;
    public int maxEnemyNum;

    private int currentEnemyNum = 0;
    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = 0; // spawn an enemy immediately, and then wait the delay
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
            }
        }
    }
}
