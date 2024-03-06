using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : WeaponBase
{

    private Player player;
    [SerializeField] private GameObject whipAttackLeft;
    [SerializeField] private GameObject whipAttackRight;
    [SerializeField] private Vector2 attackSize = new Vector2(2, 2);

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }




    private void ApplyDamage(Collider2D[] hit)
    {
        for (int i = 0; i < hit.Length; i++)
        {
            IDamageable enemy = hit[i].GetComponent<IDamageable>();
            if (enemy != null)
            {
                PostDamage(WeaponStats.damage, hit[i].transform.position);
                enemy.TakeDamage(WeaponStats.damage);
            }
        }
    }

    public override void Attack()
    {

        if (player.lastHorizontalMove < 0)
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
    }
}
