using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameObject player;
    [SerializeField] StageProgress stageProgress;
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
            //SpawnEnemy(enemyToSpawn);
        }
    }
    private void Start()
    {
        player = GameManager.instance.playerTranform.gameObject;
    }

    public void SpawnEnemy(EnemyData enemyToSpawn)
    {
        float yRandom = Random.Range(-spawnArea.y, spawnArea.y);
        float xRandom = Random.Range(-spawnArea.x, spawnArea.x);
        if(xRandom != spawnArea.x || xRandom != -spawnArea.x)
        {
            int rdNum = Random.Range(0, 1);
            yRandom = rdNum == 1 ? spawnArea.y : -spawnArea.y;
        }
        Vector3 spawnPosition = new Vector3(xRandom,yRandom,0);

        spawnPosition += player.transform.position;

        //spawning main object
        GameObject enemySpawn = Instantiate(enemyPrefab);
        enemySpawn.transform.position = spawnPosition;

        enemySpawn.GetComponent<Enemy>().SetTarget(player);
        enemySpawn.GetComponent<Enemy>().SetStats(enemyToSpawn.stats);
        enemySpawn.GetComponent<Enemy>().UpdateStatsForProgress(stageProgress.Progress);

        enemySpawn.transform.parent = transform;

        //spawning sprite
        GameObject spriteObject = Instantiate(enemyToSpawn.animatedPrefab);
        spriteObject.transform.parent = enemySpawn.transform;
        spriteObject.transform.localPosition = Vector3.zero;
    }
}
