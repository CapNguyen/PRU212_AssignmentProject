using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearWeapon : WeaponBase
{
    [SerializeField] GameObject spearPrefab;
    [SerializeField] float spread = 0.5f;

    public override void Attack()
    {
        UpdateVectorOfAttack();
        for (int i = 0; i < WeaponStats.numberOfAttacks; i++)
        {
            GameObject throwingAxe = Instantiate(spearPrefab);

            Vector3 axepos = transform.position;

            throwingAxe.transform.position = axepos;

            ThrowingAxeProjectile throwingAxeProjectile = throwingAxe.GetComponent<ThrowingAxeProjectile>();
            throwingAxeProjectile.setDirection(vectorOfAttack.x, vectorOfAttack.y);
            throwingAxeProjectile.dmg = GetDamage();
        }

    }
}
