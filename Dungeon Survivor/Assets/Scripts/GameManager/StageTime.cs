using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTime : MonoBehaviour

{
    public float time;
    TimerUI timerUI;
    // Start is called before the first frame update
    void Awake()
    {
        timerUI = FindObjectOfType<TimerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timerUI.UpdateTime(time);
        
    }
}
