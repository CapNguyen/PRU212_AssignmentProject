using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageData stageData;
    [SerializeField] EnemyManager enemyManager;
    int eventIndexer;

    StageTime stageTime;
    // Start is called before the first frame update
    void Awake()
    {
        stageTime = GetComponent<StageTime>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(eventIndexer >= stageData.stageEvents.Count)
        {
            return;
        }

        if (stageTime.time > stageData.stageEvents[eventIndexer].time)
        {
            Debug.Log(stageData.stageEvents[eventIndexer].message);

            for(int i = 0; i < stageData.stageEvents.Count; ++i)
            {
                enemyManager.SpawnEnemy();
            }
            eventIndexer += 1;
        }
    }
}
