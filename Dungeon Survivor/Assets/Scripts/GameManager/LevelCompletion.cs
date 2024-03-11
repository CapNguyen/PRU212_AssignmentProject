using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    [SerializeField] float timeToComplete;

    StageTime stageTime;
    PauseManager pauseManager;

    [SerializeField] LevelCompletionPanel levelCompletionPanel;

    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
        pauseManager = FindObjectOfType<PauseManager>();
        levelCompletionPanel = FindObjectOfType<LevelCompletionPanel>(true);
    }

    public void Update()
    {
        if(stageTime.time > timeToComplete)
        {
            pauseManager.PauseGame();
            levelCompletionPanel.gameObject.SetActive(true);
        }
    }
}
