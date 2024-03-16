using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingAxeWeapon : WeaponBase
{
    [SerializeField] GameObject axePrefab;
    [SerializeField] float spread = 0.5f;

    public override void Attack()
    {
        UpdateVectorOfAttack();
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            Vector3 axePos = transform.position;

            if (weaponStats.numberOfAttacks > 1)
            {
                axePos.y -= (spread * weaponStats.numberOfAttacks - 1) / 2; //calculating offset
                axePos.y += (i * spread); //spreading axe along line
            }

            SpawnProjectile(axePrefab, axePos);
        }
        
    }
}
