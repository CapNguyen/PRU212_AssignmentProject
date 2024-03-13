using System;
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
    PlayerWinManager playerWin;

    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
    }

    private void Start()
    {
        playerWin = FindObjectOfType<PlayerWinManager>();
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
                case StageEventType.SpawnEnemy:
                    SpawnEnemy(false);
                    break;
                case StageEventType.SpawnObject:
                    SpawnObject();
                    break;
                case StageEventType.WinStage:
                    WinStage();
                    break;
                case StageEventType.SpawnEnemyBoss:
                    SpawnEnemy(true);
                    break;
            }
            Debug.Log(stageData.stageEvents[eventIndexer].message);
            eventIndexer += 1;
        }
    }

    private void WinStage()
    {
        FindObjectOfType<PlayerWinManager>().Win();
    }

    private void SpawnEnemy(bool bossEnemy)
    {
        for (int i = 0; i < stageData.stageEvents[eventIndexer].count; i++)
        {
            enemyManager.SpawnEnemy(stageData.stageEvents[eventIndexer].enemyToSpawn, bossEnemy);
        }
    }
        
    private void SpawnObject()
    {
        for (int i = 0; i < stageData.stageEvents[eventIndexer].count; i++)
        {
            Vector3 positionToSpawn = GameManager.instance.playerTranform.position;

            SpawnManager.instance.SpawnObject(stageData.stageEvents[eventIndexer].objectToSpawn);
        }

    }
}
