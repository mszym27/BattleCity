using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;

    public float spawnDelay = 5;
    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;
    }

    void Update()
    {
        if (nextSpawnTime < Time.time)
        {
            Instantiate(enemy, this.transform.position, this.transform.rotation);

            nextSpawnTime = Time.time + spawnDelay;
        }
    }
}
