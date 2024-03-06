using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData WeaponData;
    public WeaponStats WeaponStats;
    float timer;
    public void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            Attack();
            timer = WeaponStats.timeToAttack;
        }
    }
    public virtual void SetData(WeaponData wd)
    {
        WeaponData = wd;

        WeaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack);
    }
    public abstract void Attack();

    public virtual void PostDamage(int damage, Vector3 targetPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), targetPosition);
    }

    public void Upgrade(UpgradeData upgradeData)
    {
        WeaponStats.Sum(upgradeData.weaponUpgradeStats);
    }
}
