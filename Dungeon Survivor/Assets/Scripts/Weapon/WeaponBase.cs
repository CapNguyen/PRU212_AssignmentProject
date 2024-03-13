using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData WeaponData;
    public WeaponStats WeaponStats;
    float timer;

    PlayerManager playerManager;
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

        WeaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack, wd.stats.numberOfAttacks);
    }
    public abstract void Attack();

    public int GetDamage()
    {
        int damage = (int)(WeaponData.stats.damage * playerManager.damageBonus);
        return damage;
    } 

    public virtual void PostDamage(int damage, Vector3 targetPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), targetPosition);
    }

    public void Upgrade(UpgradeData upgradeData)
    {
        WeaponStats.Sum(upgradeData.weaponUpgradeStats);
    }

    internal void AddOwnerCharacter(PlayerManager character)
    {
        playerManager = character;
    }
}
