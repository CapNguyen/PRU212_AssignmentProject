using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData WeaponData;
    public WeaponStats WeaponStats;
    public float timeToAttack;
    float timer;
    public void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            Attack();
            timer = timeToAttack;
        }
    }
    public virtual void SetData(WeaponData wd)
    {
        WeaponData = wd;
        timeToAttack = WeaponData.stats.timeToAttack;

        WeaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack);
    }
    public abstract void Attack();
}
