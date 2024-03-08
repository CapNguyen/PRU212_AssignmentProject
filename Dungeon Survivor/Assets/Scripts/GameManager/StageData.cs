using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageEvent
{
    public float time;
    public string message;
    public Enemy enemy;
    public int count;
}

[CreateAssetMenu]
public class StageData : ScriptableObject
{
   public List<StageEvent> stageEvents;
}
