using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : WeaponBase
{
    [SerializeField] private GameObject whipAttackLeft;
    [SerializeField] private GameObject whipAttackRight;
    [SerializeField] private Vector2 attackSize = new Vector2(2, 2);

    public override void Attack()
    {
        StartCoroutine(AttackProcess());
    }

    IEnumerator AttackProcess()
    {
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            if (player.lastHorizontalDeCoupledVector < 0)
            {
                whipAttackLeft.SetActive(true);
                Collider2D[] hit = Physics2D.OverlapBoxAll(whipAttackLeft.transform.position, attackSize, 0f);
                ApplyDamage(hit);
            }
            else
            {
                whipAttackRight.SetActive(true);
                Collider2D[] hit = Physics2D.OverlapBoxAll(whipAttackRight.transform.position, attackSize, 0f);
                ApplyDamage(hit);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
