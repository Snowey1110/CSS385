using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyPrefab;
    public float spawnInterval = 2.0f;
    private float spawnTimer;
    void Start()
    {
        spawnTimer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval;
        }
    }

    public void SpawnEnemy()
    {
        GameObject spawnedEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
