using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public string enemyDataName;
    public GameObject animatedPrefab;
    public EnemyStats stats;
}
