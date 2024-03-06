using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingAxeWeapon : WeaponBase
{
    [SerializeField] GameObject axePrefab;
    Player player;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }



    public override void Attack()
    {
        GameObject throwingAxe = Instantiate(axePrefab);
        throwingAxe.transform.position = transform.position;
        ThrowingAxeProjectile throwingAxeProjectile = throwingAxe.GetComponent<ThrowingAxeProjectile>();
        throwingAxeProjectile.setDirection(player.lastHorizontalMove, 0f);
        throwingAxeProjectile.dmg = WeaponStats.damage;
    }
}
