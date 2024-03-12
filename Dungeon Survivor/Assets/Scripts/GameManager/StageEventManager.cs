using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageData stageData;
    [SerializeField] private Vector2 spawnArea;
    [SerializeField] EnemyManager enemyManager;

    StageTime stageTime;
    int eventIndexer;

    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
    }

    private void Update()
    {
        if (eventIndexer >= stageData.stageEvents.Count)
        {
            return;
        }
        if (stageTime.time > stageData.stageEvents[eventIndexer].time) 
        {
            switch (stageData.stageEvents[eventIndexer].eventType)
            {
                case StageEvenetType.SpawnEnemy:
                    for (int i = 0; i < stageData.stageEvents[eventIndexer].count; i++)
                    {
                        SpawnEnemy();
                    }
                    break;
                case StageEvenetType.SpawnObject:
                    for(int i = 0; i < stageData.stageEvents[eventIndexer].count; i++)
                    {
                        SpawnObject();
                    }
                    break;
                case StageEvenetType.WinStage:
                    break;
            }
            Debug.Log(stageData.stageEvents[eventIndexer].message);
            eventIndexer += 1;
        }
    }

    private void SpawnEnemy()
    {
        enemyManager.SpawnEnemy(stageData.stageEvents[eventIndexer].enemyToSpawn);
    }

    private void SpawnObject()
    {
        Vector3 positionToSpawn = GameManager.instance.playerTranform.position;

        float yRandom = Random.Range(-spawnArea.y, spawnArea.y);
        float xRandom = Random.Range(-spawnArea.x, spawnArea.x);
        if (xRandom != spawnArea.x || xRandom != -spawnArea.x)
        {
            int rdNum = Random.Range(0, 1);
            yRandom = rdNum == 1 ? spawnArea.y : -spawnArea.y;
        }

        positionToSpawn += new Vector3(xRandom, yRandom, 0);

        SpawnManager.instance.SpawnObject(
                                    positionToSpawn,
                                    stageData.stageEvents[eventIndexer].objectToSpawn
                                    );
    }
}
