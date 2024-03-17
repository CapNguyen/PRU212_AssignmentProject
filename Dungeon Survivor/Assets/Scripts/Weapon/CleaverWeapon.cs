using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaverWeapon : WeaponBase
{
    //[SerializeField] float attackAreaSize = 5f;
    //public override void Attack()
    //{
    //    Collider2D[] colliders= Physics2D.OverlapCircleAll(transform.position, attackAreaSize);
    //    ApplyDamage(colliders);
    //}

    [SerializeField] private GameObject leftAtk;
    [SerializeField] private GameObject rightAtk;
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
                leftAtk.SetActive(true);
                Collider2D[] hit = Physics2D.OverlapBoxAll(leftAtk.transform.position, attackSize, 0f);
                ApplyDamage(hit);
            }
            else
            {
                rightAtk.SetActive(true);
                Collider2D[] hit = Physics2D.OverlapBoxAll(rightAtk.transform.position, attackSize, 0f);
                ApplyDamage(hit);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

}
