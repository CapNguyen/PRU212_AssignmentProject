using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingAxeWeapon : WeaponBase
{
    [SerializeField] GameObject axePrefab;
    [SerializeField] float spread = 0.5f;
    Player player;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }



    public override void Attack()
    {
        

        for (int i = 0; i < WeaponStats.numberOfAttacks; i++)
        {
            GameObject throwingAxe = Instantiate(axePrefab);

            Vector3 axepos = transform.position;

            if (WeaponStats.numberOfAttacks > 1)
            {
                axepos.y -= (spread * WeaponStats.numberOfAttacks - 1) / 2; //calculating offset
                axepos.y += (i * spread); //spreading axe along line
            }

            throwingAxe.transform.position = axepos;

            ThrowingAxeProjectile throwingAxeProjectile = throwingAxe.GetComponent<ThrowingAxeProjectile>();
            throwingAxeProjectile.setDirection(player.lastHorizontalMove, 0f);
            throwingAxeProjectile.dmg = GetDamage();
        }
        
    }
}
