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
        throwingAxe.GetComponent<ThrowingAxeProjectile>().setDirection(player.lastHorizontalMove, 0f);
    }
}
