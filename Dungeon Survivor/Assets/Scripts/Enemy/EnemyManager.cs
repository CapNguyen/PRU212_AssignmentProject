using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    private GameObject player;
    [SerializeField] StageProgress stageProgress;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector2 spawnArea;
    [SerializeField] private float spawnCooldown;
    private float timer;

    List<Enemy> bossEnemiesList;
    int totalBossHealth;
    int currentBossHealth;
    [SerializeField] Slider bossHealthBar;

    private void Update()
    {
        UpdateBossHealth();
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
        bossHealthBar = FindObjectOfType<BossHPBar>(true).GetComponent<Slider>();
    }

    private void UpdateBossHealth()
    {
        if(bossEnemiesList == null) { return; }
        if(bossEnemiesList.Count == 0) { return; }
        currentBossHealth = 0;
        for (int i = 0; i < bossEnemiesList.Count; i++)
        {
            if (bossEnemiesList[i] == null) 
            {
                continue;
            }
            currentBossHealth += bossEnemiesList[i].stats.hp;
        }

        bossHealthBar.value = currentBossHealth;

        if(currentBossHealth <= 0)
        {
            bossHealthBar.gameObject.SetActive(false);
            bossEnemiesList.Clear();
        }
    }

    public void SpawnEnemy(EnemyData enemyToSpawn, bool isBoss)
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

        if (isBoss == true)
        {
            SpawnBossEnemy(enemySpawn.GetComponent<Enemy>());
        }

        enemySpawn.transform.parent = transform;

        //spawning sprite
        GameObject spriteObject = Instantiate(enemyToSpawn.animatedPrefab);
        spriteObject.transform.parent = enemySpawn.transform;
        spriteObject.transform.localPosition = Vector3.zero;
    }

    private void SpawnBossEnemy(Enemy newBoss)
    {
        if(bossEnemiesList == null)
        {
            bossEnemiesList = new List<Enemy>();
        }
        bossEnemiesList.Add(newBoss);

        totalBossHealth += newBoss.stats.hp;

        bossHealthBar.gameObject.SetActive(true);
        bossHealthBar.maxValue = totalBossHealth;
    }
}
