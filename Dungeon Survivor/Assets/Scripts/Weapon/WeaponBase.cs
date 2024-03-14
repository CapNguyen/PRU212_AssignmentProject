using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackDirection
{
    None,
    Forward,
    LeftRight,
    UpDown
}
public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData WeaponData;
    public WeaponStats WeaponStats;
    float timer;

    Player playerMovement;
    PlayerManager playerManager;
    public Vector2 vectorOfAttack;
    [SerializeField] AttackDirection attackDirection;
    public void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            Attack();
            timer = WeaponStats.timeToAttack;
        }
    }
    private void Awake()
    {
        playerMovement = GetComponentInParent<Player>();
    }
    public virtual void SetData(WeaponData wd)
    {
        WeaponData = wd;

        WeaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack, wd.stats.numberOfAttacks);
    }
    public abstract void Attack();
    public void ApplyDamage(Collider2D[] hit)
    {
        int damage = GetDamage();
        for (int i = 0; i < hit.Length; i++)
        {
            IDamageable enemy = hit[i].GetComponent<IDamageable>();
            if (enemy != null)
            {
                PostDamage(damage, hit[i].transform.position);
                enemy.TakeDamage(damage);
            }
        }
    }
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
    public void UpdateVectorOfAttack()
    {
        if (attackDirection == AttackDirection.None)
        {
            vectorOfAttack = Vector2.zero;
            return;
        }
        switch (attackDirection)
        {
            case AttackDirection.Forward:
                vectorOfAttack.x = playerMovement.lastHorizontalCoupledVector;
                vectorOfAttack.y = playerMovement.lastVerticalCoupledVector;
                break;
            case AttackDirection.UpDown:
                vectorOfAttack.x = 0f;
                vectorOfAttack.y = playerMovement.lastVerticalDeCoupledVector;
                break;
            case AttackDirection.LeftRight:
                vectorOfAttack.x = playerMovement.lastHorizontalDeCoupledVector;
                vectorOfAttack.y = 0f;
                break;
        }
        vectorOfAttack = vectorOfAttack.normalized;
    }
}
