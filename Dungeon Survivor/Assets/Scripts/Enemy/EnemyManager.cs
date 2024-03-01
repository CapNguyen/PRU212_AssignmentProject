using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector2 spawnArea;
    [SerializeField] private float spawnCooldown;
    private float timer;
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = spawnCooldown;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float yRandom = 0;
        float xRandom = Random.Range(-spawnArea.x, spawnArea.x);
        if(xRandom != spawnArea.x || xRandom != -spawnArea.x)
        {
            int rdNum = Random.Range(0, 1);
            yRandom = rdNum == 1 ? spawnArea.y : -spawnArea.y;
        }
        else
        {
            yRandom = Random.Range(-spawnArea.y, spawnArea.y);
        }
        Vector3 spawnPosition = new Vector3(xRandom,yRandom,0);
        spawnPosition += player.transform.position;
        GameObject enemySpawn = Instantiate(enemyPrefab);
        enemySpawn.transform.position = spawnPosition;
        enemySpawn.GetComponent<Enemy>().SetTarget(player);
    }
}
